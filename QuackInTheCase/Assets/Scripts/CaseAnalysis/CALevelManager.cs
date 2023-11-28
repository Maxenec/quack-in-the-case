using UnityEngine;
using System.Collections;
using TMPro;

public class CALevelManager : MonoBehaviour
{
    public Timer timer;
    public TMP_Text questionText;
    private GameManager gameManager;
    private QuestionManager questionManager;
    private float winDelay = 2;
    private bool gameOver = false;

    private void Awake()
    {
        timer = GetComponent<Timer>();
        gameManager = GetComponent<GameManager>();
        questionManager = GetComponent<QuestionManager>();
    }

    private void Start()
    {
        timer.StartTimer(20);
    }

    void Update()
    {
        if (!gameOver)
        {
            CheckGameStatus();
        }
    }

    public void SelectedSuccessfully()
    {
        timer.StopTimer();
        StartCoroutine(GameWinDelay());
    }

    private void CheckGameStatus()
    {
        if (timer.TimerStatus())
        {
            gameOver = true;
            gameManager.LoseGame();
        }
    }

    private IEnumerator GameWinDelay()
    {
        if (questionManager.DidPigeonWin())
        {
            questionText.text = "It's the bloody pigeon?!";
        }
        else
        {
            questionText.text = "It's the bloody cat?!";
        }

        while (winDelay > 0)
        {
            yield return new WaitForSeconds(1.0f);
            winDelay -= 1.0f;

        }
        gameManager.WinGame();
    }
}
