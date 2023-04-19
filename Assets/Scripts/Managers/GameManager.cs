using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private EnemyPool _enemyPool;

    private System.Random _rand;

    private void Start() 
    {
        _rand = new System.Random();

        //  Start spawning enemy every x seconds.
        InvokeRepeating("spawn", 3f, 3f);
    }

    private void Update() 
    {

    }

    private void spawn()
    {
        float halfWide = 10;
        Vector2 spawnPosition = new Vector2(
            (float)_rand.Next((int)(_player.position.x - halfWide), (int)(_player.position.x + halfWide)),
            (float)_rand.Next((int)(_player.position.y - halfWide), (int)(_player.position.y + halfWide))
        );
        _enemyPool.Spawn(spawnPosition);
    }
}
