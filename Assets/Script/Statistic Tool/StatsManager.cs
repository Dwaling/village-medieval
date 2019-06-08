using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;
    private StatsData statsData;

    public GameObject parentObject;
    public GameObject statsPrefab;
    public string fileName;

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

        DontDestroyOnLoad(gameObject);

        LoadStats(fileName);

        FillStatsMenu();
    }

    private void Start()
    {
    }

    public void LoadStats(string fileName)
    {
        statsData = new StatsData();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            StatsData loadedData = JsonUtility.FromJson<StatsData>(dataAsJson);

            statsData.nbGameWin = loadedData.nbGameWin;
            statsData.nbGameLost = loadedData.nbGameLost;
            statsData.nbSteps = loadedData.nbSteps;
            statsData.nbJumps = loadedData.nbJumps;
            statsData.timeInVillageInSec = loadedData.timeInVillageInSec;
            statsData.nb30SecGame = loadedData.nb30SecGame;
            statsData.nb60SecGame = loadedData.nb60SecGame;
            statsData.nb120SecGame = loadedData.nb120SecGame;
            statsData.nb300SecGame = loadedData.nb300SecGame;
            statsData.nbInfiniteGame = loadedData.nbInfiniteGame;
            statsData.nbGame1Gold = loadedData.nbGame1Gold;
            statsData.nbGame3Gold = loadedData.nbGame3Gold;
            statsData.nbGame5Gold = loadedData.nbGame5Gold;
            statsData.nbGame10Gold = loadedData.nbGame10Gold;
            statsData.nbGoldBagAllTime = loadedData.nbGoldBagAllTime;
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }
    }

    public void FillStatsMenu()
    {
        GameObject statsNbWin = Instantiate(statsPrefab, parentObject.transform);
        statsNbWin.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_gamewin";
        statsNbWin.transform.GetChild(1).GetComponent<Text>().text = statsData.nbGameWin.ToString();
        
        GameObject statsNbLost = Instantiate(statsPrefab, parentObject.transform);
        statsNbLost.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_gamelost";
        statsNbLost.transform.GetChild(1).GetComponent<Text>().text = statsData.nbGameLost.ToString();
        
        GameObject statsNbPlayed = Instantiate(statsPrefab, parentObject.transform);
        statsNbPlayed.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_gameplayed";
        statsNbPlayed.transform.GetChild(1).GetComponent<Text>().text = (statsData.nbGameWin + statsData.nbGameLost).ToString();

        GameObject statsNbJumps = Instantiate(statsPrefab, parentObject.transform);
        statsNbJumps.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_jumps";
        statsNbJumps.transform.GetChild(1).GetComponent<Text>().text = statsData.nbJumps.ToString();

        GameObject statsNbSteps = Instantiate(statsPrefab, parentObject.transform);
        statsNbSteps.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_steps";
        statsNbSteps.transform.GetChild(1).GetComponent<Text>().text = statsData.nbSteps.ToString();

        GameObject statsNbSecInVillage = Instantiate(statsPrefab, parentObject.transform);
        statsNbSecInVillage.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_timeinvillage";
        statsNbSecInVillage.transform.GetChild(1).GetComponent<Text>().text = statsData.timeInVillageInSec.ToString();

        GameObject statsNbGoldFoundAllTime = Instantiate(statsPrefab, parentObject.transform);
        statsNbGoldFoundAllTime.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_nbgold_alltime";
        statsNbGoldFoundAllTime.transform.GetChild(1).GetComponent<Text>().text = statsData.nbGoldBagAllTime.ToString();

        GameObject statsNbGame30Sec = Instantiate(statsPrefab, parentObject.transform);
        statsNbGame30Sec.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_30secgame";
        statsNbGame30Sec.transform.GetChild(1).GetComponent<Text>().text = statsData.nb30SecGame.ToString();

        GameObject statsNbGame60Sec = Instantiate(statsPrefab, parentObject.transform);
        statsNbGame60Sec.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_60secgame";
        statsNbGame60Sec.transform.GetChild(1).GetComponent<Text>().text = statsData.nb60SecGame.ToString();

        GameObject statsNbGame120Sec = Instantiate(statsPrefab, parentObject.transform);
        statsNbGame120Sec.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_120secgame";
        statsNbGame120Sec.transform.GetChild(1).GetComponent<Text>().text = statsData.nb120SecGame.ToString();

        GameObject statsNbGame300Sec = Instantiate(statsPrefab, parentObject.transform);
        statsNbGame300Sec.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_300secgame";
        statsNbGame300Sec.transform.GetChild(1).GetComponent<Text>().text = statsData.nb300SecGame.ToString();

        GameObject statsNbInfiniteGame = Instantiate(statsPrefab, parentObject.transform);
        statsNbInfiniteGame.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_nbinfinitegame";
        statsNbInfiniteGame.transform.GetChild(1).GetComponent<Text>().text = statsData.nbInfiniteGame.ToString();

        GameObject statsNbGame1Gold = Instantiate(statsPrefab, parentObject.transform);
        statsNbGame1Gold.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_nbgold1game";
        statsNbGame1Gold.transform.GetChild(1).GetComponent<Text>().text = statsData.nbGame1Gold.ToString();

        GameObject statsNbGame3Gold = Instantiate(statsPrefab, parentObject.transform);
        statsNbGame3Gold.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_nbgold3game";
        statsNbGame3Gold.transform.GetChild(1).GetComponent<Text>().text = statsData.nbGame3Gold.ToString();

        GameObject statsNbGame5Gold = Instantiate(statsPrefab, parentObject.transform);
        statsNbGame5Gold.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_nbgold5game";
        statsNbGame5Gold.transform.GetChild(1).GetComponent<Text>().text = statsData.nbGame5Gold.ToString();

        GameObject statsNbGame10Gold = Instantiate(statsPrefab, parentObject.transform);
        statsNbGame10Gold.transform.GetChild(0).GetComponent<LocalizedText>().key = "stats_label_nbgold10game";
        statsNbGame10Gold.transform.GetChild(1).GetComponent<Text>().text = statsData.nbGame10Gold.ToString();
    }

    public void UpdateStats(StatsData addgameStats)
    {
        statsData.nbGameLost += addgameStats.nbGameLost;
        statsData.nbGameWin += addgameStats.nbGameWin;
        statsData.nbJumps += addgameStats.nbJumps;
        statsData.nbSteps += addgameStats.nbSteps;
        statsData.timeInVillageInSec += addgameStats.timeInVillageInSec;
        statsData.nb30SecGame += addgameStats.nb30SecGame;
        statsData.nb60SecGame += addgameStats.nb60SecGame;
        statsData.nb120SecGame += addgameStats.nb120SecGame;
        statsData.nb300SecGame += addgameStats.nb300SecGame;
        statsData.nbInfiniteGame += addgameStats.nbInfiniteGame;
        statsData.nbGame1Gold += addgameStats.nbGame1Gold;
        statsData.nbGame3Gold += addgameStats.nbGame3Gold;
        statsData.nbGame5Gold += addgameStats.nbGame5Gold;
        statsData.nbGame10Gold += addgameStats.nbGame10Gold;
        statsData.nbGoldBagAllTime += addgameStats.nbGoldBagAllTime;
    }

    public void SaveStatsData()
    {
        string filePath = Application.streamingAssetsPath + "/" + fileName;

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(statsData);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    public StatsData GetStatsData()
    {
        return statsData;
    }
}
