using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.VisualScripting;
using UnityEngine;


public class AchievementManager : Singleton<AchievementManager>
{
    private OnAchievementDataChangedEvent _achivementDataChangedEvent = new OnAchievementDataChangedEvent();

    [SerializeField] private List<AchievementSO> _metaDatas;
    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll((value) => new AchievementDTO(value));

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Start()
    {
        EventManager.AddListener<CurrencyIncreaseEvent>(CurrencyIncrease);
        EventManager.AddListener<MonsterKillEvent>(MonsterKill);
    }

    private void Init()
    {
        _achievements = new List<Achievement>();
        foreach(var so in _metaDatas)
        {
            _achievements.Add(new Achievement(so));
        }
    }

    private void CurrencyIncrease(CurrencyIncreaseEvent evt)
    {
        if(evt.Type == ECurrencyType.Gold)
        {
            Increase(EAchievementCondition.GoldCollect, evt.Value);
        }
        else if(evt.Type == ECurrencyType.Diamond)
        {
            Increase(EAchievementCondition.DiamondCollect, evt.Value);
        }
    }

    private void MonsterKill(MonsterKillEvent evt)
    {
        if(evt.Type == EEnemyType.Hat || evt.Type == EEnemyType.Hover)
        {
            Increase(EAchievementCondition.DroneKillCount, 1);
        }
        else if(evt.Type == EEnemyType.Boss)
        {
            Increase(EAchievementCondition.BossKillCount, 1);
        }
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach(var achievement in _achievements)
        {
            if(achievement.Condition == condition)
            {
                achievement.Increase(value);
            }
        }

        EventManager.Broadcast(_achivementDataChangedEvent);
    }

    public bool TryClaimReward(AchievementDTO achievementDto)
    {
        Achievement achievement = _achievements.Find(item => item.ID == achievementDto.ID);
        
        if(achievement.TryClaimReward())
        {
            CurrencyManager.Instance.AddCurrency(achievement.RewardCurrencyType, achievement.RewardAmount);
            EventManager.Broadcast(_achivementDataChangedEvent);
            return true;
        }

        return false;
    }
}