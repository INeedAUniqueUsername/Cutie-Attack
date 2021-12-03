using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceStage : MonoBehaviour
{
    // Start is called before the first frame update
    private float tempY;
    bool close;
    public float speed = 5f;
    public float height = .8f;
    private float triggerY;
    private GameObject hubManager;
    private HubManager hubScript;
    void Start()
    {
        tempY = this.transform.position.y;
        hubManager = GameObject.Find("HubManager");
        hubScript = hubManager.GetComponent<HubManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (close)
        {
            float newY = Mathf.Sin(Time.time * speed) * height + triggerY;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.transform.Translate(Vector3.up * 5);
            triggerY = this.transform.position.y;
            close = true;
            if (this.gameObject.name == "Stage1")
            {
                hubScript.stage1 = true;
            } else
            {
                hubScript.stage2 = true;
            }
            Debug.Log("Stage lifted");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close = false;
            this.transform.position = new Vector3(transform.position.x, tempY, transform.position.z);
            Debug.Log("Stage dropped");
            if (this.gameObject.name == "Stage1")
            {
                hubScript.stage1 = false;
            }
            else
            {
                hubScript.stage2 = false;
            }
        }
    }


}

