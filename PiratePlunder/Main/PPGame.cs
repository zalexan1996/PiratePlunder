using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PiratePlunder.Engine.Core;
using PP.GameStates;
using Serilog;

namespace PiratePlunder;

public class PPGame : MainGame
{
    public PPGame(GameState startingState) : base(startingState)
    {

    }
}