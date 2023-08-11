using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PiratePlunder.Engine.Core.Services;
using PiratePlunder.Engine.Input;

namespace PiratePlunder.Engine.Core;

public class MainGame : Game
{
    private GameState _activeState;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ServiceLocator _serviceLocator;
    private IInputService _inputService;
    public GameState ActiveState => _activeState;
    public MainGame(GameState startingState)
    {
        _graphics = new GraphicsDeviceManager(this);
        _serviceLocator.Register(_graphics);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _activeState = startingState;
    }

    protected override void Initialize()
    {
        _inputService = new DefaultInputService();
        _serviceLocator.Register(_inputService);
        _activeState.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _activeState.LoadContent();
    }

    protected override void UnloadContent()
    {
        _activeState.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _activeState.Update();
        _inputService.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
        _activeState.Draw();
    }
}