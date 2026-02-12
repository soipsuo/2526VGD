using Unity.VisualScripting;
using UnityEngine;

public class LavaRising : MonoBehaviour
{

    private bool lavaRising = false;
    private float timer;

    public GameObject warningSymbols;
    public GameObject lavaUI;

    public float count;

    
    void Start()
    {
        lavaUI.SetActive(true);
        lavaRising = true;
    }

    void Update()
    {
        if(lavaRising)
        {
            Debug.Log("warningsymbol is true");
            timer += Time.deltaTime;

            if (timer <= 0.5f)
            {
                warningSymbols.SetActive(true);
            }
            else if (timer <= 1f)
            {
                warningSymbols.SetActive(false);
            }
            else
            {
                timer = 0f;
                count++;
            }

            if (count >= 3)
            {
                warningSymbols.SetActive(false);
                lavaUI.SetActive(false);
                lavaRising = false;
            }
        }
    }
}
