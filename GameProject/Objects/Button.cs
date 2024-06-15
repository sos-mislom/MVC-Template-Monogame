using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Objects;

public class Button(Texture2D _texture, Point _point,  SoundEffect _soundEffect): IObject, IClickable
{
    private Texture2D Texture { get; set; } = _texture;
    public Rectangle Rectangle { get; set; } = new Rectangle(_point.X, _point.Y, _texture.Width, _texture.Height);
    private SoundEffect HoverButtonSound { get; set; } = _soundEffect;
    private bool isHovered = false;
    private void ClickHandler()
    {
        if (Globals.MouseState.LeftButton == ButtonState.Pressed &&
            Globals.OldMouseState.LeftButton == ButtonState.Released &&
            Rectangle.Contains(Globals.MouseState.Position))
        {
            OnClick.Invoke(true);
        }
        else
        {
            OnClick.Invoke(false);
        }
    }

    private void HoverHandler()
    {
        var wasHovered = isHovered;
        isHovered = Rectangle.Contains(Globals.MouseState.Position);

        if (isHovered && !wasHovered)
            HoverButtonSound?.Play();
    }
    public void LoadContent(ContentManager contentManager)
    {
        throw new System.NotImplementedException();
    }

    public void Update(GameTime gameTime)
    {
        ClickHandler();
        HoverHandler();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Rectangle, Color.White);
    }

    public event OnClickEventHandler? OnClick;
}