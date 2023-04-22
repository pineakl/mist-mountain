using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : MonoBehaviour
{
    private ActionInvoker _actionInvoker;
    private static Invoker _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public static Invoker Instance
    {
        get { return _instance; }
    }

    private void Start() 
    {
        _actionInvoker = new ActionInvoker();    
    }

    public void AddCommand(ICommand storedCommand)
    {
        _actionInvoker.AddCommand(storedCommand);
    }
}
