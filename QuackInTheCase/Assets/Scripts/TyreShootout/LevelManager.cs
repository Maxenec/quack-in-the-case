using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject enemyCar;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Timer>().StartTimer(10);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Timer>() != null)
        {
            if (gameObject.GetComponent<Timer>().TimerStatus() == true && gameOver == false)
            {
                gameOver = true;
                enemyCar.GetComponent<EnemyCarMovement>().ShotMissed();
            }
        }
        else if (enemyCar != null && enemyCar.GetComponent<EnemyCarMovement>().AnimationStatus() == true)
        {
            FailGame();
        }
        else if (enemyCar == null && gameOver == false)
        {
            gameOver = true;
            gameObject.GetComponent<GameManager>().EndGame();
        }
    }

    public void CarCollided()
    {
        Destroy(gameObject.GetComponent<Timer>());
        gameObject.GetComponent<GameManager>().WinGame();
    }

    public void FailGame()
    {
        Destroy(gameObject.GetComponent<Timer>());
        enemyCar.GetComponent<EnemyCarMovement>().ShotMissed();

    }
}
