using System.Collections.Generic;
using UnityEngine;

public class ActionInvoker
{
    private Stack<ICommand> _commandList;

    public ActionInvoker()
    {
        _commandList = new Stack<ICommand>();
    }

    /// <summary>
    /// Add a command to the command history and then execute it immediately. 
    /// </summary>
    public void AddCommand(ICommand command)
    {
        //_commandList.Push(command);
        command.Execute();
    }
}
