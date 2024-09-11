using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    private Transform[] _spawnPoints;

    float _spawnTimer = 2f;

    float _difficultyIncreaseRate = 0.1f;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();

        _spawnPoints = _spawnPoints.Skip(1).ToArray();

        StartCoroutine(SpawnEnemies());

        InvokeRepeating("IncreaseDifficulty", 30f, 30f);
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTimer);

            var randomPoint = Random.Range(0, _spawnPoints.Length);

            Instantiate(_enemy, _spawnPoints[randomPoint].position, Quaternion.identity);
        }
    }

    void IncreaseDifficulty()
    {
        if (_spawnTimer > 0.5f) _spawnTimer -= _difficultyIncreaseRate;
    }
}
