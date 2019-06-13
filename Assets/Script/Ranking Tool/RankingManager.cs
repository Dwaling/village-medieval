using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public static RankingManager instance;
    private RankingData rankingData;

    public GameObject scrollRect;
    public GameObject rankPrefab;
    public string fileName;
    
    private List<RankingItem> rankinglist;

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

        LoadRankingList(fileName);
        PopulateRanking();
    }

    public void LoadRankingList(string fileName)
    {
        rankinglist = new List<RankingItem>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            RankingData loadedData = JsonUtility.FromJson<RankingData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                RankingItem rankingItem = new RankingItem();
                rankingItem.name = loadedData.items[i].name;
                rankingItem.score = loadedData.items[i].score;
                rankinglist.Add(rankingItem);
            }

            Debug.Log("Data loaded, List contains: " + rankinglist.Count + " entries");

            rankingData = loadedData;
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }
    }

    public void EmptyRankingItem()
    {
        foreach(Transform child in scrollRect.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void PopulateRanking()
    {
        for(int i = 0; i < rankinglist.Count; i++)
        {
            GameObject rank = Instantiate(rankPrefab, scrollRect.transform);
            rank.transform.GetChild(0).GetComponent<Text>().text = rankinglist[i].name;
            rank.transform.GetChild(1).GetComponent<Text>().text = rankinglist[i].score.ToString();
        }
    }

    public bool IsItNewRecord(int record)
    {
        for (int i = 0; i < rankinglist.Count; i++)
        {
            if (rankinglist[i].score < record)
            {
                return true;
            }
        }
        return false;
    }

    public void AddNewRecord(string name, int score)
    {
        RankingItem newRecord = new RankingItem();
        newRecord.name = name;
        newRecord.score = score;
        bool isNewrecordAdded = false;
        for (int i = 0; i < rankinglist.Count; i++)
        {
            if (rankinglist[i].score < score && !isNewrecordAdded)
            {
                rankinglist.Insert(i, newRecord);
                isNewrecordAdded = true;
            }
        }

        if(rankinglist.Count > 20)
        {
            rankinglist.RemoveRange(20, 1);
        }
    }

    public void SaveRankingData()
    {

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        RankingItem[] rankingItems = new RankingItem[rankingData.items.Length + 1];
        for(int i = 0; i < rankingItems.Length; i++)
        {
            rankingItems[i] = rankinglist[i];
        }

        rankingData.items = rankingItems;

        if (!string.IsNullOrEmpty(fileName))
        {
            string dataAsJson = JsonUtility.ToJson(rankingData);
            File.WriteAllText(filePath, dataAsJson);
            Debug.Log(dataAsJson);
        }
    }

    public RankingData GetRankingData()
    {
        return rankingData;
    }

    public List<RankingItem> GetRankingItemsList()
    {
        return rankinglist;
    }
}
