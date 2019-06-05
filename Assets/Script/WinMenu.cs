using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void ReplayPlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameStates.timeRemaining = GameStates.timeForSearching;
        GameStates.goldRemaining = GameStates.numberOfGold;
        GameStates.isWinMusicAlreadyPlayed = false;
        GameStates.isGameStarted = false;
        GameStates.isGameOver = false;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
