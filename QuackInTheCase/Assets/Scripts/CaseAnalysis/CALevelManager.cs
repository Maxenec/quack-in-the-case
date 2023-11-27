using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CALevelManager : MonoBehaviour
{
    public Timer timer;
    private GameManager gameManager;
    public int totalRounds = 3;
    private bool gameOver = false;

    private void Awake()
    {
        timer = GetComponent<Timer>();
        gameManager = GetComponent<GameManager>();
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
        gameManager.WinGame();
    }

    private void CheckGameStatus()
    {
        if (timer.TimerStatus())
        {
            gameOver = true;
            gameManager.LoseGame();
        }
    }
}
