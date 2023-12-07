using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSLevelManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject susHP;
    public GameObject playerHP;
    public GameObject shield;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Timer>().StartTimer(30);
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
        if (gameObject.GetComponent<Timer>().TimerStatus())
        {
            shield.SetActive(false);
            FailLevel();
        } else if (playerHP.GetComponent<HP>().CheckHP()){
            //Debug.Log("ded");
            shield.SetActive(false);
            FailLevel();
        } else if (susHP.GetComponent<HP>().CheckHP()){
            shield.SetActive(false);
            WinLevel();
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
