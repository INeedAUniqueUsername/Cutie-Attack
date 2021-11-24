using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Material solid;
    public Material ghost;
    private bool phasing = false;
    private double phaseTime;

    // Start is called before the first frame update
    void Start() {
        solid = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update() {
        if (phaseTime > 0) {
            phaseTime -= Time.deltaTime;
            if(phaseTime <= 0) {
                StopPhase();
			}
        }
    }
    public void StartPhase() {
        if (phasing) {
            return;
        }
        phasing = true;
        var r = gameObject.GetComponent<Renderer>();
        r.material = ghost;
    }
    public void StopPhase() {
        if (!phasing) {
            return;
        }
        phasing = false;
        var r = gameObject.GetComponent<Renderer>();
        r.material = solid;
    }
    public void UpdatePhase(Collider other) {
        var g = other.gameObject.GetComponent<Grab>();
        if (g != null) {
            if (g.phasing) {
                phaseTime = 2;
                StartPhase();
            }
        }
    }
    public void OnTriggerEnter(Collider other) {
        UpdatePhase(other);
    }
    public void OnTriggerStay(Collider other) {
        UpdatePhase(other);
    }

    /*
    // Update is called once per frame
    void Update() {}
    public void StartPhase() {
		if (phasing) {
            return;
		}
        phasing = true;
        var r = gameObject.GetComponent<Renderer>();
        r.material = ghost;
    }
    public void StopPhase() {
		if (!phasing) {
            return;
		}
        phasing = false;
        var r = gameObject.GetComponent<Renderer>();
        r.material = solid;
    }
    public void UpdatePhase(Collider other) {
        var g = other.gameObject.GetComponent<Grab>();
        if (g != null) {
            if (g.phasing) {
                StartPhase();
            } else {
                StopPhase();
            }
        }
    }
    public void OnTriggerEnter(Collider other) {
        UpdatePhase(other);
    }
    public void OnTriggerStay(Collider other) {
        UpdatePhase(other);
	}
    public void OnTriggerExit(Collider other) {
        StopPhase();
	}
    */
}
