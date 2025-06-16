using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnDuration = 3f;
    [SerializeField] private int _maxEnemySpawnCount = 5;
    private float _timer = 0f;

    private int _enemyCount = 0;

    private void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnDuration && _enemyCount < _maxEnemySpawnCount)
        {
            var type = Random.Range(0, (int)EEnemyType.Count);
            EnemyPoolManager.Instance.GetObject(EEnemyType.Hover, transform.position, Quaternion.identity);
            _enemyCount++;
            _timer = 0f;
        }
    }

    private void Spawn(EEnemyType type)
    {
        EnemyPoolManager.Instance.GetObject(type, transform.position, Quaternion.identity);
    }
}
