using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BounceStage : MonoBehaviour
{
    // Start is called before the first frame update


    public string scene;

    private float tempY;
    private bool close;
    private float speed = 5f;
    private float height = .8f;
    private float triggerY;
    void Start()
    {
        tempY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        if (close) {
            float newY = Mathf.Sin(Time.time * speed) * height + triggerY;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            if (OVRInput.Get(OVRInput.RawButton.A)) {
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            this.transform.Translate(Vector3.up * 5);
            triggerY = this.transform.position.y;
            close = true;
            Debug.Log("Stage lifted");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            close = false;
            this.transform.position = new Vector3(transform.position.x, tempY, transform.position.z);
            Debug.Log("Stage dropped");
        }
    }
}

