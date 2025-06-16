using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public Action OnLevelChangeEvent;

    [SerializeField] private float _requiredTimeForNextStage;
    [SerializeField] private List<LevelDataSO> StageDataSO;

    private Stage _stage;
    public StageDTO StageDto => new StageDTO(_stage);
    private float _timer = 0f;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
        // TODO : 저장/로드
        _stage = new Stage(1);
    }

    private void Update()
    {
        CheckTimeAndManageLevel();
    }

    private void CheckTimeAndManageLevel()
    {
        _timer += Time.deltaTime;

        if(_timer >= StageDataSO[_stage.CurrentLevel - 1].SpawnInterval)
        {
            OnLevelChangeEvent?.Invoke();
            _stage.IncreaseLevel();
            _timer = 0f;
        }
    }

    public bool GetNextEnemyType(out EEnemyType type)
    {
        float value = UnityEngine.Random.value;
        if(value <= StageDataSO[_stage.CurrentLevel - 1].SpawnProbability)
        {
            type = EEnemyType.Hover; //TODO  랜덤 타입
            return true;
        }

        type = EEnemyType.Count;
        return false;
    }
}