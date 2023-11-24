using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject enemyCar;
    private bool failureAnimationCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Timer>().StartTimer(5);
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
        Timer timerComponent = GetComponent<Timer>();
        EnemyCarMovement enemyCarComponent = enemyCar.GetComponent<EnemyCarMovement>();

        if (timerComponent.TimerStopped() || timerComponent.TimerStatus())
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
        GetComponent<GameManager>().LoseGame();
    }

    public void CarCollided()
    {
        GetComponent<Timer>().StopTimer();
        GetComponent<GameManager>().WinGame();
    }

    public void FailAnimation()
    {
        if (!GetComponent<Timer>().TimerStatus())
        {
            GetComponent<Timer>().StopTimer();
        }
        enemyCar.GetComponent<EnemyCarMovement>().ShotMissed();
    }

    public void WinAnimation()
    {
        GetComponent<Timer>().StopTimer();
        enemyCar.GetComponent<EnemyCarMovement>().HitCar();
    }
}
