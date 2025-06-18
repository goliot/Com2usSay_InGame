public class RankingDTO
{
    public readonly string Nickname;
    public readonly int Score;

    public RankingDTO(Ranking ranking)
    {
        Nickname = ranking.Nickname;
        Score = ranking.Score;
    }

    public RankingDTO(RankingSaveData data)
    {
        Nickname = data.Nickname;
        Score = data.Score;
    }
}