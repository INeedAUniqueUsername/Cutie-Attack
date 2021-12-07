using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HubManager : MonoBehaviour
{
    void Start() { }

    // Update is called once per frame
    void Update() {
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
