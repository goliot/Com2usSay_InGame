public class RankingDTO
{
    public readonly string Id;
    public readonly int Score;

    public RankingDTO(Ranking ranking)
    {
        Id = ranking.Id;
        Score = ranking.Score;
    }

    public RankingDTO(RankingSaveData data)
    {
        Id = data.Id;
        Score = data.Score;
    }
}