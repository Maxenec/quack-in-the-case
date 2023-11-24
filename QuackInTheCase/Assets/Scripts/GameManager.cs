using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    private bool isPaused = false;
    private bool gameOver = false;
    private int unlockedFirstEpisodeGames;
    private int currentlyUnlockedFirstEpisodeGame;
    private int unlockedFirstEpisodeCutscenes;
    private int currentlyUnlockedFirstEpisodeCutscene;
    public GameObject pauseMenu;
    public GameObject FailMenu;
    public GameObject SuccessMenu;
    public GameObject dataPersistentManager;

    private void Start()
    {
        
    }

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

    public int FirstEpisode(bool isMicrogame)
    {
        int i = 0;
        if (isMicrogame)
        {
            i = unlockedFirstEpisodeGames;
        }
        else
        {
            i = unlockedFirstEpisodeCutscenes;
        }
        return i;
    }

    public void LoadData(GameData data)
    {
        this.unlockedFirstEpisodeGames = data.firstEpisodeUnlockedGames;
        this.unlockedFirstEpisodeCutscenes = data.firstEpisodeUnlockedScenes;
    }

    public void SaveData(ref GameData data)
    {
        if (currentlyUnlockedFirstEpisodeGame > unlockedFirstEpisodeGames)
        {
            data.firstEpisodeUnlockedGames = this.currentlyUnlockedFirstEpisodeGame;
        }

        if (currentlyUnlockedFirstEpisodeCutscene > unlockedFirstEpisodeCutscenes)
        {
            data.firstEpisodeUnlockedScenes = this.currentlyUnlockedFirstEpisodeCutscene;
        }
    }

    public void LevelWonRewards()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        char lastCharacter = sceneName[sceneName.Length - 1];

        Debug.Log("Current level completed: " + lastCharacter);

        currentlyUnlockedFirstEpisodeGame = (int.Parse(lastCharacter.ToString()) + 1);

        Debug.Log("Level unlocked = " + currentlyUnlockedFirstEpisodeGame);

        dataPersistentManager.GetComponent<DataPersistenceManager>().SaveGame();
    }

    public void cutsceneOver()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "E1C2-1":
                currentlyUnlockedFirstEpisodeCutscene = 1;
                return;
            case "E1C2-2":
                currentlyUnlockedFirstEpisodeCutscene = 2;
                break;
            default:
                break;
        }
        dataPersistentManager.GetComponent<DataPersistenceManager>().SaveGame();
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
            LevelWonRewards();
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