using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    private float delay = 3;
    public GameObject game;


    private void Awake()
    {
        game.SetActive(false);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart()
    {
        while (delay > 0)
        {
            yield return new WaitForSeconds(1.0f);
            delay -= 1.0f;
        }
        game.SetActive(true);
    }
}
