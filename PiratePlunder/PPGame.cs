using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PP.Engine;

namespace PiratePlunder;

public class PPGame : Game
{
    private GraphicsDeviceManager _graphics;

    public PPGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Components.Add(new Ship(new Vector2(200, 200), this));
    }

    protected override void Initialize()
    {
        base.Initialize();

    }

    protected override void LoadContent()
    {
        Services.AddService<SpriteBatch>(new SpriteBatch(GraphicsDevice));
        base.LoadContent();
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
        var spriteBatch = this.GetSpriteBatch();
        spriteBatch.Begin(blendState: BlendState.AlphaBlend);

        base.Draw(gameTime);

        spriteBatch.End();
    }
}