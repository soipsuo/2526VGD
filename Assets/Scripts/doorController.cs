using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorController : MonoBehaviour
{

    public float remainingTime2;

    public float beginningTime = 4.0f;
    public GameObject warningSymbol;
    public GameObject player;
    private Rigidbody2D playerRb;

    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;

    private bool door1locked = false;
    private bool door2locked = false;
    private bool door3locked = false;
    private bool door4locked = false;

    doorLocker doorLockerScript;

    public GameObject timerControllerObject;

    private Timer timerScript;

    public float restartTime = 30f;
    public float waitTime;

    public bool canCheckTimeNow = true;

    private bool alreadyDone = false;

    private bool timerLock = false;

    private float timeLOL;

    private bool warning = false;

    private float count = 0f;




    private void Start()
    {
        timerScript = timerControllerObject.GetComponent<Timer>();
        remainingTime2 = beginningTime;
        warningSymbol.SetActive(true);
        warning = true;
        Debug.Log("warningsymbol is: " + warning);
        playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void Update()
    {
        if (beginningTime > 0f)
        {
            beginningTime -= Time.deltaTime;
        }
        if (beginningTime <= 0f && !alreadyDone)
        {
            beginningTime = 0f;
            unlockDoor1();
            alreadyDone = true;

        }

        if (beginningTime == 0f && timerScript.timerFinished)
        {
            if (door1locked && !door2locked)
            {
                unlockDoor2();
            }
            else if (door2locked && !door3locked)
            {
                unlockDoor3();
            } 
            else if (door3locked && !door4locked)
            {
                unlockDoor4();
            }
            
        }

        if (warning)
        {
            Debug.Log("warningsymbol is true");
            timeLOL += Time.deltaTime;

            if (timeLOL <= 0.5f)
            {
                warningSymbol.SetActive(true);
            }
            else if (timeLOL <= 1f)
            {
                warningSymbol.SetActive(false);
            }
            else
            {
                timeLOL = 0f;
                count++;
            }

            if (count >= 3)
            {
                warningSymbol.SetActive(false);
                warning = false;
            }
        }

    }


    private void unlockDoor1()
    {
        if (timerLock)
        {
            return;
        }

        timerLock = true;
        door1locked = true;
        doorLockerScript = door1.GetComponent<doorLocker>();
        StartCoroutine(restartTimer());
        doorLockerScript.LockDoors();
    }

    private void unlockDoor2()
    {
        if (timerLock)
        {
            return;
        }

        timerLock = true;
        door2locked = true;
        doorLockerScript = door2.GetComponent<doorLocker>();
        StartCoroutine(restartTimer());
        doorLockerScript.LockDoors();
    }
    private void unlockDoor3()
    {
        if (timerLock)
        {
            return;
        }

        timerLock = true;
        door3locked = true;
        doorLockerScript = door3.GetComponent<doorLocker>();
        StartCoroutine(restartTimer());
        doorLockerScript.LockDoors();
    }
    private void unlockDoor4()
    {
        if (timerLock)
        {
            return;
        }

        timerLock = true;
        door4locked = true;
        doorLockerScript = door4.GetComponent<doorLocker>();
        doorLockerScript.LockDoors();
    }

    private IEnumerator restartTimer()
    {
        yield return new WaitForSeconds(waitTime);
        remainingTime2 = restartTime;
        canCheckTimeNow = true;
        timerScript.timerFinished = false;
        playerRb.constraints = RigidbodyConstraints2D.None;
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;

        timerLock = false;

    }


}
