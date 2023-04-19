using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 startPosition;
    float bulletLimitX = 10;
    void Start()
    {
        startPosition = transform.position;
    }

    private void Reset()
    {
        transform.position = startPosition;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.5f,0,0);

        if (transform.position.x > bulletLimitX)
        {
            Reset();
        }
    }
}
