using UnityEngine;
using System;

public class Stage
{
    public EStageType StageType => GetStageType(CurrentLevel);
    public int CurrentLevel { get; private set; }

    public Stage(int currentLevel)
    {
        if(currentLevel <= 0)
        {
            throw new Exception("스테이지는 0 이하일 수 없습니다.");
        }

        CurrentLevel = currentLevel;
    }

    public void IncreaseLevel()
    {
        CurrentLevel++;
    }

    public EStageType GetStageType(int stageNumber)
    {
        if (stageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(stageNumber), "스테이지 번호는 1 이상이어야 합니다.");
        }

        int index = (stageNumber - 1) / 3;
        int maxIndex = (int)EStageType.Count - 1;

        if (index > maxIndex)
        {
            index = maxIndex;
        }

        return (EStageType)index;
    }
}
