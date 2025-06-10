using System;
using UnityEngine;

[Serializable]
public class Achievement
{
    [Header("# 데이터")]
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    [Header("# 상태")]
    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _isRewardClaimed;
    public bool IsRewardClaimed => _isRewardClaimed;

    public Achievement(string id, string name, string description, EAchievementCondition condition, int goalValue, ECurrencyType rewardCurrencyType, int rewardAmount)
    {
        if(string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("ID는 비어있을 수 없습니다.");
        }
        if(string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("업적 이름은 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(description))
        {
            throw new ArgumentException("업적 설명은 비어있을 수 없습니다.");
        }
        if(goalValue <= 0)
        {
            throw new ArgumentException("업적 목표 값은 0보다 커야 합니다.");
        }
        if(rewardAmount <= 0)
        {
            throw new ArgumentException("업적 보상은 0보다 커야 합니다.");
        }

        ID = id;
        Name = name;
        Description = description;
        Condition = condition;
        GoalValue = goalValue;
        RewardCurrencyType = rewardCurrencyType;
        RewardAmount = rewardAmount;
    }

    public Achievement(AchievementSO metaData)
    {
        ID = metaData.ID;
        Name = metaData.Name;
        Description = metaData.Description;
        Condition = metaData.Condition;
        GoalValue = metaData.GoalValue;
        RewardCurrencyType = metaData.RewardCurrencyType;
        RewardAmount = metaData.RewardAmount;
    }

    public void Increase(int value)
    {
        if(value <= 0)
        {
            throw new ArgumentException("0 이하를 증가시킬 수 없습니다.");
        }

        _currentValue += value;
    }
}