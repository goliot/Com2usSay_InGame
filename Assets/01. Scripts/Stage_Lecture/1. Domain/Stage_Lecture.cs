using System.Collections.Generic;
using System;

public class Stage_Lecture
{
    public int LevelNumber { get; private set; }
    public int SubLevelNumber => _currentLevel.CurrentSubLevel;
    private StageLevel_Lecture _currentLevel;
    public StageLevel_Lecture CurrentLevel => _currentLevel;
    private float _progressTime;

    public List<StageLevel_Lecture> Levels { get; private set; } = new List<StageLevel_Lecture>();

    public Stage_Lecture(int levelNumber, int subLevelNumber, float progressTime, List<StageLevelSO_Lecture> list)
    {
        if (levelNumber <= 0)
        {
            throw new Exception("올바르지 않은 레벨 넘버 입니다.");
        }
        if (progressTime <= 0)
        {
            throw new Exception("올바르지 않은 진행 시간입니다.");
        }
        if (list == null)
        {
            throw new Exception("올바르지 않은 레벨 데이터입니다.");
        }

        LevelNumber = levelNumber;
        //SubLevelNumber = subLevelNuber;
        _progressTime = progressTime;
        foreach (var item in list)
        {
            int sub = item.StartLevel;
            if(sub < subLevelNumber)
            {
                sub = item.EndLevel;
                if (subLevelNumber < sub)
                {
                    sub = subLevelNumber;
                }
            }
            
            AddLevel(new StageLevel_Lecture(item, sub));
        }

        _currentLevel = Levels[LevelNumber - 1];
    }

    private void AddLevel(StageLevel_Lecture level)
    {
        if (level == null)
        {
            throw new Exception("레벨이 null입니다.");
        }

        Levels.Add(level);
    }

    public void Progress(float dt, Action onDataChanged)
    {
        _progressTime += dt;

        if(_currentLevel.TryLevelUp(_progressTime))
        {
            _progressTime = 0f;

            if(_currentLevel.IsEndLevel())
            {
                LevelNumber += 1;
                _currentLevel = Levels[LevelNumber - 1];
            }

            onDataChanged?.Invoke();
        }
    }
}