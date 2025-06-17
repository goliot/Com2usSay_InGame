public class RankingDTO
{
    public readonly string EmailId;
    public readonly int Score;

    public RankingDTO(Ranking ranking)
    {
        EmailId = ranking.EmailId;
        Score = ranking.Score;
    }

    public RankingDTO(RankingSaveData data)
    {
        EmailId = data.EmailId;
        Score = data.Score;
    }
}