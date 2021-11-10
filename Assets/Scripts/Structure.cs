using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Linq;

public class Structure : MonoBehaviour {
    public int breakForce;
    public bool destroyOnBreak = false;
    // Start is called before the first frame update
    void Start() {
        var count = transform.childCount;
        for(int i = 0; i < count; i++) {
            Transform c = transform.GetChild(i);
            for(int j = 0; j < count; j++) {
                if(i == j) {
                    continue;
                }
                
                Transform other = transform.GetChild(j);

                
                var joint = c.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                joint.connectedBody = other.gameObject.GetComponent<Rigidbody>();
                joint.breakForce = breakForce;
            }
            if (destroyOnBreak) {
                c.gameObject.AddComponent(typeof(WoodBreak));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
