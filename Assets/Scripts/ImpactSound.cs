using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource woodImpact;
    public float volume = .1f;
    void Start()
    {
        woodImpact = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name.Contains("Wood"))
        {
            woodImpact.PlayOneShot(woodImpact.clip, volume);
        }
    }
}
