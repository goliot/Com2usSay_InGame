using System;

public class Ranking
{
    public readonly string EmailId;
    private int _score; // 이 유저의 총 누적 스코어
    public int Score => _score;

    public Ranking(string emailId, int score)
    {
        // 이메일 검증
        var emailSpecification = new AccountEmailSpecification();
        string errorMessage;
        if (!emailSpecification.IsSatisfiedBy(emailId))
        {
            errorMessage = emailSpecification.ErrorMessage;
            throw new Exception(errorMessage);
        }
        if(score < 0)
        {
            throw new Exception("Score는 음수가 될 수 없습니다.");
        }

        EmailId = emailId;
        _score = score;
    }

    public Ranking(RankingDTO dto) : this(dto.EmailId, dto.Score) { }

    public Ranking(RankingSaveData save) : this(save.EmailId, save.Score) { }

    public void IncreaseScore(int value)
    {
        _score += value;
    }
}