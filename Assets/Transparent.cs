using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var m = gameObject.GetComponent<Renderer>().material;
        m.SetColor("_Color", new Color(m.color.r, m.color.g, m.color.b, 0.5f));
    }
}
