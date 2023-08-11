using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PiratePlunder.Engine.Input;

public struct InputActivity 
{
    public Keys KeyboardInput { get; init; }
    public bool MouseLeft { get; init; }
    public bool MouseRight { get; set; }
    public bool MouseX { get; set; }
    public bool MouseY { get; set; }
    public InputActionType ActionType { get; init; }

    public bool IsKeyboardInput => KeyboardInput != Keys.None;
    public bool IsMouseInput => MouseLeft || MouseRight || MouseX || MouseY;
    public bool IsGamepadInput => false;
}
