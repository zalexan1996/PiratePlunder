using PiratePlunder.Engine.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratePlunder.Engine.Core;

public abstract class GameCommand
{
    private readonly GameCommandArgs _args;

    public GameCommand(GameCommandArgs args)
    {
        _args = args;    
    }

    public abstract void Invoke();
}
