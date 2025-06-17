using System;
using System.Collections.Generic;

[Serializable]
public class RankingSaveData
{
    public string EmailId;
    public int Score;

    public RankingSaveData(RankingDTO dto)
    {
        EmailId = dto.EmailId;
        Score = dto.Score;
    }

    public RankingSaveData(string emailId, int score)
    {
        EmailId = emailId;
        Score = score;
    }
}

[Serializable]
public class RankingSaveDataList
{
    public List<RankingSaveData> DataList;
}