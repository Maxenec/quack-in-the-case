using System.Collections;
using UnityEngine;

public class FCLevelManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject player;
    public GameObject obstacleGenerator;
    private Timer timer;
    private GameManager gameManager;

    private void Awake()
    {
        //Simplifies the getcomponent into one word in order to make the code more readable.
        timer = GetComponent<Timer>();
        gameManager = GetComponent<GameManager>();
    }

    private void Start()
    {
        timer.StartTimer(15);
    }

    private void Update()
    {
        if (!gameOver)
        {
            CheckGameStatus();
        }
    }

    private void CheckGameStatus()
    {
        if (timer.TimerStatus())
        {
            EndLevel(true);
        }
        else if (player.GetComponent<PlayerRunController>().HitStatus())
        {
            EndLevel(false);
        }
    }

    private void EndLevel(bool isWin)
    {
        gameOver = true;
        if (isWin)
        {
            gameManager.WinGame();
        }
        else
        {
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        float winDelay = 1f;
        Time.timeScale = 0.1f;
        AudioManager.Instance.PlaySFX("BarrelHit");
        while (winDelay > 0)
        {
            yield return new WaitForSeconds(0.1f);
            winDelay -= 1f;
        }
        gameManager.LoseGame();
    }
}
