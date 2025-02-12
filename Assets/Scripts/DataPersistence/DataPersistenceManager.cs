using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "gameData.json";
    [SerializeField] private GameObject player;
    public static DataPersistenceManager instance { get; private set; }
    private List <IDataPersistence> dataPersistenceObjects;

    private GameData gameData;
    private FileDataHandler dataHandler;

    private void Awake() {
        if (instance != null) 
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 1;
        // DontDestroyOnLoad(player);
        // {
        //     GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //     foreach (GameObject p in players)
        //     if (p != player)
        //     {
        //         Destroy(p);
        //     }
        // }


        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log("Scene loaded: " + scene.name);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        // player.SetActive(true);
        Time.timeScale = 1;
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene) {
        Debug.Log("Scene unloaded: " + scene.name);
        // player.SetActive(false);
        Time.timeScale = 1;
        SaveGame();
    }

    public void NewGame(){
        this.gameData = new GameData();
    }

    public void LoadGame(){
        this.gameData = dataHandler.Load();

        if (gameData == null) 
        {
            Debug.Log("No game data found. Creating a new game.");
            NewGame();
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }

    public void SaveGame(){

        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit() {
        SaveGame();
    }
    
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();
        
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
