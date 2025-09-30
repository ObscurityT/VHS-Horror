using InventorySystem;
using SaveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Settings")]
    [SerializeField] string fileName;

    [Header("Debug Settings")]
    [SerializeField] private bool ignoreSave = false;

    public bool IsNewGame { get; private set; }
    GameData gameData;
    public GameData CurrentGameData => gameData;
    List<IDataPersistence> dataPersistenceObjects;
    FileDataHandler dataHandler;

    public static DataPersistenceManager instance;

    private void Awake() // singleton
    {
        if (instance == null) { instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistencesObjects();
        if (!ignoreSave)
        {
            LoadGame();
        }
        else
        {
            Debug.Log("[DPM] Ignorando save. Iniciando cena sem dados carregados.");
            this.gameData = new GameData(); 
            IsNewGame = true;
        }
    }

    private List<IDataPersistence> FindAllDataPersistencesObjects()
    {
        // encontrando todos os gameObjects que herdam de MonoBehaviour e IDataPersistence 
        // e em seguida retornando eles como uma lista
        IEnumerable<IDataPersistence> _dataPersistenceObjects = Resources.FindObjectsOfTypeAll<MonoBehaviour>()
   .Where(mb => mb.hideFlags == HideFlags.None && mb.gameObject.scene.IsValid()) // ignora Prefabs e Assets
   .OfType<IDataPersistence>();

        var list = new List<IDataPersistence>(_dataPersistenceObjects);

        Debug.Log("[DPM] Objetos de persistência encontrados:");
        foreach (var obj in list)
        {
            Debug.Log(" - " + obj.GetType().Name);
        }

        return list;
    }

    public void NewGame(GameData newGameData)
    {
        this.gameData = newGameData;
        IsNewGame = true;
    }

    public void LoadGame()
    {
        // carregar todos os arquivos salvos
        this.gameData = dataHandler.Load();

        // se nao tiver nenhum criar um novo
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            this.gameData = new GameData();
            IsNewGame = true;
        }

        // depois enviar todos os dados carregados para os gameobjects
        foreach (IDataPersistence dataPersistence in dataPersistenceObjects) { dataPersistence.LoadData(gameData); }
    }

    public void SaveGame()
    {
        Debug.Log("[DPM] SaveGame INICIOU");

        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            Debug.Log("[DPM] Salvando: " + dataPersistence.GetType().Name);
            try
            {
                dataPersistence.SaveData(gameData);
            }
            catch (Exception ex)
            {
                Debug.LogError("[DPM] Erro ao salvar: " + dataPersistence.GetType().Name + " - " + ex);
            }
        }

        Debug.Log("[DPM] Antes de salvar arquivo");
        dataHandler.Save(gameData);
        Debug.Log("[DPM] Save FINALIZADO");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}