using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSLevelManager : MonoBehaviour
{
    private bool gameOver = false;
    public GameObject susHP;
    public GameObject playerHP;
    private GameObject arcadeManager;
    
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("ArcadeManager") != null){
            arcadeManager = GameObject.Find("ArcadeManager");
        }
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
            FailLevel();
        } else if (playerHP.GetComponent<HP>().CheckHP()){
            //Debug.Log("ded");
            FailLevel();
        } else if (susHP.GetComponent<HP>().CheckHP()){
            WinLevel();
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
