using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer;
    private bool timesUp = false;
    private bool timerStopped = false;
    public TMP_Text timerUI;

    public void StartTimer(int setTime)
    {
        timer = setTime;
        timerUI.text = timer.ToString();
        StartCoroutine(Countdown());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
        timerStopped = true;
    }

    private IEnumerator Countdown()
    {
        Debug.Log("Timer is set for " + timer + " seconds.");
        while (timer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timer -= 1.0f;
            timerUI.text = timer.ToString();
            Debug.Log("Time left: " + ((int)timer));
        }
        Debug.Log("Time is up.");
        timesUp = true;
    }

    public bool TimerStatus()
    {
        return timesUp;
    }

    public bool TimerStopped()
    {
        return timerStopped;
    }
}
