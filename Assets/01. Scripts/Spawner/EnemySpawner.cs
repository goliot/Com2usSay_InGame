using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Threading;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    //[SerializeField] private float _spawnDuration = 3f;
    //[SerializeField] private int _maxEnemySpawnCount = 5;
    private float _timer = 0f;

    private void Update()
    {
        Spawn();
    }

    //private void Spawn()
    //{
    //    _timer += Time.deltaTime;
    //    if (_timer >= _spawnDuration && _enemyCount < _maxEnemySpawnCount)
    //    {
    //        var type = Random.Range(0, (int)EEnemyType.Count);
    //        EnemyPoolManager.Instance.GetObject(EEnemyType.Hover, transform.position, Quaternion.identity);
    //        _enemyCount++;
    //        _timer = 0f;
    //    }
    //}

    private void Spawn()
    {
        _timer += Time.deltaTime;
        if(_timer < StageManager.Instance.GetSpawnInterval())
        {
            return;
        }

        foreach (Transform point in _spawnPoints)
        {
            if (StageManager.Instance.TryGetNextEnemyType(out var type))
            {
                EnemyPoolManager.Instance.GetObject(type, point.position);
            }
        }

        _timer = 0f;
    }
}
