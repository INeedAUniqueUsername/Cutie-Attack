using UnityEngine;
public class TouchController : MonoBehaviour {
    public OVRInput.Controller controller;
    private GameObject player;
    private Vector3 playerPos;
    private void Start() {
        player = GameObject.Find("OVRPlayerController");
        playerPos = player.transform.position;
    }
    void Update () {
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
    }
}