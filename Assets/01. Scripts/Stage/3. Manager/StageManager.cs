using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public Action OnLevelChangeEvent;

    [SerializeField] private float _requiredTimeForNextStage = 10f;
    public float RequiredTimeForNextStage => _requiredTimeForNextStage;
    [SerializeField] private List<LevelDataSO> StageDataSO;
    private Dictionary<int, LevelDataSO> StageDataDict;

    private Stage _stage;
    public StageDTO StageDto => new StageDTO(_stage);

    private StageRepository _repository;

    private float _timer = 0f;
    public float FullGameTimer { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Start()
    {
        
        StartCoroutine(CoFullGameTimerEvent());
    }

    private void Init()
    {
        FullGameTimer = 0f;
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
        FullGameTimer += Time.deltaTime;
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
        LevelDataSO data = GetCurrentLevelData();
        float value = UnityEngine.Random.value;
        if(value <= data.SpawnProbability)
        {
            type = data.GetRandomEnemy();
            return true;
        }

        type = EEnemyType.Count;
        return false;
    }

    public float GetSpawnInterval()
    {
        LevelDataSO data = GetCurrentLevelData();

        return data.SpawnInterval;
    }

    public float GetStatMultiplier()
    {
        LevelDataSO data = GetCurrentLevelData();

        return data.MonsterStatMultiplier;
    }

    private LevelDataSO GetCurrentLevelData()
    {
        LevelDataSO data;
        if (_stage.CurrentLevel > StageDataDict.Count)
        {
            data = StageDataDict[StageDataDict.Count];
        }
        else
        {
            data = StageDataDict[_stage.CurrentLevel];
        }

        return data;
    }

    private IEnumerator CoFullGameTimerEvent()
    {
        var waitForSecond = new WaitForSecondsRealtime(1f);

        while(true)
        {
            yield return waitForSecond;

            OnLevelChangeEvent?.Invoke();
        }
    }
}