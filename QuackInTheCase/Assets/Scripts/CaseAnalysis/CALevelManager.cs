using UnityEngine;
using System.Collections;

public class CALevelManager : MonoBehaviour
{
    public Timer timer;
    private GameManager gameManager;
    public int totalRounds = 3;
    private float winDelay = 2;
    private bool gameOver = false;

    private void Awake()
    {
        timer = GetComponent<Timer>();
        gameManager = GetComponent<GameManager>();
    }

    private void Start()
    {
        timer.StartTimer(10);
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
        while (winDelay > 0)
        {
            yield return new WaitForSeconds(1.0f);
            winDelay -= 1.0f;

        }
        gameManager.WinGame();
    }
}
