using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStates : MonoBehaviour
{
    public static GameStates instance;
    private StatsData currentGameStats = new StatsData();
    public float timeForSearching = 120.0f;
    public float timeRemaining = 120.0f;
    public int numberOfGold = 5;
    public int goldRemaining = 5;
    [SerializeField]
    GameObject player;
    public static Vector3 playerPosition;
    Vector3 goldPosition;
    public GameObject goldPrefab;
    [SerializeField]
    GameObject goldSpawnPoints; 
    List<Transform> allSpawnPoints = new List<Transform>(); 
    public static List<Transform> randomSelectedGoldList = new List<Transform>();
    public static bool isWinMusicAlreadyPlayed = false;
    private bool isTimeUp = false;
    public static bool isGameOver;
    public static bool isGameStarted = false;
    public GameObject EndGamePanel;
    bool isGamePaused;
    private bool isGoldFound;
    private int score;
    [SerializeField]
    Text HUDscoretext;
    [SerializeField]
    Text endGameScoreText;
    [SerializeField]
    Text endGameTimeText;
    [SerializeField]
    Text endGameGoldText;
    [SerializeField]
    Minimap miniMap;
    [SerializeField]
    Text userRankingName;
    public GameObject background;
    public GameObject newRecordPanel;
    [SerializeField]
    public GameObject canvasMain;
    [SerializeField]
    public GameObject canvasHUD;
    List<GameObject> golds;
    static Vector3 playerStartPosition;
    static Quaternion playerStartRotation;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isGameOver = false;  
        Cursor.visible = true;
        golds = new List<GameObject>();
        timeRemaining = timeForSearching;
        goldRemaining = numberOfGold;
        playerStartPosition = player.transform.position;
        playerStartRotation = player.transform.rotation;
    }

    void Update()
    {
        playerPosition = player.transform.position;

        currentGameStats.timeInVillageInSec += (int)Mathf.Ceil(timeForSearching - timeRemaining);

        HUDscoretext.text = score.ToString();

        if (isGameStarted)
        {
            if (!isGameOver)
            {
                if (!isTimeUp && !isGamePaused)
                {
                    if (timeRemaining >=1200.0f)
                    {

                    }
                    else
                    {
                        timeRemaining -= Time.deltaTime;
                    }                
                }

                if (isGamePaused)
                {
                    player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
                }
                else
                {
                    player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
                }

                if(isGoldFound)
                {
                    int nbGoldFound = numberOfGold - goldRemaining;
                    int bonus = GetBonus();
                    score += 300 * nbGoldFound * bonus;
                    isGoldFound = false;
                }

                if (goldRemaining <= 0)
                {
                    if (!isWinMusicAlreadyPlayed)
                    {
                        AkSoundEngine.PostEvent("Stop_music_normal", gameObject);
                        AkSoundEngine.PostEvent("Play_music_win", gameObject);
                        isWinMusicAlreadyPlayed = true;
                    }

                    canvasMain.GetComponent<MainMenu>().SetIsCursorLocked(false);
                    background.gameObject.SetActive(true);
                    EndGamePanel.gameObject.SetActive(true);
                    EndGameScore();
                    background.transform.GetChild(0).gameObject.SetActive(false);
                    canvasHUD.gameObject.SetActive(false);
                    isGameStarted = false;
                    isGameOver = true;
                    ++currentGameStats.nbGameWin;
                    currentGameStats = GameGoldNb(currentGameStats);
                    currentGameStats = GameTimeChoice(currentGameStats);
                    currentGameStats.nbGoldBagAllTime += numberOfGold;
                    StatsManager.instance.UpdateStats(currentGameStats);
                    StatsManager.instance.SaveStatsData();
                    background.gameObject.SetActive(true);
                    StatsManager.instance.LoadStats(StatsManager.instance.fileName);
                    StatsManager.instance.FillStatsMenu();
                }

                if (timeRemaining <= 0)
                {
                    isTimeUp = true;
                    timeRemaining = 0;
                    
                    if (!isWinMusicAlreadyPlayed)
                    {
                        AkSoundEngine.PostEvent("Stop_music_normal", gameObject);
                        AkSoundEngine.PostEvent("Play_music_loose", gameObject);
                        isWinMusicAlreadyPlayed = true;
                    }
                    canvasMain.GetComponent<MainMenu>().SetIsCursorLocked(false);
                    background.gameObject.SetActive(true);
                    background.transform.GetChild(0).gameObject.SetActive(false);
                    EndGamePanel.gameObject.SetActive(true);
                    EndGamePanel.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
                    EndGamePanel.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
                    canvasHUD.gameObject.SetActive(false);
                    EndGameScore();
                    isGameOver = true;
                    isGamePaused = true;
                    isGameStarted = false;
                    
                    ++currentGameStats.nbGameLost;
                    currentGameStats = GameTimeChoice(currentGameStats);
                    currentGameStats = GameGoldNb(currentGameStats);
                    currentGameStats.nbGoldBagAllTime += numberOfGold - goldRemaining;
                    StatsManager.instance.UpdateStats(currentGameStats);
                    StatsManager.instance.SaveStatsData();
                    StatsManager.instance.LoadStats(StatsManager.instance.fileName);
                    StatsManager.instance.FillStatsMenu();
                }
            }
        }

        else
        {
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            canvasMain.GetComponent<MainMenu>().SetIsCursorLocked(false);
        }
    }

    public void ResetGame()
    {
        player.transform.position = playerStartPosition;
        player.transform.rotation = playerStartRotation;
        ResetCurrentGameStats();

        isGameStarted = false;
        isWinMusicAlreadyPlayed = false;
        isGameOver = false;
        isTimeUp = false;
        score = 0;
        miniMap.SetIsAlreadyInit(false);
        randomSelectedGoldList.Clear();
        timeRemaining = timeForSearching;
        goldRemaining = numberOfGold;

        if (golds.Count > 0)
        {
            int i = 0;
            while (golds.Count > i)
            {
                if(golds[i] != null)
                {
                    Destroy(golds[i].GetComponent<Destroy>().GetGoldIcon());
                    Destroy(golds[i]);
                    golds.Remove(golds[i]);
                }
                i++;
            }
            golds = new List<GameObject>();
        }
    }

    private void EndGameScore()
    {
        endGameScoreText.text = score.ToString();
        endGameTimeText.text = (Mathf.Floor(timeForSearching - timeRemaining)).ToString();
        endGameGoldText.text = (numberOfGold - goldRemaining).ToString() + "/" + numberOfGold.ToString();
    }
    
    public void GoFromEndGamePanelToNextPanel()
    { 
        if (RankingManager.instance.IsItNewRecord(score) && !isTimeUp) // Si c'est un nouveau record et que tout le temps n'est pas ecoulé 
        {
            newRecordPanel.SetActive(true); // On va au newRecord Panel 
        }
        else   // Sinon, on va au Main menu
        {
            canvasMain.transform.GetChild(1).gameObject.SetActive(true); // Active MainMenu panel   
            canvasMain.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true); // Active main menu addition
            ResetGame();
        }
    }

    public void GoldAsBeenFound()
    {
        isGoldFound = true;
    }

    private int GetBonus()
    {
        int bonus = 0;

        if (timeForSearching == 30)
        {
            bonus = 8;
        }
        else if (timeForSearching == 60)
        {
            bonus = 6;
        }
        else if (timeForSearching == 120)
        {
            bonus = 4;
        }
        else if (timeForSearching == 300)
        {
            bonus = 2;
        }
        return bonus;
    }

   public void GoldSpawn()
    {
        if(allSpawnPoints.Count != goldSpawnPoints.transform.childCount)
        {
            foreach (Transform child in goldSpawnPoints.transform)
            {
                allSpawnPoints.Add(child);
            }
        }

        for (int i = 0; i < numberOfGold; i++)
        {
            int rdnNum = Random.Range(0, (allSpawnPoints.Count - 1));
            randomSelectedGoldList.Add(allSpawnPoints[rdnNum]);
            allSpawnPoints.Remove(allSpawnPoints[rdnNum]);
        }

        foreach (var items in randomSelectedGoldList)
        {
            GameObject gold = Instantiate(goldPrefab, items.transform);
            golds.Add(gold);
        }
    }

    public StatsData GetCurrentGameStats()
    {
        return currentGameStats;
    }

    public void ResetCurrentGameStats()
    {
        currentGameStats = new StatsData();
    }

    private StatsData GameGoldNb(StatsData data)
    {
        if(numberOfGold == 1)
        {
            data.nbGame1Gold++;
        }else if(numberOfGold == 3)
        {
            data.nbGame3Gold++;
        }
        else if (numberOfGold == 5)
        {
            data.nbGame5Gold++;
        }
        else if (numberOfGold == 10)
        {
            data.nbGame10Gold++;
        }
        return data;
    }

    private StatsData GameTimeChoice(StatsData data)
    {
        if (timeForSearching == 30f)
        {
            data.nb30SecGame++;
        }
        else if (timeForSearching == 60f)
        {
            data.nb60SecGame++;
        }
        else if (timeForSearching == 120f)
        {
            data.nb120SecGame++;
        }
        else if (timeForSearching == 300f)
        {
            data.nb300SecGame++;
        }
        else if (timeForSearching == 1200f)
        {
            data.nbInfiniteGame++;
        }
        return data;
    }

    public void SetIsPaused(bool isPaused)
    {
      isGamePaused = isPaused;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public List<Transform> GetSelectedGoldList()
    {
        return randomSelectedGoldList;
    }

    public List<GameObject> GetGoldList()
    {
        return golds;
    }

    public bool GetIsGamePaused()
    {
        return isGamePaused;
    }

    public void SetIsGamePaused(bool gamePause)
    {
        isGamePaused = gamePause;
    }
}

