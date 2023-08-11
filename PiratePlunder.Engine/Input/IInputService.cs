using PiratePlunder.Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratePlunder.Engine.Input;

public interface IInputService
{
    void Initialize();
    void Update();
    void Subscribe(GameCommand command, InputActivity inputActivity);
}
