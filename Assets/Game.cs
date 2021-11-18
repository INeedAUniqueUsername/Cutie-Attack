using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game : MonoBehaviour
{
    private int _points;
    public int points {
        set {
            _points = value;
            updateDisplay();
        } get => _points;
    }

    private int _apples;
    public int apples {
        set {
            _apples = value;
            updateDisplay();
        }
        get => _apples;
    }
    public void updateDisplay() {
        scoreDisplay.text = $"Score: {points}\nApples: {apples}";

    }
    public GameObject scoreObject;
    private Text scoreDisplay;
    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay = scoreObject.GetComponent<Text>();
        points = 0;
        apples = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
