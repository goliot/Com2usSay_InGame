using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public int Level;
    public float SpawnInterval;
    [Range(0, 1f)] public float SpawnProbability;
}
