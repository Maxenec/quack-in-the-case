using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject enemyCar;
    private bool failureAnimationCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Timer>().StartTimer(5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            CheckGameStatus();
        }
    }

    void CheckGameStatus()
    {
        Timer timerComponent = gameObject.GetComponent<Timer>();
        EnemyCarMovement enemyCarComponent = enemyCar.GetComponent<EnemyCarMovement>();

        if (timerComponent.TimerStatus())
        {
            if (!enemyCarComponent.FailAnimationStatus())
            {
                FailAnimation();
            }
            else if (enemyCarComponent.WinAnimationStatus())
            {
                WinAnimation();
            }
            else if (enemyCarComponent.FailAnimationCompleted())
            {
                failureAnimationCompleted = true;
            }
        }

        if (failureAnimationCompleted)
        {
            ShowFailScreen();
        }
    }

    void ShowFailScreen()
    {
        gameOver = true;
        gameObject.GetComponent<GameManager>().LoseGame();
    }

    public void CarCollided()
    {
        gameObject.GetComponent<Timer>().StopTimer();
        gameObject.GetComponent<GameManager>().WinGame();
    }

    public void FailAnimation()
    {
        if (gameObject.GetComponent<Timer>().TimerStatus() == false)
        {
            gameObject.GetComponent<Timer>().StopTimer();
        }
        enemyCar.GetComponent<EnemyCarMovement>().ShotMissed();
    }

    public void WinAnimation()
    {
        gameObject.GetComponent<Timer>().StopTimer();
        enemyCar.GetComponent<EnemyCarMovement>().HitCar();
    }
}
