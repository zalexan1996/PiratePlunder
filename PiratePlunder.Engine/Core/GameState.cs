using PiratePlunder.Engine.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiratePlunder.Engine.Core;

public abstract class GameState : IGameEntity
{
    public abstract void Draw();
    public abstract void HandleInput();

    public abstract void Initialize();

    public abstract void LoadContent();

    public abstract void UnloadContent();

    public abstract void Update();
}
