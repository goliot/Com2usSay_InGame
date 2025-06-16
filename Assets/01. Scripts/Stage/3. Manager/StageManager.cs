using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private float _requiredTimeForNextStage;
    [SerializeField] private List<LevelDataSO> StageDataSO;

    private Stage _stage;
    private float _timer = 0f;

    private void Update()
    {
        CheckTimeAndManageLevel();
    }

    private void CheckTimeAndManageLevel()
    {
        _timer += Time.deltaTime;

        if(_timer >= StageDataSO[_stage.CurrentLevel].SpawnInterval)
        {
            _stage.IncreaseLevel();
            _timer = 0f;
        }
    }
}