using System;

public class Ranking
{
    public readonly string Id;
    private int _score; // 이 유저의 총 누적 스코어
    public int Score => _score;

    public Ranking(string id, int score)
    {
        // 이메일 검증
        var emailSpecification = new AccountEmailSpecification();
        string errorMessage;
        if (!emailSpecification.IsSatisfiedBy(id))
        {
            errorMessage = emailSpecification.ErrorMessage;
            throw new Exception(errorMessage);
        }
        if(score < 0)
        {
            throw new Exception("Score는 음수가 될 수 없습니다.");
        }

        Id = id;
        _score = score;
    }

    public Ranking(RankingDTO dto) : this(dto.Id, dto.Score) { }

    public Ranking(RankingSaveData save) : this(save.Id, save.Score) { }

    public void IncreaseScore(int value)
    {
        _score += value;
    }
}