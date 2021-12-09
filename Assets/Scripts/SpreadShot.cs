using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{
    public bool fired;
    [SerializeField] private GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        fired = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fired)
        {
            fired = false;
            GameObject temp = Instantiate(prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 5), this.transform.rotation);
            temp.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
            GameObject temp2 = Instantiate(prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 5), this.transform.rotation);
            temp2.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
        }
    }
}
