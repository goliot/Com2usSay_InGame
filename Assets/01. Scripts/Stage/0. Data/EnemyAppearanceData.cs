using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAppearanceData
{
    public EEnemyType EnemyType;
    [Range(0, 1f)] public float EnemyTypeProbability;
}