using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private UnitData _playerUnitData;

    [SerializeField] private TextMeshProUGUI _textLives;
    [SerializeField] private TextMeshProUGUI _textScores;

    [SerializeField] private Transform _gameOverContainer;
    private bool _gameOver;

    private void Start()
    {
        _gameOverContainer.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateTextLives();
        UpdateScores();

        ShowGameOver();
    }

    private void UpdateTextLives()
    {
        _textLives.text = "LIVES : " + _playerUnitData.Health;
    }

    private void UpdateScores()
    {
        _textScores.text = GameManager.Instance.GetScores().ToString();
    }

    private void ShowGameOver()
    {
        if (_playerUnitData.Health <= 0)
        {
            if (!_gameOver)
            {
                _gameOver = true;
                _gameOverContainer.gameObject.SetActive(true);
            }
        }
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
