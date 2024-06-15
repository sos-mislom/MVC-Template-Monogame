using System.IO;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace GameProject.Objects;

public class Box(int x, int y): IObject, IClickable, IDraggable
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    [JsonIgnore] private Texture2D Texture { get; set; }
    [JsonIgnore] public Rectangle Rectangle { get; set; } = new(x, y, 0, 0);

    [JsonIgnore] private bool _isDragging; 
    [JsonIgnore] private Vector2 _dragOffset; 
    
    public event OnDragEventHandler? OnDrag;
    public event OnClickEventHandler? OnClick;
    
    private void ClickHandler()
    {
        if (Globals.MouseState.LeftButton == ButtonState.Released &&
            Globals.OldMouseState.LeftButton == ButtonState.Pressed &&
            Rectangle.Contains(Globals.MouseState.Position))
        {
            OnClick.Invoke(true);
        }
        else
        {
            OnClick.Invoke(false);
        }
    }

    private void DragHandler()
    {
        var mousePosition = Globals.MouseState.Position;

        if (Globals.MouseState.LeftButton == ButtonState.Pressed &&
            Rectangle.Contains(mousePosition) && !_isDragging)
        {
            _isDragging = true;
            _dragOffset = mousePosition.ToVector2() - new Vector2(Rectangle.X, Rectangle.Y);
        }
        else if (Globals.MouseState.LeftButton == ButtonState.Released && _isDragging)
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            var newMousePos = mousePosition.ToVector2();
            Rectangle = new Rectangle(
                (int)(newMousePos.X - _dragOffset.X),
                (int)(newMousePos.Y - _dragOffset.Y),
                Rectangle.Width, Rectangle.Height
            );

            X = Rectangle.X;
            Y = Rectangle.Y;
        }

    }
    
    public void LoadContent(ContentManager contentManager)
    {
        Texture = contentManager.Load<Texture2D>(Path.Combine("GameScene", "BoxTexture"));
        Rectangle = new(X, Y, Texture.Width, Texture.Height);
    }

    public void Update(GameTime gameTime)
    {
        DragHandler();
        ClickHandler();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Rectangle, Color.White);
    }
}