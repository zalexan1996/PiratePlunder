using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public static class GameExtensions
{
    public static SpriteBatch GetSpriteBatch(this Game game)
    {
        return game.Services.GetService<SpriteBatch>();
    }
}