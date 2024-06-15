using System.Collections.Generic;
using System.IO;
using System.Threading;
using GameProject.Interfaces;
using GameProject.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Scenes;

public class StartScene: IObject, IScene
{
    public string Prefix { get; set; } = "StartScene";

    private Button _exitButton;
    private Button _playButton;

    private List<Button> _buttonList = new();
    public void LoadContent(ContentManager content)
    {
        LoadButtons(content);
    }
    public void Update(GameTime gameTime)
    {
        UpdateButtons(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        DrawButtons(spriteBatch);
    }
    
    private void LoadButtons(ContentManager content)
    {

        var soundEffect = content.Load<SoundEffect>( "ButtonHoverSound");
        
        
        var exitButtonTexture = content.Load<Texture2D>(Path.Combine(Prefix, "ExitButton"));
        _exitButton = new Button(exitButtonTexture, new Point(45, Globals.ScreenH - 200), soundEffect);
        _exitButton.OnClick += (args) =>
        {
            var isPressed = args.Length > 0 && args[0] is bool pressed && pressed;
            if (!isPressed) return;

            Globals.Exit = true;
        };
        _buttonList.Add(_exitButton);
        
        
        var playButtonTexture = content.Load<Texture2D>(Path.Combine(Prefix, "PlayButton"));
        _playButton = new Button(playButtonTexture, new Point(45+playButtonTexture.Width*2+45, Globals.ScreenH - 200), soundEffect);

        _playButton.OnClick += HandlePLayButtonClick;
        _buttonList.Add(_playButton);
    }

    private void HandlePLayButtonClick(params object[] args)
    {
        var isPressed = args.Length > 0 && args[0] is bool pressed && pressed;
        if (!isPressed) return;

        Globals.CurrentScene = EnumScenes.Game;
    }

    private void UpdateButtons(GameTime gameTime)
    {
        foreach (var button in _buttonList)
            button.Update(gameTime);
        
    }

    private void DrawButtons(SpriteBatch spriteBatch)
    {
        foreach (var button in _buttonList)
            button.Draw(spriteBatch);
    } 
}