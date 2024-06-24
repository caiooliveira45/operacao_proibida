using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager instance { get; private set; }

    private GameData _gameData;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Data Persistance Manager in the scene.");
        instance = this;
    }

    public void NewGame()
    {
        this._gameData = new GameData();
    }
    public void LoadGame()
    {
        if (this._gameData == null)
        {
            Debug.LogError("No data was found. Initializing data to defaults.");
            NewGame();
        }
    }
    public void SaveGame()
    {

    }
}
