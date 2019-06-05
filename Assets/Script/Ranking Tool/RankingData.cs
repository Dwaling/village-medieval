[System.Serializable]
public class RankingData
{
    public RankingItem[] items;
}

[System.Serializable]
public class RankingItem
{
    public string name;
    public int score;
}
