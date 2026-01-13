using UnityEngine;

public class GridScript : MonoBehaviour
{

    public Transform[,] grid;

    public int width, height;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid = new Transform[width, height];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
 