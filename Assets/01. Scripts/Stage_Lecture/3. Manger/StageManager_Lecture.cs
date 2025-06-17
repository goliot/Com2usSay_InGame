using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager_Lecture : Singleton<StageManager_Lecture>
{
    public event Action OnDataChanged;

    [SerializeField] private List<StageLevelSO_Lecture> _levelSOList;
    private Stage_Lecture _stage;

    // TODO : StageDTO
    public Stage_Lecture Stage => _stage;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
        _stage = new Stage_Lecture(1, 2, 17f, _levelSOList);
        OnDataChanged?.Invoke();
    }

    private void Update()
    {
        _stage.Progress(Time.deltaTime, OnDataChanged);
    }
}
