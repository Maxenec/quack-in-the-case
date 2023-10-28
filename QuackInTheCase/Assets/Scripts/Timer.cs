using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer = 10.0f;

    void Start()
    {
        
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
            Debug.Log("Time left: " + ((int)timer));
            timer -= 1.0f;
        }
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Time is up.");
    }
}
