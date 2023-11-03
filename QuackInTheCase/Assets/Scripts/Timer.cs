using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer;
    private bool timesUp = false;
    private bool timerDisabled = false;

    public void StartTimer(int setTime)
    {
        timer = setTime;
        StartCoroutine(Countdown());
    }

    public void StopTimer()
    {
        timerDisabled = true;
    }

    private IEnumerator Countdown()
    {
        Debug.Log("Timer is set for " + timer + " seconds.");
        while (timer > 0 && timerDisabled == false)
        {
            yield return new WaitForSeconds(1.0f);
            timer -= 1.0f;
            Debug.Log("Time left: " + ((int)timer));
        }
        if (timerDisabled == false)
        {
            yield return new WaitForSeconds(1.0f);
            Debug.Log("Time is up.");
            timesUp = true;
        }
    }

    public bool TimerStatus()
    {
        return timesUp;
    }
}
