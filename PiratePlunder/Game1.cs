using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PiratePlunder;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Vector2 _shipLocation = new Vector2(200, 200);
    private Texture2D _hull, _sail, _cannon;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _hull = Content.Load<Texture2D>("Ship parts/hullLarge (1)");
        _sail = Content.Load<Texture2D>("Ship parts/sailLarge (8)");
        _cannon = Content.Load<Texture2D>("Ship parts/cannon");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(blendState: BlendState.AlphaBlend);

        _spriteBatch.Draw(_hull, _shipLocation, null, Color.White, 0.0f, new Vector2(_hull.Width / 2, _hull.Height / 2), new Vector2(1,1), SpriteEffects.None, 1);
        _spriteBatch.Draw(_cannon, new Vector2(200 + 16, 200 + 24), null, Color.White, MathHelper.PiOver4, new Vector2(_cannon.Width / 2, _cannon.Height / 2), new Vector2(1,1), SpriteEffects.None, 1);
        _spriteBatch.Draw(_cannon, new Vector2(200 - 16, 200 + 24), null, Color.White, (MathHelper.PiOver4 * 3), new Vector2(_cannon.Width / 2, _cannon.Height / 2), new Vector2(1,1), SpriteEffects.None, 1);
        _spriteBatch.Draw(_sail, _shipLocation, null, Color.White, 0.0f, new Vector2(_sail.Width / 2, _sail.Height / 2), new Vector2(1,1), SpriteEffects.None, 1);

        base.Draw(gameTime);

        _spriteBatch.End();
    }
}
