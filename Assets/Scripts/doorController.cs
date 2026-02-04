using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class doorController : MonoBehaviour
{

    public float remainingTime2;

    private float beginningTime = 4.0f;
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

    public float restartTime = 30f;
    public float waitTime;

    public bool canCheckTimeNow = false;

    private bool alreadyDone = false;


    private void Start()
    {
        remainingTime2 = beginningTime;
        warningSymbol.SetActive(true);
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
            warningSymbol.SetActive(false);
            unlockDoor1();
            alreadyDone = true;

        }

        if (beginningTime == 0f && remainingTime2 == 0f && door1locked && !door2locked)
        {
            unlockDoor2();
        } else if (beginningTime == 0f && remainingTime2 == 0f && door2locked && !door3locked)
        {
            unlockDoor3();
        } else if (beginningTime == 0f && remainingTime2 == 0f && door3locked && !door4locked)
        {
            unlockDoor4();
        }



    }

    private void unlockDoor1()
    {
        Debug.Log("Unlocking Door 1");
        doorLockerScript = door1.GetComponent<doorLocker>();
        StartCoroutine(restartTimer());
        doorLockerScript.LockDoors();
        door1locked = true;
    }

    private void unlockDoor2()
    {
        doorLockerScript = door2.GetComponent<doorLocker>();
        StartCoroutine(restartTimer());
        doorLockerScript.LockDoors();
        door2locked = true;
    }
    private void unlockDoor3()
    {
        doorLockerScript = door3.GetComponent<doorLocker>();
        StartCoroutine(restartTimer());
        doorLockerScript.LockDoors();
        door3locked = true;
    }
    private void unlockDoor4()
    {
        doorLockerScript = door4.GetComponent<doorLocker>();
        StartCoroutine(restartTimer());
        doorLockerScript.LockDoors();
        door4locked = true;
    }

    private IEnumerator restartTimer()
    {
        yield return new WaitForSeconds(waitTime);
        remainingTime2 = restartTime;
        canCheckTimeNow = true;
        playerRb.constraints = RigidbodyConstraints2D.None;
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}
