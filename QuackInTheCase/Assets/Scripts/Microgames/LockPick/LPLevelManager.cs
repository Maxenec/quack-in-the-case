using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPLevelManager : MonoBehaviour
{
    private bool gameOver = false;
    private int pinsUp = 0;
    public GameObject lockPick;
    public List<GameObject> lockPins;
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
        pinsUp = 0;
        for(int i = 0; i < lockPins.Count; i++){
            if(lockPins[i].GetComponent<LockPins>().CheckStatus()){
                pinsUp++;
            }
        }

        //Debug.Log(pinsUp);
        if (gameObject.GetComponent<Timer>().TimerStatus())
        {
            FailLevel();
        } else if (lockPick.GetComponent<LockPick>().CheckStatus()){
            //Debug.Log("ded");
            FailLevel();
        } else if(pinsUp >= lockPins.Count){
            WinLevel();
            //Debug.Log("win");
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