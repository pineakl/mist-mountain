using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private int _maxInPool;
    [SerializeField] private GameObject _enemyPrefab;

    private Transform[] _enemies;
    private int _alive = 0;
    private int _spawnPtr = 0;
    private int _total;

    private void Start()
    {
        //  Create enemes then add them to the object pool
        _enemies = new Transform[_maxInPool];
        for (int i = 0; i < _maxInPool; i++)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, Vector3.zero, _enemyPrefab.transform.rotation);
            newEnemy.SetActive(false);
            newEnemy.transform.parent = transform;
            _enemies[i] = newEnemy.transform;
        }

    }

    /// <summary>
    /// Spawn an enemy object at position (x,z).
    /// </summary>
    public void Spawn(Vector2 position)
    {
        Debug.Log("Spawn This");
        if (_alive < _maxInPool)
        {
            bool spawned = false;
            while (!spawned)
            {
                if (!_enemies[_spawnPtr].gameObject.activeSelf)
                {
                    _enemies[_spawnPtr].position = new Vector3(position.x, 0.2f, position.y);
                    _enemies[_spawnPtr].gameObject.SetActive(true);
                    _spawnPtr = (_spawnPtr + 1) % _maxInPool;
                    _alive++;
                    spawned = true;
                }
                else
                {
                    _spawnPtr = (_spawnPtr + 1) % _maxInPool;
                }
            }
        }
        else
        {
            Debug.Log("All available enemy has spawned!");
        }
    }
}