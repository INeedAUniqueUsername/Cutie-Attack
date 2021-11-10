using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCannon : MonoBehaviour
{
    public GameObject source;
    public GameObject projectile;
    public bool fire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fire = fire || Input.GetKeyDown("space");
        if (fire) {
            fire = false;
            GameObject p = Instantiate(projectile, source.transform.position, Quaternion.identity);
            p.GetComponent<Rigidbody>().velocity = (transform.position - source.transform.position) * 2;
        }
    }
}
