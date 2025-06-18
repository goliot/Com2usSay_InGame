public class RankingDTO
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly int Score;

    public RankingDTO(Ranking ranking)
    {
        Email = ranking.Email;
        Nickname = ranking.Nickname;
        Score = ranking.Score;
    }

    public RankingDTO(RankingSaveData data)
    {
        Email = data.Email;
        Nickname = data.Nickname;
        Score = data.Score;
    }
}