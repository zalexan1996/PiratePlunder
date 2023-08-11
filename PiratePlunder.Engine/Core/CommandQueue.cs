using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratePlunder.Engine.Core;

public class CommandQueue
{
    private Queue<GameCommand> _commands;

    public void Add(GameCommand command)
    {
        _commands.Enqueue(command);
    }

    public void Process()
    {
        while (_commands.Peek() is not null)
        {
            _commands.Dequeue().Invoke();
        }
    }
}
