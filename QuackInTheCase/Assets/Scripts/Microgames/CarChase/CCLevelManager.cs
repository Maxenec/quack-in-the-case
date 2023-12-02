using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCLevelManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject player;
    public GameObject obstacleGenerator;
    private GameObject arcadeManager;

    // Start is called before the first frame update
    private void Start()
    {
        if(GameObject.Find("ArcadeManager") != null){
            arcadeManager = GameObject.Find("ArcadeManager");
        }
        gameObject.GetComponent<Timer>().StartTimer(20);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameOver)
        {
            CheckGameStatus();
        }
    }

    private void CheckGameStatus()
    {
        if (gameObject.GetComponent<Timer>().TimerStatus())
        {
            Destroy(obstacleGenerator);
            WinLevel();
        }
        else if (player.GetComponent<CarControlls>().HitStatus())
        {
            FailLevel();
        }
    }

    private void WinLevel()
    {
        gameOver = true;
        if(arcadeManager != null){
            arcadeManager.GetComponent<ArcadeMode>().ArcadeButton();
        }else{
            gameObject.GetComponent<GameManager>().WinGame();
        }
    }

    private void FailLevel()
    {
        gameOver = true;
        if(arcadeManager != null){
            arcadeManager.GetComponent<ArcadeMode>().ArcadeFail();
        }else{
            gameObject.GetComponent<GameManager>().LoseGame();
        }
    }
}
