using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File storage config")]

    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> DataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Data Persistence Manager in Scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.DataPersistenceObjects = FindAllDataPersistenceObjects();
        this.gameData = dataHandler.Load();
        if (SceneManager.GetActiveScene().name == "MenuScreen")
        {
            LoadGame();
        }
    }

    public void NewGame()
    {
        Debug.Log("Initializing data to defaults.");
        this.gameData = new GameData();
        SaveGame();
        LoadGame();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in DataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Save Progress shows you are on level " + gameData.firstEpisodeUnlockedGames);
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in DataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        Debug.Log("Saved level " + gameData.firstEpisodeUnlockedGames);

        dataHandler.Save(gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistencesObjects);
    }
}
