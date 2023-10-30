using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer30s : MonoBehaviour
{
    private float timer = 30.0f;
    public TMP_Text timerUI;
    public List<GameObject> disable;
    public List<GameObject> enable;
    private bool pause = false;

    void Start()
    {
        timerUI.text = timer.ToString();
        StartTimer((int)timer);
    }

    public void StartTimer(int timerLength)
    {
        timer = timerLength;
        pause = false;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (timer > 0)
        {
            while (pause)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1.0f);
            Debug.Log("Time left: " + ((int)timer));
            timer -= 1.0f;
            timerUI.text = timer.ToString();
        }
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Time is up.");
        for(int i = 0;i<enable.Count;i++){
            enable[i].SetActive(true);
        }
        for(int i = 0;i<disable.Count;i++){
            disable[i].SetActive(false);
        }
    }

    public void StopTimer(){
        StopCoroutine(Countdown());
        pause = true;
        Debug.Log("stop");
    }

    public void PlayTimer(){
        StartCoroutine(Countdown());
        pause = false;
        Debug.Log("play");
    }
}
