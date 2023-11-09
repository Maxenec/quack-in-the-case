using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;
    private bool gameOver = false;
    public GameObject pauseMenu;
    public GameObject FailMenu;
    public GameObject SuccessMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !gameOver)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && !gameOver)
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
            gameOver = false;
            UnpauseGame();
        }
    }

    public void SwitchScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("Switched scene to " + sceneToLoad);
        if (isPaused)
        {
            gameOver = false;
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
        if (isPaused)
        {
            gameOver = false;
            UnpauseGame();
        }
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

    public void LoseGame()
    {
        if (FailMenu != null)
        {
            gameOver = true;
            FailMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            Debug.Log("You have failed the game.");
        }
    }

    public void WinGame()
    {
        if (SuccessMenu != null)
        {
            gameOver = true;
            SuccessMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            Debug.Log("Congragulations, you have completed this minigame.");
        }
    }

    public void PauseGame()
    {
        if (pauseMenu != null)
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("The game is now paused.");
        }
    }

    public void UnpauseGame()
    {
        if (pauseMenu != null)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("The game has been resumed.");
        }
    }
}