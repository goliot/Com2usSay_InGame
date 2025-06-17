using System;
using System.Collections.Generic;

[Serializable]
public class RankingSaveData
{
    public string Id;
    public int Score;

    public RankingSaveData(RankingDTO dto)
    {
        Id = dto.Id;
        Score = dto.Score;
    }

    public RankingSaveData(string id, int score)
    {
        Id = id;
        Score = score;
    }
}

[Serializable]
public class RankingSaveDataList
{
    public List<RankingSaveData> DataList;
}