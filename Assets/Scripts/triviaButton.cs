using UnityEngine;

public class triviaButton : MonoBehaviour
{
    public bool answered;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            answered = true;
        }
    }

}
