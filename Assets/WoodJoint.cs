using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Linq;

public class WoodJoint : MonoBehaviour
{
    public int breakForce;
    // Start is called before the first frame update
    void Start() {
        foreach(Transform c in transform) {
            foreach(Transform other in transform) {
                if(c == other) {
                    continue;
                }

                var j = c.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                j.connectedBody = other.gameObject.GetComponent<Rigidbody>();
                j.breakForce = breakForce;
            }
            
            c.gameObject.AddComponent(typeof(WoodBreak));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
