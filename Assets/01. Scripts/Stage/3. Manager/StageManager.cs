using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public Action OnLevelChangeEvent;

    [SerializeField] private float _requiredTimeForNextStage = 10f;
    [SerializeField] private List<LevelDataSO> StageDataSO;
    private Dictionary<int, LevelDataSO> StageDataDict;

    private Stage _stage;
    public StageDTO StageDto => new StageDTO(_stage);

    private StageRepository _repository;

    private float _timer = 0f;
    public float Timer => _timer;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
        _repository = new StageRepository();
        StageDataDict = new Dictionary<int, LevelDataSO>(StageDataSO.Count);
        foreach(var data in StageDataSO)
        {
            if(StageDataDict.ContainsKey(data.Level))
            {
                throw new Exception($"동일 레벨의 데이터가 존재합니다. {data.Level}");
            }

            StageDataDict.Add(data.Level, data);
        }
        // TODO : 저장/로드

        StageSaveData saveData = _repository.Load();
        if(saveData != null)
        {
            _stage = new Stage(saveData.CurrentLevel);
        }
        else
        {
            _stage = new Stage(1);
        }
    }

    private void Update()
    {
        CheckTimeAndManageLevel();
    }

    private void CheckTimeAndManageLevel()
    {
        _timer += Time.deltaTime;

        if(_timer >= _requiredTimeForNextStage)
        {
            _stage.IncreaseLevel();
            OnLevelChangeEvent?.Invoke();
            _timer = 0f;
        }
    }

    public bool TryGetNextEnemyType(out EEnemyType type)
    {

        float value = UnityEngine.Random.value;
        if(value <= StageDataDict[_stage.CurrentLevel].SpawnProbability)
        {
            type = StageDataDict[_stage.CurrentLevel].GetRandomEnemy();
            return true;
        }

        type = EEnemyType.Count;
        return false;
    }

    public float GetSpawnInterval()
    {
        return StageDataDict[_stage.CurrentLevel].SpawnInterval;
    }

    public float GetStatMultiplier()
    {
        return StageDataDict[_stage.CurrentLevel].MonsterStatMultiplier;
    }
}