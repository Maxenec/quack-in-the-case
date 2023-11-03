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
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            UnpauseGame();
        }
    }

    public void ReloadScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Reloaded current scene " + currentScene);
        SceneManager.LoadScene(currentScene);
        if (isPaused)
        {
            UnpauseGame();
        }
    }

    public void SwitchScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("Switched scene to " + sceneToLoad);
        if (isPaused)
        {
            UnpauseGame();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        UnpauseGame();
    }

    public void SaveGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        string unlocked = scene.name;
        PlayerPrefs.SetString("Episode1", unlocked);
        string test = PlayerPrefs.GetString("Episode 1");
        Debug.Log(test);
        PlayerPrefs.DeleteAll();
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
            Time.timeScale = 0;
            Debug.Log("The game is now paused.");
        }
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        Debug.Log("The game has been resumed.");
        isPaused = false;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }
}