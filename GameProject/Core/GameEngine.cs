using System;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject;

public class GameEngine : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private GameStateManager _gsm;
    private Texture2D _cursorTexture;
    public GameEngine()
    {
        _graphics = new GraphicsDeviceManager(this);

        Activated += ActivateGame;
        Deactivated += DeactivateGame;
        
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    private void ActivateGame(object sendet, EventArgs eventArgs)
    {
        Globals.IsGameActive = true;
    }
    private void DeactivateGame(object sendet, EventArgs eventArgs)
    {
        Globals.IsGameActive = false;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = Globals.ScreenW;
        _graphics.PreferredBackBufferHeight = Globals.ScreenH;
        
        _graphics.ApplyChanges();
        _gsm = new GameStateManager();
        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _cursorTexture = Content.Load<Texture2D>("CursorTexture");
        _gsm.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (!Globals.IsGameActive)
            return;

        Globals.OldMouseState = Globals.MouseState;
        Globals.MouseState = Mouse.GetState();
        
        _gsm.Update(gameTime);
        
        if (Globals.Exit)
            Exit();
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        _gsm.Draw(_spriteBatch);

        DrawCursor(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }


    private void DrawCursor(SpriteBatch spriteBatch)
    {
        var cursorPosition = new Vector2(Globals.MouseState.X, Globals.MouseState.Y);
        spriteBatch.Draw(_cursorTexture, cursorPosition, null, 
            Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
    }
}