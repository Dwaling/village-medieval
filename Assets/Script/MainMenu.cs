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

    public GameObject inputField;

    private int[] goldNum = { 1, 3, 5, 10 };
    string[] gameDuration = { "30 sec", "60 sec", "2 min", "5 min", "illimité" };
    float[] gameDurationValue = { 30.0f, 60.0f, 120.0f, 300.0f, 1200.0f };

    void Start()
    {
        CanvasObject.SetActive(false);
        //CanvasObject.enabled = CanvasObject.enabled;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
        GameObject.Find("numGoldSelector").GetComponent<Text>().text = GameStates.numberOfGold.ToString();
        GameObject.Find("gameDurationSelector").GetComponent<Text>().text = gameDuration[2];
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameStates.isGameStarted)
            {
               // CanvasObject.SetActive(!CanvasObject.activeSelf);
               

                if (!isCursorLocked)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = (false);
                    isCursorLocked = (true);
                    GameObject.Find("GameState").GetComponent<GameStates>().isGamePaused = false;
                    CanvasObject.SetActive(false);

                }
                else if (isCursorLocked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = (true);
                    isCursorLocked = (false);
                    GameObject.Find("GameState").GetComponent<GameStates>().isGamePaused = true;
                    CanvasObject.SetActive(true);
                }
            }
        }
    }

    public void ShowHideMenu()
    {
        CanvasObject.SetActive(!CanvasObject.activeSelf);

        if (!isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);
            isCursorLocked = (true);
        }
        else if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);
            isCursorLocked = (false);
        }
    }

    public void SetIsGameStarted(bool isGameStarted)
    {
    
        if(!isGameStarted)
        {
            GameStates.isGameStarted = false;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameStates.instance.ResetCurrentGameStats();
            GameStates.goldRemaining = GameStates.numberOfGold;
            GameStates.isWinMusicAlreadyPlayed = false;
            GameStates.isGameOver = false;
            GameStates.timeRemaining = GameStates.timeForSearching;
        }

        if (isGameStarted)
        {
            GameStates.isGameStarted = true;
        }
       

    }

    public void PlayGame()
    {
        GameObject.Find("GameState").GetComponent<GameStates>().GoldSpawn();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //ShowHideMenu();
        isCursorLocked = (true);
        
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
        int currentNumberOfGold = GameStates.numberOfGold;
        int index = System.Array.IndexOf(goldNum, currentNumberOfGold);

        index++;

        index = index % 4;

        GameStates.numberOfGold = goldNum[index];
        GameStates.goldRemaining = goldNum[index];

        GameObject.Find("numGoldSelector").GetComponent<Text>().text = GameStates.numberOfGold.ToString();
    }


    public void DecreaseNumberOfGold()
    {
        int currentNumberOfGold = GameStates.numberOfGold;
        int index = System.Array.IndexOf(goldNum, currentNumberOfGold);

        index--;

        if(index < 0)
        {

            index = goldNum.Length - 1;
            //index = 3;
        } else
        {
            // index = index % 4;
            index = index % goldNum.Length;
        }
   
        GameStates.numberOfGold = goldNum[index];
        GameStates.goldRemaining = goldNum[index];
        GameObject.Find("numGoldSelector").GetComponent<Text>().text = GameStates.numberOfGold.ToString();
    }


    public void IncreaseGameDuration()
    {
        float currentGameDuration = GameStates.timeForSearching;
        int index = System.Array.IndexOf(gameDurationValue, currentGameDuration);

        index++;

        index = index % 5;

        GameStates.timeForSearching = gameDurationValue[index];
        GameStates.timeRemaining = gameDurationValue[index];

        GameObject.Find("gameDurationSelector").GetComponent<Text>().text = gameDuration[index];
    }

    public void DecreaseGameDuration()
    {
        float currentGameDuration = GameStates.timeForSearching;
        int index = System.Array.IndexOf(gameDurationValue, currentGameDuration);

        index--;

        if (index < 0)
        {
            // index = 4;
            index = gameDurationValue.Length - 1;
        }
        else
        {
            // index = index % 5;
            index = index % gameDurationValue.Length;
        }

        GameStates.timeForSearching = gameDurationValue[index];
        GameStates.timeRemaining = gameDurationValue[index];

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

    public void ShowCorrectMenu()
    {
        if (isFromMainMenu)
        {
            
            GameObject.Find("MainMenu_panel").SetActive(true);
            GameObject.Find("MainMenuAddition").SetActive(true);
            isFromMainMenu = false;
            GameObject.Find("Option_panel").SetActive(false);
        }
        else
        {
            GameObject.Find("Pause_panel").SetActive(true);
            GameObject.Find("Option_panel").SetActive(false);
            GameObject.Find("MainMenu_panel").SetActive(false);
            GameObject.Find("MainMenuAddition").SetActive(false);
        }
    }

    public void SetIsCursorLocked(bool isLocked)
    {

       if(isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);
            isCursorLocked = true;
        }

        if (!isLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);
            isCursorLocked = false;
        }
    }

    public void SaveNewRecord()
    {
        if (GameStates.instance.IsItNewRecord())
        {
            string name = inputField.transform.GetChild(0).GetComponent<Text>().text;

            RankingManager.instance.AddNewRecord(name, GameStates.instance.GetScore());
            RankingManager.instance.SaveRankingData();
            RankingManager.instance.PopulateRanking();
            GameStates.instance.SetIsNewRecord(false);
        }

        GameStates.instance.ResetScore();
    }


}
