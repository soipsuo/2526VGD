using UnityEngine;

public class PentominoReset : MonoBehaviour
{
    public GameObject Pentomino;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ResetCollide"))
        {
            Vector3 currentPosition = Pentomino.transform.position;
            currentPosition.y += 7;
            Pentomino.transform.position = currentPosition;
        }
    }
}