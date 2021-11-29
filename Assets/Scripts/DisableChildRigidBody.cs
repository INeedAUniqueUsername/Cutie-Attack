using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableChildRigidBody : MonoBehaviour
{
    // Start is called before the first frame update
    private Component[] rigidBodies;
    void Start()
    {
        rigidBodies = this.GetComponentsInChildren<Rigidbody>();
        turnKineticOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void turnKineticOff()
    {
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.isKinematic = true;
        }
    }
}
