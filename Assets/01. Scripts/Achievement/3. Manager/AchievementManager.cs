using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using System;

public class AchievementManager : Singleton<AchievementManager>
{
    private AchievementRepository _repositiory;
    [SerializeField] private Transform _achievementSlotPanelTransform;
    [SerializeField] private GameObject _achievementSlot;
    [SerializeField] private List<AchievementSO> _metaDatas;
    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll((value) => new AchievementDTO(value));

    public event Action<AchievementDTO> OnNewAchievementRewarded;

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

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventManager.RemoveListener<CurrencyIncreaseEvent>(CurrencyIncrease);
        EventManager.RemoveListener<MonsterKillEvent>(MonsterKill);
    }


    private Achievement FindByID(string id)
    {
        return _achievements.Find(data => data.ID == id);
    }

    private AchievementSO FindMetaDataByID(string id)
    {
        return _metaDatas.Find(data => data.ID == id);
    }

    private void Init()
    {
        foreach (Transform child in _achievementSlotPanelTransform)
        {
            Destroy(child.gameObject);
        }

        _repositiory = new AchievementRepository();
        _achievements = new List<Achievement>();
        List<AchievementDTO> loadAchievements = _repositiory.Load()?.ConvertAll(data => new AchievementDTO(data.ID, data.CurrentValue, data.IsRewardClaimed));

        if (loadAchievements != null)
        {
            foreach(var data in loadAchievements)
            {
                AchievementSO metaData = FindMetaDataByID(data.ID);
                if(metaData != null)
                {
                    _achievements.Add(new Achievement(metaData, data));
                    Instantiate(_achievementSlot, _achievementSlotPanelTransform);
                }
            }
        }

        foreach (var so in _metaDatas)
        {
            Achievement duplicatedId = FindByID(so.ID);
            if (duplicatedId == null)
            {
                _achievements.Add(new Achievement(so));
                Instantiate(_achievementSlot, _achievementSlotPanelTransform);
            }
        }

        CleanUpInvalidAchievements();

        foreach (var item in _achievements)
        {
            Debug.Log($"업적 로드 : {item.ID}");
        }
    }

    private void CleanUpInvalidAchievements()
    {
        var toRemove = new List<Achievement>();
        foreach (var item in _achievements)
        {
            if (FindMetaDataByID(item.ID) == null)
            {
                toRemove.Add(item);
            }
        }
        foreach (var item in toRemove)
        {
            _achievements.Remove(item);
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
                bool prevCanGetReward = achievement.CanGetReward();
                achievement.Increase(value);
                bool curCanGetReward = achievement.CanGetReward();

                if(!prevCanGetReward && curCanGetReward)
                {
                     //TODO : 새로운 리워드 보상 알림
                    //OnNewAchievementRewarded?.Invoke(new AchievementDTO(achievement));
                    Events.NewAchievementRewarded.AchievementDto = new AchievementDTO(achievement);
                    EventManager.Broadcast(Events.NewAchievementRewarded);
                }
            }
        }
        _repositiory.Save(Achievements);
        EventManager.Broadcast(Events.AchievementDataChangedEvent);
    }

    public bool TryClaimReward(AchievementDTO achievementDto)
    {
        Achievement achievement = FindByID(achievementDto.ID);
        
        if(achievement.TryClaimReward())
        {
            Events.CurrencyIncreaseEvent.Type = achievement.RewardCurrencyType;
            Events.CurrencyIncreaseEvent.Value = achievement.RewardAmount;
            EventManager.Broadcast(Events.CurrencyIncreaseEvent);
            //CurrencyManager.Instance.AddCurrency(achievement.RewardCurrencyType, achievement.RewardAmount);
            _repositiory.Save(Achievements);
            EventManager.Broadcast(Events.AchievementDataChangedEvent);
            return true;
        }

        return false;
    }
}