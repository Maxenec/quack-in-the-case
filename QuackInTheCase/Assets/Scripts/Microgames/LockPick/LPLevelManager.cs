using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPLevelManager : MonoBehaviour
{
    private bool gameOver = false;
    private int pinsUp = 0;
    public GameObject lockPick;
    public List<GameObject> lockPins;

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
        pinsUp = 0;
        for(int i = 0; i < lockPins.Count; i++){
            if(lockPins[i].GetComponent<LockPins>().CheckStatus()){
                pinsUp++;
            }
        }

        if (gameObject.GetComponent<Timer>().TimerStatus())
        {
            StartCoroutine(FailLevel());
        } else if (lockPick.GetComponent<LockPick>().CheckStatus()){
            StartCoroutine(FailLevel());
        } else if(pinsUp >= lockPins.Count){
            WinLevel();
        }
    }

    private void WinLevel()
    {
        gameOver = true;
        gameObject.GetComponent<GameManager>().WinGame();
    }
    
    IEnumerator FailLevel()
    {
        gameOver = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<GameManager>().LoseGame();
    }
}