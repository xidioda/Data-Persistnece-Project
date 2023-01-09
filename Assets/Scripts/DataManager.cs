using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public static PlayerData playerData = new PlayerData();

    public static PlayerData HighScorePlayerData = new PlayerData();    

    private void Awake()
    {      
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CheckFiles();
    }

    private void CheckFiles()
    {
        string highScorePath = Application.persistentDataPath + "/HighScore.json";
        string latestPath = Application.persistentDataPath + "/LatestPlayer.json";

        //If highscore exists, load player name and score from the saved file to the corresponding PlayerData class instance
        if (File.Exists(highScorePath))
        {
            string json = File.ReadAllText(highScorePath);
            HighScorePlayerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else // Else just load Nobody with 0 score
        {
            HighScorePlayerData.PlayerName = "Nobody";
            HighScorePlayerData.PlayerScore = 0;
        }

        //Check if it's not the first time the game is played, and load the details of the latest player
        if (File.Exists(latestPath))
        {
            string json = File.ReadAllText(latestPath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public string PlayerName;
        public int PlayerScore;
    }

    public void GameStarted(string playerNameInput)
    {        
        playerData.PlayerName = playerNameInput;
        playerData.PlayerScore = 0;
    }

    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/"+playerData.PlayerName+".json";

        File.WriteAllText(path, json);

        if (playerData.PlayerScore > HighScorePlayerData.PlayerScore)
        {
            UpdateHighscore();
        }
        LatestPlayer();
    }

    public void UpdateHighscore()
    {
        string json = JsonUtility.ToJson(playerData);
        string highScorePath = Application.persistentDataPath + "/HighScore.json";

        File.WriteAllText(highScorePath, json);
    }

    public void LatestPlayer()
    {
        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/LatestPlayer.json";

        File.WriteAllText(path, json);
    }
}
