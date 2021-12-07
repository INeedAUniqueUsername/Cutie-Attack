using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DisableChildRigidBody : MonoBehaviour {
    void Start() {
        RecursiveDestroy(gameObject);
        Destroy(this);
    }

    public void RecursiveDestroy(GameObject g) {
        foreach (var a in g.GetComponentsInChildren<FixedJoint>()) {
            Destroy(a);
        }
        foreach (var a in g.GetComponentsInChildren<Rigidbody>()) {
            Destroy(a);
        }
        foreach (var a in g.GetComponentsInChildren<Ghost>()) {
            Destroy(a);
        }
        foreach (var a in g.GetComponentsInChildren<Collider>()) {
            Destroy(a);
        }
        foreach (var a in g.GetComponentsInChildren<Structure>()) {
            Destroy(a);
        }
        foreach (var a in g.GetComponentsInChildren<WoodBreak>()) {
            Destroy(a);
        }
    }
    // Update is called once per frame
    void Update() {}
}
