using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool isFromMainMenu;
    public GameObject CanvasObject;
    bool isCursorLocked;
    [SerializeField]
    Text inputName;
    private int[] goldNum = { 1, 3, 5, 10 };
    string[] gameDuration = { "30 sec", "60 sec", "2 min", "5 min", "illimité" };
    float[] gameDurationValue = { 30.0f, 60.0f, 120.0f, 300.0f, 1200.0f };
    public Text goldSelectorText;
    public Text timeSelectorText;

    void Start()
    {
        CanvasObject.SetActive(false);
        SetIsCursorLocked(false);
        goldSelectorText.text = GameStates.instance.numberOfGold.ToString();
        timeSelectorText.text = gameDuration[2];
    }

    void Update()
    {
        if(!GameStates.instance.GetIsGamePaused())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameStates.isGameStarted && !GameStates.isGameOver)
                {
                    if (!isCursorLocked)
                    {
                        SetIsCursorLocked(true);
                        GameStates.instance.SetIsGamePaused(false);
                        CanvasObject.SetActive(false);
                    }
                    else if (isCursorLocked)
                    {
                        SetIsCursorLocked(false);
                        GameStates.instance.SetIsGamePaused(true);
                        CanvasObject.SetActive(true);
                    }
                }
            }
        } 
    }

    public void SetIsGameStarted(bool isGameStarted)
    {
        if(!isGameStarted)
        {
            GameStates.isGameStarted = false;
            GameStates.instance.timeRemaining = GameStates.instance.timeForSearching;
            GameStates.instance.goldRemaining = GameStates.instance.numberOfGold;
        }
        else if (isGameStarted)
        {
            GameStates.isGameStarted = true;
        }
    }

    public void PlayGame()
    {
        GameStates.instance.GetComponent<GameStates>().GoldSpawn();
        SetIsCursorLocked(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }

   public void IncreaseNumberOfGold()
    {
        int currentNumberOfGold = GameStates.instance.numberOfGold;
        int index = System.Array.IndexOf(goldNum, currentNumberOfGold);
        index++;
        index = index % 4;
        GameStates.instance.numberOfGold = goldNum[index];
        GameStates.instance.goldRemaining = goldNum[index];
        GameObject.Find("numGoldSelector").GetComponent<Text>().text = GameStates.instance.numberOfGold.ToString();
    }

    public void DecreaseNumberOfGold()
    {
        int currentNumberOfGold = GameStates.instance.numberOfGold;
        int index = System.Array.IndexOf(goldNum, currentNumberOfGold);

        index--;

        if(index < 0)
        {
            index = goldNum.Length - 1;
        }
        else
        {
            index = index % goldNum.Length;
        }

        GameStates.instance.numberOfGold = goldNum[index];
        GameStates.instance.goldRemaining = goldNum[index];
        GameObject.Find("numGoldSelector").GetComponent<Text>().text = GameStates.instance.numberOfGold.ToString();
    }


    public void IncreaseGameDuration()
    {
        float currentGameDuration = GameStates.instance.timeForSearching;
        int index = System.Array.IndexOf(gameDurationValue, currentGameDuration);
        index++;
        index = index % 5;
        GameStates.instance.timeForSearching = gameDurationValue[index];
        GameStates.instance.timeRemaining = gameDurationValue[index];
        GameObject.Find("gameDurationSelector").GetComponent<Text>().text = gameDuration[index];
    }

    public void DecreaseGameDuration()
    {
        float currentGameDuration = GameStates.instance.timeForSearching;
        int index = System.Array.IndexOf(gameDurationValue, currentGameDuration);

        index--;

        if (index < 0)
        {
            index = gameDurationValue.Length - 1;
        }
        else
        {
            index = index % gameDurationValue.Length;
        }

        GameStates.instance.timeForSearching = gameDurationValue[index];
        GameStates.instance.timeRemaining = gameDurationValue[index];
        GameObject.Find("gameDurationSelector").GetComponent<Text>().text = gameDuration[index];
    }

    public void SetIsFromMainMenu(bool isMainMenu)
    {
        isFromMainMenu = isMainMenu;
    }

    public bool GetIsFromMainMenu()
    {
        return isFromMainMenu;
    }

    public void SetIsCursorLocked(bool isLocked) 
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);
            isCursorLocked = true;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);
            isCursorLocked = false;
        }
    }

    public void SaveNewRecord()
    {
        if (RankingManager.instance.IsItNewRecord(GameStates.instance.GetScore()))
        {
            RankingManager.instance.AddNewRecord(inputName.text, GameStates.instance.GetScore());
            RankingManager.instance.SaveRankingData();
            RankingManager.instance.EmptyRankingItem();
            RankingManager.instance.PopulateRanking();
        }

        GameStates.instance.ResetScore();
    }
}
