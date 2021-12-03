using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HubManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool stage1;
    public bool stage2;
    void Start()
    {
        stage1 = false;
        stage2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.A)) //use Occulus input here for actual lab
        {
            if (stage1)
            {
                Debug.Log("Stage 1");
                SceneManager.LoadScene("L1", LoadSceneMode.Single);
            }

            if (stage2)
            {
                Debug.Log("Stage 2");
                SceneManager.LoadScene("SandBox", LoadSceneMode.Single);
            }
        }

        if (OVRInput.Get(OVRInput.RawButton.B)) //use the B button in the Occulus Rift for the condition code here
        {
           #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
           #else
            Application.Quit();
           #endif
        }
    }

    }
