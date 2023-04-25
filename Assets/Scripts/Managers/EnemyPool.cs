using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private int _maxInPool;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _playerTransform;

    private static EnemyPool _instance;

    private Invoker _invoker;

    private Transform[] _enemies;
    private int _alive = 0;
    private int _spawnPtr = 0;

    private void Awake()
    {
        _instance = this;
    }

    public static EnemyPool Instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        _invoker = Invoker.Instance;

        //  Create enemies then add them to the object pool
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

                    //  Create Spawn Command!
                }
                else
                {
                    _spawnPtr = (_spawnPtr + 1) % _maxInPool;
                }
            }
        }
    }

    /// <Summary>
    /// Set enemy object inactive and store it to the pool as available
    /// </Summary>
    public void DeSpawn(Transform enemyTransform)
    {
        enemyTransform.gameObject.SetActive(false);
        _alive--;
    }
}
