using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private UnitData _playerUnitData;

    [SerializeField] private TextMeshProUGUI _textLives;
    [SerializeField] private TextMeshProUGUI _textScores;

    private void Update()
    {
        UpdateTextLives();
        UpdateScores();
    }

    private void UpdateTextLives()
    {
        _textLives.text = "LIVES : " + _playerUnitData.Health;
    }

    private void UpdateScores()
    {
        _textScores.text = GameManager.Instance.GetScores().ToString();
    }
}
