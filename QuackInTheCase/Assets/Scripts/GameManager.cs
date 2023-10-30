using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public TMP_Text resolutionText;

    private void Awake()
    {
        
    }

    void Start()
    {
        int width = Screen.width;
        int height = Screen.height;

        resolutionText.text = width + " by " + height;
    }

    public void SwitchScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("Switched scene to " + sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {

    }

    public void RetrieveSave()
    {

    }

}