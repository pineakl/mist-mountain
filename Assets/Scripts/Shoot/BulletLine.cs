using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLine : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Hide", 0.05f);    
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
