using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBreak : MonoBehaviour
{
    public bool destroyOnBreak;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnJointBreak(float breakForce) {
        if (destroyOnBreak) {
            var joints = gameObject.GetComponents<FixedJoint>();
            if (joints.Length == 1) {
                gameObject.SetActive(false);
            }
        }
        transform.parent.GetComponent<Structure>().OnDamage();
    }
}
