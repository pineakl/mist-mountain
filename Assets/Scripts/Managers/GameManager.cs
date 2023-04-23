using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private EnemyPool _enemyPool;

    private void Start() 
    {
        //  Start spawning enemy every x seconds.
        InvokeRepeating("spawn", 3f, 3f);
    }

    private void Update() 
    {

    }

    private void spawn()
    {
        Vector2 playerPosition = new Vector2(_player.position.x, _player.position.z);
        float clean = 10f;
        float radius = 15f;
        Vector2 randCircle = Random.insideUnitCircle * radius;
        Vector2 spawnPosition = new Vector2(
            _player.position.x + randCircle.x,
            _player.position.z + randCircle.y
        );
        Vector2 cleanNormal = (spawnPosition - playerPosition).normalized;
        Vector2 spawnPositionOutside = spawnPosition + (cleanNormal * clean);

        _enemyPool.Spawn(spawnPositionOutside);
    }
}
