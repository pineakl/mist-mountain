using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : MonoBehaviour
{
    private ActionInvoker _actionInvoker;
    private void Start() 
    {
        _actionInvoker = new ActionInvoker();    
    }

    public void AddCommand(ICommand storedCommand)
    {
        _actionInvoker.AddCommand(storedCommand);
    }
}
