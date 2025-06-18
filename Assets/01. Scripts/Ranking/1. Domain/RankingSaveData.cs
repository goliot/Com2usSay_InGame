using System;
using System.Collections.Generic;

[Serializable]
public class RankingSaveData
{
    public string Email;
    public string Nickname;
    public int Score;

    public RankingSaveData(RankingDTO dto)
    {
        Email = dto.Email;
        Nickname = dto.Nickname;
        Score = dto.Score;
    }

    public RankingSaveData(string email, string nickname, int score)
    {
        Email = email;
        Nickname = nickname;
        Score = score;
    }
}

[Serializable]
public class RankingSaveDataList
{
    public List<RankingSaveData> DataList;
}