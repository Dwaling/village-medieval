using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStates : MonoBehaviour
{
    public static GameStates instance;
    private StatsData currentGameStats = new StatsData();
    public static float timeForSearching = 120.0f;
    public static float timeRemaining = timeForSearching;
    public static int numberOfGold = 5;
    public static int goldRemaining = numberOfGold;
    private GameObject player;
    public static Vector3 playerPosition;
    Vector3 goldPosition;
    public GameObject goldPrefab;
    public GameObject goldSpawnPoints; 
    List<Transform> allSpawnPoints = new List<Transform>(); 
    public static List<Transform> randomSelectedGoldList = new List<Transform>();
    public static bool isWinMusicAlreadyPlayed = false;
    private bool isTimeUp = false;
    public static bool isGameOver;
    public static bool isGameStarted = false;
    public GameObject EndGamePanel;
    public bool isGamePaused;
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
    //private Text scoreText;
    public GameObject background;
    public GameObject newRecordPanel;
    [SerializeField]
    public GameObject canvasMain;
    [SerializeField]
    public GameObject canvasHUD;
    List<GameObject> golds;

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
        player = GameObject.Find("FPSController");
        Cursor.visible = true;
        golds = new List<GameObject>();
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
                    GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;   
                }
                else
                {
                    GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
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
            GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            canvasMain.GetComponent<MainMenu>().SetIsCursorLocked(false);
        }
    }

    private void EndGameScore()
    {
        endGameScoreText.text = score.ToString();
        endGameTimeText.text = (Mathf.Floor(timeForSearching - timeRemaining)).ToString() + "Sec.";
        endGameGoldText.text = (numberOfGold - goldRemaining).ToString();
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
            // TODO Reloadscene et/ou ResetGameStates
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
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
        goldSpawnPoints = GameObject.Find("goldSpawnPoints");

        foreach (Transform child in goldSpawnPoints.transform)
        {
            allSpawnPoints.Add(child);
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
           // gold.GetComponent<Destroy>().goldIcon = miniMap.CreateGoldIcon(items.transform.position);

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
}

