using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource woodImpact;
    public float volume = .1f;
    public bool flying = false;

    private Game game;
    void Start()
    {
        woodImpact = this.GetComponent<AudioSource>();
        game = GameObject.FindWithTag("Game").GetComponent<Game>();
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

        if(other.gameObject.name.Equals("ExitTrigger")) {
            SceneManager.LoadScene("MainHub", LoadSceneMode.Single);
        }

        if(flying) {
            var r = gameObject.GetComponent<Rigidbody>();

            int count = 0;
            foreach (Transform t in gameObject.transform) {
                if (t.gameObject.GetComponent<Collider>() != null) {
                    count++;
                }
            }


            var m = r.mass / count;
            var av = r.angularVelocity;
            var v = r.velocity;
            foreach (Transform t in gameObject.transform) {
                if (t.gameObject.GetComponent<Collider>() != null) {
                    var rb = t.gameObject.AddComponent<Rigidbody>();
                    rb.mass = m;
                    rb.angularVelocity = av;
                    rb.velocity = v;
                }
                else {
                    Destroy(t.gameObject);
                }
            }
            Destroy(r);
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
