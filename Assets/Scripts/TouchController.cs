using UnityEngine;
public class TouchController : MonoBehaviour
{
    public OVRInput.Controller controller;
    private GameObject player;
    private Vector3 playerPos;
    //Vector3 offset;
    private void Start() {
        player = GameObject.Find("OVRPlayerController");
        playerPos = player.transform.position;
    }
    void Update() {
        /*
        var v = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        offset += new Vector3(v.x, 0, v.y) / 30;
        */
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
        
    }
}