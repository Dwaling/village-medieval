using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseMenu : MonoBehaviour
{
    public void ReplayPlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameStates.instance.timeRemaining = GameStates.instance.timeForSearching;
        GameStates.instance.goldRemaining = GameStates.instance.numberOfGold;
        GameStates.isWinMusicAlreadyPlayed = false;
        GameStates.isGameStarted = true;
        GameStates.isGameOver = false;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
