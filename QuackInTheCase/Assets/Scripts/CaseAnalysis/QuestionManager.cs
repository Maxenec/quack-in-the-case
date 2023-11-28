using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public string question;
    public Sprite pigeonImage;
    public Sprite catImage;
    public Sprite pigeonSelectionImage;
    public Sprite catSelectionImage;
}

public class QuestionManager : MonoBehaviour
{
    private int currentQuestionIndex = 0;
    private int catPoints = 0;
    private int pigeonPoints = 0;
    private float delay = 2;
    public TMP_Text questionText;
    public TMP_Text consequenceText;
    public TMP_Text pigeonPointsText;
    public TMP_Text catPointsText;
    public Button firstOption;
    public Button secondOption;
    public Button resultCard;
    public GameObject resultCardObject;
    public GameObject firstOptionObject;
    public GameObject secondOptionObject;
    public Question[] questions;
    public CALevelManager caLevelManager;
    private Sprite catResult;
    private Sprite pigeonResult;

    void Start()
    {
        caLevelManager = GetComponent<CALevelManager>();
        resultCardObject.SetActive(false);
        firstOptionObject.SetActive(true);
        secondOptionObject.SetActive(true);
        setQuestion();
    }

    public void setQuestion()
    {
        firstOptionObject.SetActive(true);
        secondOptionObject.SetActive(true);
        if (currentQuestionIndex < questions.Length)
        {
            consequenceText.text = " ";
            Question currentQuestion = questions[currentQuestionIndex];

            questionText.text = currentQuestion.question;

            bool randomBool = Random.Range(0, 2) == 0;

            if (randomBool)
            {
                SetButtonValues(firstOption, currentQuestion.pigeonImage, currentQuestion.pigeonSelectionImage, secondOption, currentQuestion.catImage, currentQuestion.catSelectionImage, true);
            }
            else
            {
                SetButtonValues(firstOption, currentQuestion.catImage, currentQuestion.catSelectionImage, secondOption, currentQuestion.pigeonImage, currentQuestion.pigeonSelectionImage, false);
            }

            currentQuestionIndex++;
        }
        else
        {
            firstOptionObject.SetActive(false);
            secondOptionObject.SetActive(false);
            Debug.Log("No more questions.");
            firstOption.onClick.RemoveAllListeners();
            secondOption.onClick.RemoveAllListeners();
            caLevelManager.SelectedSuccessfully();
        }
    }

    public void CatAnswer()
    {
        catPoints++;
        firstOptionObject.SetActive(false);
        secondOptionObject.SetActive(false);
        resultCardObject.SetActive(true);
        resultCard.transform.Find("Image").GetComponent<Image>().sprite = catResult;
        Debug.Log("Cat Recieves a point.");
        catPointsText.text = catPoints.ToString();
        consequenceText.text = "Owned by the Cat";
        StartCoroutine(SetNextQuestion());
    }

    public void PigeonAnswer()
    {
        pigeonPoints++;
        firstOptionObject.SetActive(false);
        secondOptionObject.SetActive(false);
        resultCardObject.SetActive(true);
        resultCard.transform.Find("Image").GetComponent<Image>().sprite = pigeonResult;
        Debug.Log("Pigeon Recieves a point.");
        pigeonPointsText.text = pigeonPoints.ToString();
        consequenceText.text = "Owned by the Pigeon";
        StartCoroutine(SetNextQuestion());
    }

    void SetButtonValues(Button button1, Sprite outcome1, Sprite result1, Button button2, Sprite outcome2, Sprite result2, bool pigeonFirst)
    {
        if (pigeonFirst)
        {
            button1.onClick.AddListener(() => OutcomeSelected(0));
            button2.onClick.AddListener(() => OutcomeSelected(1));
            catResult = result2;
            pigeonResult = result1;
        }
        else
        {
            button1.onClick.AddListener(() => OutcomeSelected(1));
            button2.onClick.AddListener(() => OutcomeSelected(0));
            catResult = result1;
            pigeonResult = result2;
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

    private IEnumerator SetNextQuestion()
    {
        firstOption.onClick.RemoveAllListeners();
        secondOption.onClick.RemoveAllListeners();
        questionText.text = " ";

        if (currentQuestionIndex < questions.Length)
        {
            while (delay > 0)
            {
                yield return new WaitForSeconds(1.0f);
                delay -= 1.0f;
            }
            delay = 2;
            resultCardObject.SetActive(false);
        }
        setQuestion();
    }

    public bool DidPigeonWin()
    {
        bool check;
        if (pigeonPoints > catPoints)
        {
            check = true;
        }
        else
        {
            check = false;
        }

        return check;
    }
}
