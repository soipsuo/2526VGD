using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triviaTimer : MonoBehaviour
{

    public float remainingTime;
    [SerializeField] TextMeshPro timerText;

    public float delayTime = 5f;


    void Update()
    {

        delayTime -= Time.deltaTime;

        if (remainingTime > 0 && delayTime <= 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60F);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
