using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _blinkingText;
    [SerializeField] private AudioSource _source;

    private bool _click;

    private void OnEnable()
    {
        //  Start blinking text
        InvokeRepeating("ToggleBlink", 0f, 0.5f);
    }

    private void Update()
    {
        // Listen to mouse click to start game
        if (Input.GetMouseButtonDown(0))
        {
            if (!_click)
            {
                _click = true;
                _source.Play();
                CancelInvoke("ToggleBlink");
                Invoke("GotoMain", 1f);
            }
        }
    }

    /// <Summary>
    /// Toggle text blinking state
    /// </Summary>
    private void ToggleBlink()
    {
        if (_blinkingText.gameObject.activeSelf) _blinkingText.gameObject.SetActive(false);
        else _blinkingText.gameObject.SetActive(true);
    }

    /// <Summary>
    /// Load and go to MainScene immediately
    /// </Summary>
    private void GotoMain()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
