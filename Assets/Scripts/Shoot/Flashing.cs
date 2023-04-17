using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Material _flashMaterial;

    private Material _defaultMaterial;

    private void Start() 
    {
        _defaultMaterial = _spriteRenderer.material;    
    }

    public void flash(float duration)
    {
        _spriteRenderer.material = _flashMaterial;
        StartCoroutine(turnOff(duration));
    }

    private IEnumerator turnOff(float duration)
    {
        yield return new WaitForSeconds(duration);
        _spriteRenderer.material = _defaultMaterial;
    }
}
