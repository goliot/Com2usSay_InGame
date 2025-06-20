using System;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager_Lecture : Singleton<RankingManager_Lecture>
{
    public event Action OnDataChanged;

    private RankingRepository_Lecture _repository;
    private List<Ranking_Lecture> _rankings;
    public List<RankingDTO_Lecture> Rankings => _rankings.ConvertAll(r => r.ToDTO());

    private Ranking_Lecture _myRanking;
    public RankingDTO_Lecture MyRanking => _myRanking.ToDTO();

    protected override void Awake()
    {
        base.Awake();
        OnDataChanged += Sort;
        Init();
    }

    private void Init()
    {
        _repository = new RankingRepository_Lecture();

        List<RankingSaveData_Lecture> saveDataList = _repository.Load();
        _rankings = new List<Ranking_Lecture>();
        foreach(var saveData in saveDataList)
        {
            Ranking_Lecture ranking = new Ranking_Lecture(saveData.Email, saveData.Nickname, saveData.Score);
            _rankings.Add(ranking);

            if(ranking.Email == AccountManager.Instance.UserEmail)
            {
                _myRanking = ranking;
            }
        }

        if(_myRanking == null)
        {
            _myRanking = new Ranking_Lecture(AccountManager.Instance.UserEmail, AccountManager.Instance.UserNickname, 0);
        }

        OnDataChanged?.Invoke();
    }

    private void Sort()
    {
        _rankings.Sort((a, b) => b.Score.CompareTo(a.Score));

        for(int i=0; i<_rankings.Count; ++i)
        {
            _rankings[i].SetRank(i + 1);
        }
    }
}
