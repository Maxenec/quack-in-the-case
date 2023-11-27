using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public int ID;
    public string question;
    public Sprite pigeonImage;
    public Sprite catImage;
}

public class QuestionManager : MonoBehaviour
{
    private int currentQuestionIndex = 0;
    public TMP_Text questionText;
    public TMP_Text consequenceText;
    public Button firstOption;
    public Button secondOption;
    public Question[] questions;
    public CALevelManager caLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        caLevelManager = GetComponent<CALevelManager>();
        setQuestion();
    }

    public void setQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            Question currentQuestion = questions[currentQuestionIndex];

            questionText.text = currentQuestion.question;

            bool randomBool = Random.Range(0, 2) == 0;

            if (randomBool)
            {
                SetButtonValues(firstOption, currentQuestion.pigeonImage, secondOption, currentQuestion.catImage, true);
            }
            else
            {
                SetButtonValues(firstOption, currentQuestion.catImage, secondOption, currentQuestion.pigeonImage, false);
            }

            currentQuestionIndex++;
        }
        else
        {
            Debug.Log("No more questions.");
            firstOption.onClick.RemoveAllListeners();
            secondOption.onClick.RemoveAllListeners();
            caLevelManager.SelectedSuccessfully();
        }
    }

    public void CatAnswer()
    {
        Debug.Log("Cat Recieves a point.");
        setQuestion();
    }

    public void PigeonAnswer()
    {
        Debug.Log("Pigeon Recieves a point.");
        setQuestion();
    }

    void SetButtonValues(Button button1, Sprite outcome1, Button button2, Sprite outcome2, bool pigeonFirst)
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        if (pigeonFirst)
        {
            button1.onClick.AddListener(() => OutcomeSelected(0));
            button2.onClick.AddListener(() => OutcomeSelected(1));
        }
        else
        {
            button1.onClick.AddListener(() => OutcomeSelected(1));
            button2.onClick.AddListener(() => OutcomeSelected(0));
        }

        button1.transform.Find("Image").GetComponent<Image>().sprite = outcome2;
        button2.transform.Find("Image").GetComponent<Image>().sprite = outcome1;
    }

    public void OutcomeSelected(int outcome)
    {
        if (outcome == 0)
        {
            CatAnswer();
        }
        else if (outcome == 1)
        {
            PigeonAnswer();
        }
    }
}
