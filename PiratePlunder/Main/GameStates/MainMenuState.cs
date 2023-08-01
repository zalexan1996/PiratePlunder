using Microsoft.Xna.Framework;
using PP.Engine.Core;
using PP.Engine.UI.Button;

namespace PP.GameStates;

public class MainMenuState : GameState
{
    private Button newGameButton, loadGameButton, quitButton;
    public MainMenuState(Game game) : base(game)
    {
        newGameButton = new Button(game)
        {
            Text = "New Game",
            ScreenLocation = new Vector2(400, 200)
        };

        loadGameButton = new Button(game)
        {
            Text = "Load Game",
            ScreenLocation = new Vector2(400, 200 + 75)
        };

        quitButton = new Button(game)
        {
            Text = "Quit",
            ScreenLocation = new Vector2(400, 200 + 75*2)
        };

        newGameButton.OnClicked += onNewGame;
        loadGameButton.OnClicked += onLoadGame;
        quitButton.OnClicked += onQuit;

        Components.Add(newGameButton);
        Components.Add(loadGameButton);
        Components.Add(quitButton);

    }

    public override void EndState()
    {
        Components.Remove(newGameButton);
        Components.Remove(loadGameButton);
        Components.Remove(quitButton);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    private void onNewGame(Button button)
    {
        OnStateProgressed(new PPGameState(Game));
    }

    private void onLoadGame(Button button)
    {

    }

    private void onQuit(Button button)
    {
        Game.Exit();
    }
}