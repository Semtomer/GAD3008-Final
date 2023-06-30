using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float timeValue = 30;
    public Text timeText;

    void Start()
    {
    }


    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);

        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("GameSection");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("FinishSection");
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
            HUDController.lapCount -= 1;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }
        else
        {
            SceneManager.LoadScene("GameSection", LoadSceneMode.Single);
        }
           
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{00}", seconds);
    }
}
