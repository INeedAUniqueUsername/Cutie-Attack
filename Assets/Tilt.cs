using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour
{
    public bool disqualified = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Magnitude(gameObject.transform.localPosition) > 0.1) {
            disqualified = true;
            GameObject.FindWithTag("Game").GetComponent<Game>().Disqualify();
        }
    }
}
