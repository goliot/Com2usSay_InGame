using System;
using System.Collections.Generic;

[Serializable]
public class RankingSaveData
{
    public string Nickname;
    public int Score;

    public RankingSaveData(RankingDTO dto)
    {
        Nickname = dto.Nickname;
        Score = dto.Score;
    }

    public RankingSaveData(string nickname, int score)
    {
        Nickname = nickname;
        Score = score;
    }
}

[Serializable]
public class RankingSaveDataList
{
    public List<RankingSaveData> DataList;
}