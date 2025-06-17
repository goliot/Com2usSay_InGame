using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class EnemySpawner_Lecture : MonoBehaviour
{
    public List<Transform> SpawnPoints;
    public GameObject EnemyPrefab;

    //TODO DTO
    private StageLevel_Lecture _stageLevel;

    private float _currentTime = 0f;

    private void Start()
    {
        Refresh();
        StageManager_Lecture.Instance.OnDataChanged += Refresh;
    }

    private void Update()
    {
        if (_stageLevel == null)
        {
            return;
        }

        _currentTime += Time.deltaTime;

        if(_currentTime >= _stageLevel.SpawnInterval)
        {
            _currentTime = 0f;

            foreach(var spawnPoint in SpawnPoints)
            {
                if(Random.Range(0, 100) < _stageLevel.SpawnRate)
                {
                    GameObject enemy = Instantiate(EnemyPrefab, spawnPoint.position, Quaternion.identity);
                    Health health = enemy.GetComponent<Health>();
                    health.MaxHealth = health.MaxHealthData * StageManager_Lecture.Instance.Stage.CurrentLevel.HealthFactor;
                }
            }
        }
    }

    private void Refresh()
    {
        _stageLevel = StageManager_Lecture.Instance.Stage.CurrentLevel;
    }
}