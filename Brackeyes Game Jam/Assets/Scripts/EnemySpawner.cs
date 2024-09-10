using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    private Transform[] _spawnPoints;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();

        _spawnPoints = _spawnPoints.Skip(1).ToArray();

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            var randomPoint = Random.Range(0, _spawnPoints.Length);

            Instantiate(_enemy, _spawnPoints[randomPoint].position, Quaternion.identity);
        }
    }
}
