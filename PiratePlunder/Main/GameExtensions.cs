using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

public static class GameExtensions
{
    public static TService GetService<TService>(this Game game) where TService : class
    {
        return game.Services.GetService<TService>();
    }

    public static SpriteBatch GetSpriteBatch(this Game game)
    {
        return game.GetService<SpriteBatch>();
    }

    public static ILogger GetLogger(this Game game)
    {
        return game.GetService<ILogger>();
    }
}