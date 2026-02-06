using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.Collections;

public class questionManger : MonoBehaviour
{

    public TextMeshPro questionText;

    public string [] questions;

    private string[] answers = { "yes", "no" };


    public bool question1;
    public bool question2;
    public bool question3;
    public bool question4;

    public int Index = 0;

    private float timer;

    void Start()
    {
        questionText.text = "Hello little Hamster! TO YOUR IMPENDING DOOM! MUAHAHAHAH!";
        StartCoroutine(InitialInstructions());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 4)
        {
            questionText.text = questions[Index];



        }
    }


    private IEnumerator InitialInstructions()
    {
        yield return new WaitForSeconds(4);
        questionText.text = "Answer these questions correctly to survive!";
    }

    /* private IEnumerator ChangeQuestion() 
    {
        
        questionText.text = questions[Index];
    } */

}
