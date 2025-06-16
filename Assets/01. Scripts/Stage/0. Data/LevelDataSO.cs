using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public int Level;
    public float SpawnInterval;
    [Range(0, 1f)] public float SpawnProbability;
    public float MonsterStatMultiplier = 1f;

    public List<EnemyAppearanceData> EnemyAppearanceDatas;

    public EEnemyType GetRandomEnemy()
    {
        float totalWeight = 0f;

        foreach (var data in EnemyAppearanceDatas)
        {
            totalWeight += data.EnemyTypeProbability;
        }

        if (totalWeight <= 0f)
        {
            throw new System.Exception("EnemyAppearanceDatas의 총 확률이 0 이하입니다.");
        }

        float randomValue = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var data in EnemyAppearanceDatas)
        {
            cumulative += data.EnemyTypeProbability;
            if (randomValue <= cumulative)
            {
                return data.EnemyType;
            }
        }

        return EnemyAppearanceDatas[EnemyAppearanceDatas.Count - 1].EnemyType;
    }
}