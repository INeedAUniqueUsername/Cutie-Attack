using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Linq;

public class Structure : MonoBehaviour {
    public int breakForce;
    public bool destroyOnBreak = false;
    public bool intact = true;
    public int score = 100;
    public bool apple = false;
    // Start is called before the first frame update
    void Start() {

        if (apple) {
            GameObject.FindWithTag("Game").GetComponent<Game>().apples++;
        }
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
            c.gameObject.AddComponent(typeof(WoodBreak));
            c.gameObject.GetComponent<WoodBreak>().destroyOnBreak = destroyOnBreak;
        }
    }
    public void OnDamage() {
        if(intact) {
            GameObject.FindWithTag("Game").GetComponent<Game>().points += score;
            if (apple) {
                GameObject.FindWithTag("Game").GetComponent<Game>().apples--;
            }
            intact = false;
        }
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
