using UnityEngine;
using System;

public class StageLevel_Lecture
{
    [Header("# Game Design")]
    public readonly string Name;
    public readonly int StartLevel;
    public readonly int EndLevel;
    public float HealthFactor;
    public float DamageFactor;
    public float Duration => 60f;
    public readonly float SpawnInterval; // 스폰 주기
    public readonly float SpawnRate; // 스폰 확률

    [Header("# State")]
    public int CurrentSubLevel; // StartLevel ~ EndLevel

    public StageLevel_Lecture(string name, int startLevel, int endLevel, float healthFactor, float damageFactor, float spawnInterval, float spawnRate, int currentLevel)
    {
        if(string.IsNullOrEmpty(name))
        {
            throw new Exception("스테이지 이름이 비었습니다.");
        }
        if (startLevel < 0 || endLevel < startLevel)
        {
            throw new Exception("시작 레벨이 올바르지 않습니다.");
        }
        if (endLevel < 0 || endLevel < startLevel)
        {
            throw new Exception("마지막 레벨이 올바르지 않습니다.");
        }
        if(healthFactor < 1)
        {
            throw new Exception("체력 배율이 올바르지 않습니다.");
        }
        if (damageFactor < 1)
        {
            throw new Exception("데미지 배율이 올바르지 않습니다.");
        }
        if(spawnInterval <= 0)
        {
            throw new Exception("스폰 주기가 올바르지 않습니다.");
        }
        if(spawnRate <= 0 || 100 < spawnRate)
        {
            throw new Exception("스폰 확률이 올바르지 않습니다.");
        }
        if(currentLevel < startLevel || endLevel < currentLevel)
        {
            throw new Exception("현재 레벨이 올바르지 않습니다.");
        }

        Name = name;
        StartLevel = startLevel;
        EndLevel = endLevel;
        HealthFactor = healthFactor;
        DamageFactor = damageFactor;
        SpawnInterval = spawnInterval;
        SpawnRate = spawnRate;
        CurrentSubLevel = currentLevel;
    }

    public StageLevel_Lecture(StageLevelSO_Lecture so, int currentLevel) : this(so.Name, so.StartLevel, so.EndLevel, so.HealthFactor, so.DamageFactor, so.SpawnInterval, so.SpawnRate, currentLevel)
    {
    }

    public bool TryLevelUp(float progressTime)
    {
        if (progressTime >= Duration)
        {
            CurrentSubLevel += 1;
            return true;
        }

        return false;
    }

    public bool IsEndLevel()
    {
        return CurrentSubLevel == EndLevel;
    }
}