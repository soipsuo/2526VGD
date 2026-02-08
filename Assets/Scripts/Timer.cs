using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    public static float remainingTime;

    public GameObject doorControllerObject;
    private doorController doorControllerScript;

    bool timeChanged = false;
    float t;

    public bool timerFinished;

    private void Start()
    {
        doorControllerScript = doorControllerObject.GetComponent<doorController>();
        t = doorControllerScript.beginningTime;

        Debug.Log("t is equal to: " + t);
    }

    // Update is called once per frame
    void Update()
    {
        if (t > 0)
        {
            remainingTime = t;
            timeChanged = true;
            t = 0;
        } else if (timeChanged && remainingTime == 0 && doorControllerScript.canCheckTimeNow)
        {
            t = doorControllerScript.remainingTime2;
            doorControllerScript.canCheckTimeNow = false;
        } 

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        } else if (remainingTime < 0)
        {
            remainingTime = 0;
            timerFinished = true;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60F);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
