using System.Collections;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    private float delay = 3f;
    public GameObject game;
    public GameObject instructions;


    private void Awake()
    {
        game.SetActive(false);
        instructions.SetActive(true);
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
        instructions.SetActive(false);
        game.SetActive(true);
    }
}
