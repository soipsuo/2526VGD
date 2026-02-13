using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.Design;
using UnityEngine.SceneManagement;

public class questionManger : MonoBehaviour
{

    public TextMeshPro questionText;

    public string [] questions;

    triviaButton button1Script;
    triviaButton button2Script;

    public GameObject button1;
    public GameObject button2;

    public GameObject[] answers1;
    public GameObject[] answers2;

    private GameObject currentAnswer1;
    private GameObject currentAnswer2;

    public bool question1;
    public bool question2;
    public bool question3;
    public bool question4;

    public bool question1answered = false;
    public bool question2answered = false;
    public bool question3answered = false;
    public bool question4answered = false;

    bool calledAlready = false;

    public int Index = 0;

    private float timer;

    public bool initialQuestion = false;


    void Start()
    {
        questionText.text = "Hello little Hamster! TO YOUR IMPENDING DOOM! MUAHAHAHAH!";
        StartCoroutine(InitialInstructions());
        button1Script = button1.GetComponent<triviaButton>();
        button2Script = button2.GetComponent<triviaButton>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer <10)
        {
            button1Script.answered = false;
            button2Script.answered = false;
        }

        if (timer >= 10)
        {
            if (!initialQuestion)
            {
                questionText.text = questions[Index];
                currentAnswer1 = answers1[Index];
                currentAnswer2 = answers2[Index];
                currentAnswer1.SetActive(true);
                currentAnswer2.SetActive(true);
                initialQuestion = true;
            }

            if (button1Script.answered || button2Script.answered)
            {
                if (Index == 0 && !calledAlready)
                {
                    Debug.Log("Question 1 function called");
                    question1Function();
                    calledAlready = true;
                }
                else if (Index == 1)
                {
                    Debug.Log("Question 2 function called");
                    question2Function();
                    calledAlready = true;
                }
                else if (Index == 2)
                {
                    Debug.Log("Question 3 function called");
                    question3Function();
                    calledAlready = true;
                }
                else if (Index == 3)
                {
                    Debug.Log("Question 4 function called");
                    question4Function();
                    calledAlready = true;
                }

            }

        }
    }

    public void question1Function()
    {
        if (!question1 && !question1answered && button1Script.answered)
        {
            button1Script.answered = false;
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question1answered = true;
            questionText.text = "Correct answer! Moving onto next question.";
            StartCoroutine(ChangeQuestion());

        }
        else if (question1 && !question1answered && button2Script.answered)
        {
            StartCoroutine(ChangeQuestion());
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            questionText.text = "Correct answer! Moving onto next question.";
            button2Script.answered = false;
            question1answered = true;
        }
        else if (!question1 && !question1answered && button2Script.answered)
        {
            questionText.text = "WRONG ANSWER! TIME TO RESTART!";
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question1answered = true;
            StartCoroutine(WrongAnswer());
        }
        else if (question1 && !question1answered && button1Script.answered)
        {
            questionText.text = "WRONG ANSWER! TIME TO RESTART!";
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question1answered = true;
            StartCoroutine(WrongAnswer());
        }
    }

    public void question2Function()
    {
        if (!question2 && !question2answered && button1Script.answered)
        {
            button1Script.answered = false;
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question2answered = true;
            questionText.text = "Correct answer! Moving onto next question.";
            StartCoroutine(ChangeQuestion());

        }
        else if (question2 && !question2answered && button2Script.answered)
        {
            questionText.text = "Correct answer! Moving onto next question.";
            button2Script.answered = false;
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question2answered = true;
            StartCoroutine(ChangeQuestion());
        }
        else if (!question2 && !question2answered && button2Script.answered)
        {
            questionText.text = "WRONG ANSWER! TIME TO RESTART!";
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question2answered = true;
            StartCoroutine(WrongAnswer());
        }
        else if (question2 && !question2answered && button1Script.answered)
        {
            questionText.text = "WRONG ANSWER! TIME TO RESTART!";
            question2answered = true;
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            StartCoroutine(WrongAnswer());
        }
    }

    public void question3Function()
    {
        if (!question3 && !question3answered && button1Script.answered)
        {
            button1Script.answered = false;
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question3answered = true;
            questionText.text = "Correct answer! Moving onto next question.";
            StartCoroutine(ChangeQuestion());

        }
        else if (question3 && !question3answered && button2Script.answered)
        {
            StartCoroutine(ChangeQuestion());
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            questionText.text = "Correct answer! Moving onto next question.";
            button2Script.answered = false;
            question3answered = true;
        }
        else if (!question3 && !question3answered && button2Script.answered)
        {
            questionText.text = "WRONG ANSWER! TIME TO RESTART!";
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question3answered = true;
            StartCoroutine(WrongAnswer());
        }
        else if (question3 && !question3answered && button1Script.answered)
        {
            questionText.text = "WRONG ANSWER! TIME TO RESTART!";
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question3answered = true;
            StartCoroutine(WrongAnswer());
        }
    }

    public void question4Function()
    {
        if (!question4 && !question4answered && button1Script.answered)
        {
            StartCoroutine(endScene());
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            button1Script.answered = false;
            question4answered = true;
            questionText.text = "Correct answer! You survived! Congrats!";
    
        }
        else if (question4 && !question4answered && button2Script.answered)
        {
            StartCoroutine(endScene());
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            button2Script.answered = false;
            questionText.text = "Correct answer! You survived! Congrats!";
            question4answered = true;
        }
        else if (!question4 && !question4answered && button2Script.answered)
        {
            questionText.text = "WRONG ANSWER! TIME TO RESTART!";
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question4answered = true;
            StartCoroutine(WrongAnswer());
        }
        else if (question4 && !question4answered && button1Script.answered)
        {
            questionText.text = "WRONG ANSWER! TIME TO RESTART!";
            currentAnswer1.SetActive(false);
            currentAnswer2.SetActive(false);
            question4answered = true;
            StartCoroutine(WrongAnswer());
        }
    }


    private IEnumerator InitialInstructions()
    {
        yield return new WaitForSeconds(4);
        questionText.text = "Answer these questions correctly to survive!";
    }

    private IEnumerator WrongAnswer()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

     private IEnumerator ChangeQuestion() 
    {
        yield return new WaitForSeconds(3);
        calledAlready = false;
        Index++;
        currentAnswer1 = answers1[Index];
        currentAnswer2 = answers2[Index];
        currentAnswer1.SetActive(true);
        currentAnswer2.SetActive(true);
        questionText.text = questions[Index];

    }

    private IEnumerator endScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
