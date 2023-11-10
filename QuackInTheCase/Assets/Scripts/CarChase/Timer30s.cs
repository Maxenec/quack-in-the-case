using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer30s : MonoBehaviour
{
    public float timer = 30.0f;
    public TMP_Text timerUI;
    public List<GameObject> disableOnTimer;
    public List<GameObject> disableAfterTimer;
    public List<GameObject> enable;
    public GameObject manager;
    public bool winAfter;

    void Start()
    {
        if (timerUI != null)
        {
            timerUI.text = timer.ToString();
            StartTimer((int)timer);
        }
    }

    public void StartTimer(int timerLength)
    {
        timer = timerLength;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timer -= 1.0f;
            //Debug.Log("Time left: " + ((int)timer));
            timerUI.text = timer.ToString();
        }
        for (int i = 0; i < disableOnTimer.Count; i++)
        {
            disableOnTimer[i].SetActive(false);
        }
        yield return new WaitForSeconds(1.0f);
        //Debug.Log("Time is up.");
        for(int i = 0;i<enable.Count;i++){
            enable[i].SetActive(true);
        }
        for(int i = 0;i<disableAfterTimer.Count;i++){
            disableAfterTimer[i].SetActive(false);
        }
        
        if(winAfter){
            manager.GetComponent<GameManager>().WinGame();
        }else{
            manager.GetComponent<GameManager>().LoseGame();
        }
    }
}
