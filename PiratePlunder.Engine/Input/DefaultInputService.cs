using Microsoft.Xna.Framework.Input;
using PiratePlunder.Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratePlunder.Engine.Input;

internal class DefaultInputService : IInputService
{
    private KeyboardState _keyboardState;
    private MouseState _mouseState;
    private IDictionary<InputActivity, GameCommand> _subscribedCommands;
    private Keys[] downThisFrame = { }, downLastFrame = { };
    public void Initialize()
    {
        _subscribedCommands = new Dictionary<InputActivity, GameCommand>();
    }

    public void Subscribe(GameCommand command, InputActivity inputActivity)
    {
        _subscribedCommands.Add(inputActivity, command);
    }

    public void Update()
    {
        _keyboardState = Keyboard.GetState();
        _mouseState = Mouse.GetState();

        downLastFrame = downThisFrame;
        downThisFrame = _keyboardState.GetPressedKeys();

        foreach (var key in _subscribedCommands.Keys)
        {
            if (IsInputActive(key))
            {
                _subscribedCommands[key].Invoke();
            }
        }
    }

    protected bool IsInputActive(InputActivity inputActivity)
    {
        return IsKeyActive(inputActivity) || IsMouseActive(inputActivity) || IsGamepadActive(inputActivity);
    }
    protected bool IsKeyActive(InputActivity inputActivity)
    {
        if (!inputActivity.IsKeyboardInput)
        {
            return false;
        }


        switch (inputActivity.ActionType)
        {
            case InputActionType.PRESSED:
                return downThisFrame.Contains(inputActivity.KeyboardInput) 
                    && !downLastFrame.Contains(inputActivity.KeyboardInput);

            case InputActionType.RELEASED:
                return !downThisFrame.Contains(inputActivity.KeyboardInput) 
                    && downLastFrame.Contains(inputActivity.KeyboardInput);

            case InputActionType.HOLD:
                return downThisFrame.Contains(inputActivity.KeyboardInput) 
                    && downLastFrame.Contains(inputActivity.KeyboardInput);

            default: return false;
        }
    }
    protected bool IsMouseActive(InputActivity inputActivity)
    {
        if (!inputActivity.IsMouseInput)
        {
            return false;
        }

        if (inputActivity.MouseLeft)
        {
            return (inputActivity.ActionType == InputActionType.PRESSED && _mouseState.LeftButton == ButtonState.Pressed);
        }
        if (inputActivity.MouseRight)
        {
            return (inputActivity.ActionType == InputActionType.PRESSED && _mouseState.RightButton == ButtonState.Pressed);
        }

        return false;
    }
    protected bool IsGamepadActive(InputActivity inputActivity)
    {
        if (!inputActivity.IsGamepadInput)
        {
            return false;
        }
        return false;
    }
}
