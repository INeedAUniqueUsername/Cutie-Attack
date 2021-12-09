using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource woodImpact;
    private AudioSource metalImpact;
    public float volume = .1f;
    public bool flying = true;

    private Game game;
    void Start()
    {
        woodImpact = this.GetComponent<AudioSource>();
        game = GameObject.FindWithTag("Game").GetComponent<Game>();
        metalImpact = game.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name.Contains("Apple") || other.gameObject.transform.parent?.name.Contains("Apple") == true) {
			if (flying) {
                game.points += 5000;
            }
        }
        if (other.gameObject.name.Contains("Wood")){
            woodImpact.PlayOneShot(woodImpact.clip, volume);
            if(flying) {
                game.points += 1000;
            }
        }

        if (other.gameObject.name.Contains("Metal"))
        {
            metalImpact.PlayOneShot(metalImpact.clip, volume);
            if(flying)
            {
                game.points += 500;
            }
        }

        flying = false;
    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log(other.gameObject.name);
        if (other.gameObject.name.Equals("ExitTrigger")) {
            SceneManager.LoadScene("MainHub", LoadSceneMode.Single);
        }
    }
}
