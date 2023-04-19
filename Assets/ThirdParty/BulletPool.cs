using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] Transform[] balls;
    int bulletFired = 0;
    int amountOfBulletsInTotal;
    private void Start()
    {
        amountOfBulletsInTotal = transform.childCount-1;
    }

    private void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.GetChild(bulletFired).gameObject.SetActive(true);

            bulletFired++;
         
            if (bulletFired > amountOfBulletsInTotal)
            {
                bulletFired = 0;
            }
        }
    }
}
