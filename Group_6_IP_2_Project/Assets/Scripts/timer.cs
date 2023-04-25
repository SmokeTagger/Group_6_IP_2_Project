using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{

    public float timeValue = 300;
    public TextMeshProUGUI timeNumber;

    public GameObject gameOver;
    public GameObject draw;

    // runs a count down timer when the timer is active, when its not sets it timer to 0 and the throws up the game over UI
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;

        }
        else
        {
            timeValue = 0;
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            draw.SetActive(true);

        }

        DisplayTime(timeValue);
    }

    // this methos organise the time float to the correct format and the set the text in the UI to it
    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minuets = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeNumber.text = string.Format("{0:00}:{1:00}", minuets, seconds);
    }
}
