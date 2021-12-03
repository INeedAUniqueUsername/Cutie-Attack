using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
            if(value == 0) {
                Win();
			}
        }
        get => _apples;
    }

    public double restartTimer = -1;
    public double winTimer = -1;
    public void updateDisplay() {
        if (scoreDisplay != null) {
            if (restartTimer == -1 && winTimer == -1) {
                scoreDisplay.text = $"Score: {points}\nApples: {apples}";
            } else if (restartTimer == -1) {
                scoreDisplay.text = $"Score: {points}";
            } else {
                scoreDisplay.text = $"";
            }
            
        }

    }
    public GameObject infoObject, scoreObject;
    private Text infoDisplay, scoreDisplay;

    public bool won;

    // Start is called before the first frame update
    void Start() {
        infoDisplay = infoObject.GetComponent<Text>();
        scoreDisplay = scoreObject.GetComponent<Text>();

        updateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if(restartTimer > 0) {
            if (restartTimer < Time.deltaTime) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else {
                restartTimer -= Time.deltaTime;
            }
        }
        if(winTimer > 0) {
            if(winTimer < Time.deltaTime) {
                SceneManager.LoadScene("Hub");
            } else {
                winTimer -= Time.deltaTime;
			}
        }
    }
    public void Win() {
        if(restartTimer > 0) {
            return;
		}
        infoDisplay.text = "Stage cleared!";
        won = true;
        winTimer = 10;
    }
    public void Disqualify() {
		if (won) {
            return;
		}
        if (restartTimer < 0) {
            infoDisplay.text = "DISQUALIFIED!!!";
            restartTimer = 10;
        }
	}
}
