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
    public GameObject arcadeManager;

    [SerializeField] private AudioSource clickSoundEffect;

    private void Awake()
    {
        gameOver = false;
    }

    private void Start()
    {
        DisableUI();

        if (GameObject.Find("ArcadeManager") != null)
        {
            arcadeManager = GameObject.Find("ArcadeManager");
        }

        if (SceneManager.GetActiveScene().name == "MenuScreen")
        {
            Debug.Log("Menu Screen music activated.");
            AudioManager.Instance.PlayMusic("MenuMusic");
        }
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
            Debug.Log("Saved in data.");
            data.firstEpisodeUnlockedGames = this.currentlyUnlockedFirstEpisodeGame;
        }

        if (currentlyUnlockedFirstEpisodeCutscene > unlockedFirstEpisodeCutscenes)
        {
            data.firstEpisodeUnlockedScenes = this.currentlyUnlockedFirstEpisodeCutscene;
        }
    }

    public void LevelWonRewards()
    {
        dataPersistentManager.GetComponent<DataPersistenceManager>().LoadGame();

        string sceneName = SceneManager.GetActiveScene().name;

        char lastCharacter = sceneName[sceneName.Length - 1];

        Debug.Log("Current level completed: " + lastCharacter);

        currentlyUnlockedFirstEpisodeGame = int.Parse(lastCharacter.ToString()) + 1;

        if (currentlyUnlockedFirstEpisodeGame == 6)
        {
            currentlyUnlockedFirstEpisodeCutscene = (2);
        }

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
        ButtonClick();
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
        ButtonClick();
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
        ButtonClick();
    }

    public void QuitToMenu()
    {
        if(arcadeManager != null)
        {
            Destroy(arcadeManager.gameObject);
        }
        SceneManager.LoadScene(0);
        if (isPaused)
        {
            ButtonClick();
            UnpauseGame();
        }
    }

    public void LoseGame()
    {
        if(arcadeManager != null){
            QuitToMenu();
        }
        else if (FailMenu != null)
        {
            gameOver = true;
            FailMenu.SetActive(true);
            StopBGMusic();
            AudioManager.Instance.PlaySFX("GameFail");
            Time.timeScale = 0;
            isPaused = true;
            Debug.Log("You have failed the game.");
        }
    }

    public void WinGame()
    {
        if(arcadeManager != null){
            arcadeManager.GetComponent<ArcadeMode>().ArcadeButton();
        }else if (SuccessMenu != null)
        {
            gameOver = true;
            SuccessMenu.SetActive(true);
            StopBGMusic();
            AudioManager.Instance.PlaySFX("GameWin");
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
            PauseBGMusic();
            Time.timeScale = 0;
            Debug.Log("The game is now paused.");
        }
    }

    public void UnpauseGame()
    {
        if (pauseMenu != null)
        {
            isPaused = false;
            ButtonClick();
            if (!gameOver)
            {
                ResumeBGMusic();
            }
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("The game has been resumed.");
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    private void DisableUI()
    {
        if (pauseMenu != null && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Debug.Log("Diabled Pause Menu");
        }
        if (SuccessMenu != null && SuccessMenu.activeSelf)
        {
            SuccessMenu.SetActive(false);
            Debug.Log("Diabled Pass Menu");
        }
        if (FailMenu != null && FailMenu.activeSelf)
        {
            FailMenu.SetActive(false);
            Debug.Log("Diabled Fail Menu");
        }
    }

    public void ButtonClick()
    {
        clickSoundEffect.Play();
    }

    private void StopBGMusic()
    {
        AudioManager.Instance.StopMusic();
    }

    private void PauseBGMusic()
    {
        AudioManager.Instance.PauseMusic();
    }

    private void ResumeBGMusic()
    {
        AudioManager.Instance.ResumeMusic();
    }
}