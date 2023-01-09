using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    public TextMeshProUGUI highScorePlayer;

    private void Start()
    {
        highScorePlayer.SetText("Best Score: " + DataManager.HighScorePlayerData.PlayerName + ": " + DataManager.HighScorePlayerData.PlayerScore);
        playerNameInputField.text = DataManager.playerData.PlayerName;
    }
    public void StartGameClicked()
    {
        DataManager.Instance.GameStarted(playerNameInputField.text);
        SceneManager.LoadScene(1);
    }
}
