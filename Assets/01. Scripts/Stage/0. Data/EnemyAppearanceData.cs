using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAppearanceData
{
    public List<EEnemyType> AppearEnemyType;
    [Range(0, 1f)] public List<float> EnemyTypeProbability;
}