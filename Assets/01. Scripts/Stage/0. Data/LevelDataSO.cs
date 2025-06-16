using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public int Level;
    public float SpawnInterval;
    [Range(0, 1f)] public float SpawnProbability;
    public float MonsterStatMultiplier;

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
            Debug.LogWarning("EnemyAppearanceDatas의 총 확률이 0 이하입니다.");
            return EEnemyType.Count; // 예외 처리: 기본값 반환
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

        // 부동소수점 문제로 인해 끝까지 도달했을 경우 대비
        return EnemyAppearanceDatas[EnemyAppearanceDatas.Count - 1].EnemyType;
    }
}