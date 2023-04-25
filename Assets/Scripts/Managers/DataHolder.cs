using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [SerializeField] private UnitData _unitDataObject;

    private int _health;
    public bool DeathFlag { get; private set; }

    private void Start()
    {
        ResetValue();
    }

    private void OnEnable()
    {
        ResetValue();
    }

    /// <Summary>
    /// Reset scriptable object Health and Death value
    /// </Summary>
    private void ResetValue()
    {
        _health = _unitDataObject.MaxHealth;
        _unitDataObject.Health = _unitDataObject.MaxHealth;
        DeathFlag = false;
        _unitDataObject.UnitTransform = transform;
        if (_unitDataObject.Mutable) _unitDataObject.UnitPosition = transform.position;
    }

    private void Update()
    {
        _unitDataObject.UnitPosition = transform.position;
    }

    /// <Summary>
    /// Decrease object health by 1
    /// </Summary>
    public void SubstractHealth()
    {
        if (_health > 0) _health--;
        if (_unitDataObject.Mutable) _unitDataObject.Health = _health;
    }

    /// <Summary>
    /// Check if the object's health is below max health
    /// </Summary>
    public bool GetDamaged()
    {
        if (_health < _unitDataObject.Health) return true;
        return false;
    }

    /// <Summary>
    /// Check if the object's health is only 1 point
    /// </Summary>
    public bool GetCritical()
    {
        if (_health > 1) return false;
        else if (_health == 1) return true;

        return false;
    }

    private void LateUpdate()
    {
        //  Check perform death procedure
        if (_health <= 0) 
        {
            if (gameObject.tag == "Player")
            {
                if (!DeathFlag)
                {
                    DeathFlag = true;
                    BeginGameOver();
                }
            }
            else if (gameObject.tag == "Enemy")
            {
                if (!DeathFlag)
                {
                    DeathFlag = true;
                    GameManager.Instance.AddScore(50);
                    Invoke("BeginDeSpawn", 0.2f);
                }
            }
        }
    }

    /// <Summary>
    /// Set enemy object inactive and store it to the pool as available
    /// </Summary>
    private void BeginDeSpawn()
    {
        EnemyPool.Instance.DeSpawn(transform);
    }

    /// <Summary>
    /// Remove controller ability
    /// </Summary>
    private void BeginGameOver()
    {
        GameManager.Instance.SetPlayerDead();
    }
}
