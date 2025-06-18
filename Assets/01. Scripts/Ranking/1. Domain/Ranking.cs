using System;

public class Ranking
{
    public readonly string Nickname;
    private int _score; // 이 유저의 총 누적 스코어
    public int Score => _score;

    public Ranking(string nickname, int score)
    {
        // 이메일 검증
        var emailSpecification = new AccountEmailSpecification();
        string errorMessage;
        if (!emailSpecification.IsSatisfiedBy(nickname))
        {
            errorMessage = emailSpecification.ErrorMessage;
            throw new Exception(errorMessage);
        }
        if(score < 0)
        {
            throw new Exception("Score는 음수가 될 수 없습니다.");
        }

        Nickname = nickname;
        _score = score;
    }

    public Ranking(RankingDTO dto) : this(dto.Nickname, dto.Score) { }

    public Ranking(RankingSaveData save) : this(save.Nickname, save.Score) { }

    public void IncreaseScore(int value)
    {
        _score += value;
    }
}