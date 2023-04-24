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

    private void ResetValue()
    {
        _health = _unitDataObject.Health;
        DeathFlag = false;
        _unitDataObject.UnitTransform = transform;
        if (_unitDataObject.Mutable) _unitDataObject.UnitPosition = transform.position;
    }

    private void Update()
    {
        _unitDataObject.UnitPosition = transform.position;
    }

    public void SubstractHealth()
    {
        if (_health > 0) _health--;
        if (_unitDataObject.Mutable) _unitDataObject.Health = _health;
    }

    public bool GetDamaged()
    {
        if (_health < _unitDataObject.Health) return true;
        return false;
    }

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
                    Invoke("BeginDeSpawn", 0.5f);
                }
            }
        }
    }

    private void BeginDeSpawn()
    {
        EnemyPool.Instance.DeSpawn(transform);
    }

    private void BeginGameOver()
    {
        GameManager.Instance.SetPlayerDead();
    }
}
