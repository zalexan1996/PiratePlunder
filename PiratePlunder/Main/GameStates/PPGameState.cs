using Microsoft.Xna.Framework;
using PP.Engine;
using PP.Engine.Core;

namespace PP.GameStates;

public class PPGameState : GameState
{
    public PPGameState(Game game) : base(game)
    {
        Components.Add(new Ship(new Vector2(200, 200), game));
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}