using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Serilog;

namespace PP.Engine.Core;

public abstract class GameState : DrawableGameComponent
{
    protected GameState(Game game) : base(game)
    {
    }

    protected ContentManager Content => Game.Content;
    protected GameServiceContainer Services => Game.Services;
    protected GameComponentCollection Components => Game.Components;

    public delegate void OnStateProgressedHandler(GameState oldState, GameState newState);
    public event OnStateProgressedHandler StateProgressed;
    protected virtual void OnStateProgressed(GameState newState)
    {
        EndState();
        StateProgressed?.Invoke(this, newState);

    }
    public virtual void BeginState()
    {

    }

    public virtual void EndState()
    {
        
    }

}