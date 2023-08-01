using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PP.Engine.UI.Button;

public class Button : DrawableGameComponent
{
    public Vector2 ScreenLocation { get; set; }
    public Texture2D ActiveTexture => ButtonTextures.ContainsKey(State) 
        ? ButtonTextures[State] : ButtonTextures[ButtonState.Normal];
    public Color ActiveShade => ButtonShades.ContainsKey(State)
        ? ButtonShades[State] : Color.White;
    public IDictionary<ButtonState, Texture2D> ButtonTextures { get; set; } = new Dictionary<ButtonState, Texture2D>();
    public IDictionary<ButtonState, Color> ButtonShades { get; set; } = new Dictionary<ButtonState, Color>();
    private SpriteFont _spriteFont;
    public ButtonState State { get; protected set; }
    public string Text { get; set; }

    public delegate void ClickedHandler(Button button);
    public event ClickedHandler OnClicked;

    public Button(Game game) : base(game)
    {
    }

    protected override void LoadContent()
    {
        _spriteFont = Game.Content.Load<SpriteFont>("Fonts/FutureFont");
        ButtonTextures[ButtonState.Normal] = Game.Content.Load<Texture2D>("UI/blue_button04");
        ButtonTextures[ButtonState.Pressed] = Game.Content.Load<Texture2D>("UI/blue_button05");
        ButtonTextures[ButtonState.Released] = Game.Content.Load<Texture2D>("UI/blue_button04");
        ButtonTextures[ButtonState.Disabled] = Game.Content.Load<Texture2D>("UI/blue_button04");

        ButtonShades[ButtonState.Normal] = Color.White;
        ButtonShades[ButtonState.Pressed] = Color.White;
        ButtonShades[ButtonState.Released] = Color.AntiqueWhite;
        ButtonShades[ButtonState.Disabled] = Color.Gray;
        ButtonShades[ButtonState.Hovered] = Color.AntiqueWhite;
    }


    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState(Game.Window);

        if (IsInBounds(mouseState.X, mouseState.Y))
        {
            if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                State = ButtonState.Pressed;
            }
            else if (State == ButtonState.Pressed)
            {
                State = ButtonState.Released;

                OnClicked?.Invoke(this);
            }
            else
            {
                State = ButtonState.Hovered;
            }
        }
        else
        {
            State = ButtonState.Normal;
        }
    }

    protected bool IsInBounds(float x, float y)
    {
        return x >= ScreenLocation.X - ActiveTexture.Width / 2 && x <= ActiveTexture.Width / 2 + ScreenLocation.X
            && y >= ScreenLocation.Y - ActiveTexture.Height / 2&& y <= ActiveTexture.Height / 2 + ScreenLocation.Y;
    }

    public override void Draw(GameTime gameTime)
    {
        Game.GetSpriteBatch().Draw(ActiveTexture, ScreenLocation, null, ActiveShade, 0.0f, new Vector2(ActiveTexture.Width / 2f, ActiveTexture.Height / 2f), Vector2.One, SpriteEffects.None, 1f);
        
        var fontSize = _spriteFont.MeasureString(Text);
        Game.GetSpriteBatch().DrawString(_spriteFont, Text, ScreenLocation, Color.White, 0.0f, new Vector2(fontSize.X / 2f, fontSize.Y / 2f), 1.0f, SpriteEffects.None, 1f);
    }
}