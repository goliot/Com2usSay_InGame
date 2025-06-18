using UnityEngine;
using System;

public class Ranking_Lecture
{
    public int Rank { get; private set; }
    public readonly string Email;
    public readonly string Nickname;
    public int Score { get; private set; }

    public Ranking_Lecture(string email, string nickname, int score)
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
        if(score < 0)
        {
            throw new Exception("점수는 음수일 수 없습니다.");
        }

        Email = email;
        Nickname = nickname;
        Score = score;
    }

    public void SetRank(int rank)
    {
        if(rank <= 0)
        {
            throw new Exception("랭킹은 음수일 수 없습니다.");
        }
        Rank = rank;
    }

    public void AddScore(int score)
    {
        if(score <= 0)
        {
            throw new Exception("점수를 뺄 수 없습니다");
        }

        Score += score;
    }

    public RankingDTO_Lecture ToDTO()
    {
        return new RankingDTO_Lecture(this);
    }
}
