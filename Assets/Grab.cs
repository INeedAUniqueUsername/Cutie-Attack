using UnityEngine;

public class Grab : MonoBehaviour {


	public OVRInput.Controller controller;

    public const float THRESH_GRAB = 0.55f;
    public const float THRESH_DROP = 0.35f;

	private bool grabbing;
	private bool reaching;

	public float grabRadius;
	public LayerMask grabMask;
	public LayerMask slingMask;

	private GameObject grabbedObject;

	private Quaternion lastRotation, currentRotation;

	public GameObject sling;

	void GrabObject(){
		Collider[] hits;
		hits = Physics.OverlapSphere(transform.position, grabRadius, grabMask);
		if (hits.Length == 0){
			return;
		}
		Collider closest = hits[0];
		foreach (Collider h in hits) {
			if (Vector3.Distance(h.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position)) {
				closest = h;
			}
		}

		grabbing = true;
		//grabbedObject = hits[closestHit].transform.gameObject;
		grabbedObject = closest.attachedRigidbody.gameObject;
		//			grabbedObject = hits[closestHit].rigidbody.gameObject;
		grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
		grabbedObject.transform.position = transform.position;
		grabbedObject.transform.parent = transform;

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
			vel = 2* (sling.transform.position - grabbedObject.transform.position);
			//sling = null;
		}
		grabbedObject.GetComponent<Rigidbody>().velocity = vel;
		grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();

		grabbedObject = null;
	}
	void FindSling() {

		Debug.Log("FindSling");
		if (grabbedObject == null) {
			//return;
		}
		if(sling != null) {
			return;
		}


		Collider[] hits;
		hits = Physics.OverlapSphere(transform.position, grabRadius, grabMask, QueryTriggerInteraction.Collide);
		if (hits.Length == 0) {
			return;
		}
		Collider closest = hits[0];
		foreach (Collider h in hits) {
			
			var o = h.gameObject;

			Debug.Log(o.name);
			if (o.name != "SlingArea") {
				sling = o.transform.parent.gameObject;
			}
		}
	}
	Vector3 GetAngularVelocity(){
		Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
		return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
	}

	void Update () {
		if (grabbedObject != null){
			lastRotation = currentRotation;
			currentRotation = grabbedObject.transform.rotation;
		}
		OVRInput.Update();
		if(!grabbing && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) >= THRESH_GRAB) GrabObject();
        if(grabbing && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) <= THRESH_DROP) DropObject();


		if (sling == null && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) >= THRESH_GRAB) FindSling();
	}
}
