using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCLevelManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject player;
    public GameObject obstacleGenerator;

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.GetComponent<Timer>().StartTimer(20);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameOver)
        {
            StartCoroutine(CheckGameStatus());
        }
    }

    IEnumerator CheckGameStatus()
    {
        if (gameObject.GetComponent<Timer>().TimerStatus())
        {
            Destroy(obstacleGenerator);
            WinLevel();
        }
        else if (player.GetComponent<CarControlls>().HitStatus())
        {
            yield return new WaitForSeconds(0.667f);
            FailLevel();
        }
    }

    private void WinLevel()
    {
        gameOver = true;
            gameObject.GetComponent<GameManager>().WinGame();
    }

    private void FailLevel()
    {
        gameOver = true;
            gameObject.GetComponent<GameManager>().LoseGame();
    }
}
