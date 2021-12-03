using UnityEngine;

public class Grab : MonoBehaviour {


	public OVRInput.Controller controller;

    public const float THRESH_GRAB = 0.55f;
    public const float THRESH_DROP = 0.35f;

	private bool grabbing;
	private bool reaching;

	private float grabRadius = 1;
	public LayerMask grabMask;
	public LayerMask slingMask;

	private GameObject grabbedObject;

	private Quaternion lastRotation, currentRotation;

	public GameObject slingOrigin;

	private GameObject sling;
	private LineRenderer slingBand;

	public bool phasing = false;

	public Material solid;
	public Material holding;
	public Material ghost;

	public Grab() {}

	void GrabObject(){
		Collider[] hits;
		hits = Physics.OverlapSphere(transform.position, grabRadius, grabMask);
		if (hits.Length == 0){
			//Debug.Log("No hits");
			return;
		}

		Collider closest = null;
		foreach (Collider h in hits) {
			if (h.isTrigger || h.attachedRigidbody == null || h.gameObject.name.Contains("Crosshair")) {
				//Debug.Log("Skip trigger");
				continue;
			}
			//Debug.Log("Hit: " + h.gameObject.name);
			if (closest == null || Vector3.Distance(h.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position)) {
				closest = h;
			}
		}
		if(closest == null) {
			return;
		}
		//Debug.Log("Closest: " + closest.gameObject.name);

		grabbing = true;
		//grabbedObject = hits[closestHit].transform.gameObject;
		grabbedObject = closest.attachedRigidbody.gameObject;
		//			grabbedObject = hits[closestHit].rigidbody.gameObject;
		grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
		grabbedObject.transform.position = transform.position;
		grabbedObject.transform.parent = transform;

		foreach (Transform c in gameObject.transform) {
			c.gameObject.GetComponent<Renderer>().material = holding;
		}
	}
	void DropObject(){

		grabbing = false;

		if (grabbedObject == null){
			return;
		}

		grabbedObject.transform.parent = null;
		grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

		Vector3 vel = new Vector3(0, 0, 0);
		if(sling != null) {
			vel = (sling.transform.position - grabbedObject.transform.position);
			vel = Vector3.Normalize(vel) * Mathf.Pow(Vector3.Magnitude(vel), 1.3f);
			sling = null;
		}

		grabbedObject.GetComponent<Rigidbody>().velocity = vel;
		grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();

		var missile = grabbedObject.GetComponent<Missile>();// ?? grabbedObject.transform.parent.GetComponent<Missile>();
		if (missile != null) { missile.flying = true; }
		grabbedObject = null;

		foreach (Transform c in gameObject.transform) {
			c.gameObject.GetComponent<Renderer>().material = solid;
		}
	}
	void FindSling() {

		//Debug.Log("FindSling");

		if(Vector3.Magnitude(grabbedObject.transform.position - slingOrigin.transform.position) < 5) {
			sling = slingOrigin;
			slingBand = sling.GetComponent<LineRenderer>();
		}
	}
	Vector3 GetAngularVelocity(){
		Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
		return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
	}

	public void StartPhase() {
		phasing = true;
		if(grabbing) {
			DropObject();
		}

		foreach (Transform c in gameObject.transform) {
			c.gameObject.GetComponent<Renderer>().material = ghost;
		}
	}
	public void StopPhase() {
		phasing = false;

		foreach(Transform c in gameObject.transform) {
			c.gameObject.GetComponent<Renderer>().material = solid;
		}
	}

	void Update () {
		if (grabbedObject != null){
			lastRotation = currentRotation;
			currentRotation = grabbedObject.transform.rotation;

			if(sling != null) {
				
				var v = (grabbedObject.transform.position - sling.transform.position);
				v = new Vector3(-v.x, v.y, -v.z);
				slingBand.SetPosition(1, v + new Vector3(0, 0, -1));
				slingBand.SetPosition(2, v + new Vector3(0, 0, 1));
			} else {
				FindSling();
			}
		}
		OVRInput.Update();
		if (phasing) {
			if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) <= THRESH_DROP) StopPhase();
		} else {
			if (!grabbing && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) >= THRESH_GRAB) GrabObject();
			if (grabbing && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) <= THRESH_DROP) DropObject();
			if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) >= THRESH_GRAB) StartPhase();
		}
	}
}
