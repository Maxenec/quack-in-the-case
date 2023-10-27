using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene(int level)
    {
        SceneManager.LoadScene(level);
        Debug.Log("Switched to level "+ level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}