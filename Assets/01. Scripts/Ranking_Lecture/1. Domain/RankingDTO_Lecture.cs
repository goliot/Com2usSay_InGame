using System;

public class RankingDTO_Lecture
{
    public readonly int Rank;
    public readonly string Email;
    public readonly string Nickname;
    public readonly int Score;

    public RankingDTO_Lecture(string email, string nickname, int score)
    {
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }
        if (string.IsNullOrEmpty(nickname))
        {
            throw new Exception("닉네임은 비어있을 수 없습니다.");
        }
        if (score < 0)
        {
            throw new Exception("점수는 음수일 수 없습니다.");
        }

        Email = email;
        Nickname = nickname;
        Score = score;
    }

    public RankingDTO_Lecture(Ranking_Lecture ranking)
    {
        Rank = ranking.Rank;
        Email = ranking.Email;
        Nickname = ranking.Nickname;
        Score = ranking.Score;
    }
}