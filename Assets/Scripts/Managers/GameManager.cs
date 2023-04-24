using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    
    private static GameManager _instance;

    private bool _playerIsAlive;
    private int _scores;

    private void Awake()
    {
        _instance = this;
    }

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Start() 
    {
        _playerIsAlive = true;

        //  Start spawning enemy every x seconds.
        InvokeRepeating("spawn", 3f, 3f);
    }

    private void spawn()
    {
        Vector2 playerPosition = new Vector2(_player.position.x, _player.position.z);
        float clean = 20f;
        float radius = 5f;
        Vector2 randCircle = Random.insideUnitCircle * radius;
        Vector2 spawnPosition = new Vector2(
            _player.position.x + randCircle.x,
            _player.position.z + randCircle.y
        );
        Vector2 cleanNormal = (spawnPosition - playerPosition).normalized;
        Vector2 spawnPositionOutside = spawnPosition + (cleanNormal * clean);

        EnemyPool.Instance.Spawn(spawnPositionOutside);
    }

    public Transform GetPlayerTransform()
    {
        return _player.transform;
    }

    public bool IsPlayerAlive()
    {
        return _playerIsAlive;
    }

    public void SetPlayerDead()
    {
        _playerIsAlive = false;
    }

    public int GetScores()
    {
        return _scores;
    }

    public void AddScore(int score)
    {
        _scores += score;
    }
}
