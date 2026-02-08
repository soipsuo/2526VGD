using UnityEngine;

public class terminalscript : MonoBehaviour
{
    public static bool terminal1Complete = false;
    public static bool terminal2Complete = false;
    public static bool terminal3Complete = false;

    private bool playerCollided = false;
    private GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerCollided = true;
            Debug.Log("Player collision state: " + playerCollided);
        }
    }

    private void Update()
    {
        if (playerCollided && Input.GetKeyDown(KeyCode.F) && !terminal1Complete)
        {
            terminal1Complete = true;
            Debug.Log("Input recieved " + terminal1Complete);
        }
        else if (playerCollided && Input.GetKeyDown(KeyCode.F) && terminal1Complete && !terminal2Complete)
        {
            terminal2Complete = true;
            Debug.Log("Input recieved " + terminal2Complete);
        }
        else if (playerCollided && Input.GetKeyDown(KeyCode.F) && terminal1Complete && terminal2Complete && !terminal3Complete)
        {
            terminal3Complete = true;
            Debug.Log("Input recieved " + terminal3Complete);
        }

    }
}
