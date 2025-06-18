using UnityEngine;
using System;

public class Ranking_Lecture : MonoBehaviour
{
    public int Rank { get; private set; }
    public readonly string Email;
    public readonly string Nickname;
    public int Score { get; private set; }

    public Ranking_Lecture(string email, string nickname, int score)
    {
        if(string.IsNullOrEmpty(email))
        {
            throw new Exception("이메일은 비어있을 수 없습니다.");
        }
        if(string.IsNullOrEmpty(nickname))
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
}
