using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _cameraBoxVolume;
    
    private static GameManager _instance;

    private bool _playerIsAlive;
    private int _scores;

    private float _baseSpawnTimer = 2f;
    private int _wave = 1;

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
        InvokeRepeating("Spawn", 3f, _baseSpawnTimer);
    }

    private void Update()
    {
        int nextWave = 1 + (_scores / 1000);
        if (_wave != nextWave)
        {
            _wave = nextWave;
            CancelInvoke("Spawn");
            float spawnTime = _baseSpawnTimer - (0.5f * (_wave - 1));
            if (spawnTime < 0.5f) spawnTime = 0.5f;
            InvokeRepeating("Spawn", 0f, spawnTime);
        }
    }

    /// <Summary>
    /// Spawn enemy at random position outside player visible area
    /// </Summary>
    private void Spawn()
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

    /// <Summary>
    /// Get player transform from static game manager
    /// </Summary>
    public Transform GetPlayerTransform()
    {
        return _player.transform;
    }

    /// <Summary>
    /// Get player alive status
    /// </Summary>
    public bool IsPlayerAlive()
    {
        return _playerIsAlive;
    }

    /// <Summary>
    /// Flag player death
    /// </Summary>
    public void SetPlayerDead()
    {
        CancelInvoke("Spawn");
        _cameraBoxVolume.localPosition = Vector3.zero;
        _playerIsAlive = false;
        SaveScore();
    }

    private void SaveScore()
    {
        if (_scores > PlayerPrefs.GetInt("MidnightMoonCanyon_HighScore"))
        {
            PlayerPrefs.SetInt("MidnightMoonCanyon_HighScore", _scores);
        }
    }

    /// <Summary>
    /// Get current score
    /// </Summary>
    public int GetScores()
    {
        return _scores;
    }

    /// <Summary>
    /// Get current wave
    /// </Summary>
    public int GetWave()
    {
        return _wave;
    }

    /// <Summary>
    /// Add current score by "score" value
    /// </Summary>
    public void AddScore(int score)
    {
        _scores += score;
    }
}
