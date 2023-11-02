using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
            Debug.Log("The game is now paused.");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            UnpauseGame();
            Debug.Log("The game has been resumed.");
        }
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

    public void SaveGame(string level)
    {
        Scene scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("Episode1", scene.name);
    }

    public void RetrieveSave()
    {

    }

    public void PauseGame()
    {
        isPaused = true;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }
}