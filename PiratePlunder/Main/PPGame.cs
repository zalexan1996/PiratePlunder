using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PP.Engine.Core;
using PP.GameStates;
using Serilog;

namespace PiratePlunder;

public class PPGame : Game
{
    private GraphicsDeviceManager _graphics;
    public GameState ActiveGameState { get; set; }
    protected SpriteBatch SpriteBatch => this.GetSpriteBatch();
    public PPGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        ActiveGameState = new MainMenuState(this);
        ActiveGameState.StateProgressed += OnStateProgressed;
        Components.Add(ActiveGameState);
        
        Log.Logger.Verbose($"{nameof(PPGame)} constructed");
    }

    protected override void Initialize()
    {
        base.Initialize();

        Log.Logger.Verbose($"{nameof(PPGame)} initialized");
    }

    protected override void LoadContent()
    {
        Services.AddService<SpriteBatch>(new SpriteBatch(GraphicsDevice));
        
        base.LoadContent();
        Log.Logger.Verbose($"{nameof(PPGame)} content loaded");
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
        SpriteBatch.Begin(blendState: BlendState.AlphaBlend);

        base.Draw(gameTime);

        SpriteBatch.End();
    }

    protected void OnStateProgressed(GameState oldState, GameState newState)
    {
        Components.Remove(oldState);
        Components.Add(newState);
        newState.StateProgressed += OnStateProgressed;
    }
}