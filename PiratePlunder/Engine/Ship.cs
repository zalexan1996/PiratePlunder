using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace PP.Engine;

public class Ship : DrawableGameComponent
{
    private Vector2 _location;
    private Texture2D _hull, _sail, _cannon;

    public Ship(Vector2 location, Game game) : base(game)
    {
        _location = location;
        Log.Logger.Verbose("Ship constructed at '{location}'", location);
    }

    protected override void LoadContent()
    {
        _hull = Game.Content.Load<Texture2D>("Ship parts/hullLarge (1)");
        _sail = Game.Content.Load<Texture2D>("Ship parts/sailLarge (8)");
        _cannon = Game.Content.Load<Texture2D>("Ship parts/cannon");

        base.LoadContent();
        Log.Logger.Verbose("Ship content loaded.");
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        var spriteBatch = Game.GetSpriteBatch();

        spriteBatch.Draw(_hull, _location, null, Color.White, 0.0f, new Vector2(_hull.Width / 2, _hull.Height / 2), new Vector2(1,1), SpriteEffects.None, 1);
        spriteBatch.Draw(_sail, _location, null, Color.White, 0.0f, new Vector2(_sail.Width / 2, _sail.Height / 2), new Vector2(1,1), SpriteEffects.None, 1);
    }
}