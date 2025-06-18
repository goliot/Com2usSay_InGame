using System;

public class Ranking
{
    public readonly string Email;
    public readonly string Nickname;
    public int Score { get; private set; }

    public Ranking(string email, string nickname, int score)
    {
        // 이메일 검증
        var emailSpecification = new AccountEmailSpecification();
        string errorMessage;
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            errorMessage = emailSpecification.ErrorMessage;
            throw new Exception(errorMessage);
        }
        if(score < 0)
        {
            throw new Exception("Score는 음수가 될 수 없습니다.");
        }

        Email = email;
        Nickname = nickname;
        Score = score;
    }

    public Ranking(RankingDTO dto) : this(dto.Email, dto.Nickname, dto.Score) { }

    public Ranking(RankingSaveData save) : this(save.Email, save.Nickname, save.Score) { }

    public void IncreaseScore(int value)
    {
        Score += value;
    }
}