using System;
using System.Collections.Generic;
using System.IO;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Scenes;

public class GameScene: IObject, IScene
{
    private readonly string BoxSaveFilePath = Path.Combine("Content", "Saves", "Boxes.json");
    public string Prefix { get; set; } = "GameScene";

    private Random _random = new Random();
    private List<Box> _boxList;
    private SoundEffect _soundEffect;
    private Button _backButton;
    
    public void LoadContent(ContentManager contentManager)
    {
        RefreshBoxes();
        
        _soundEffect = contentManager.Load<SoundEffect>("ButtonHoverSound");
        
        var exitButtonTexture = contentManager.Load<Texture2D>(Path.Combine(Prefix, "ExitButton"));
        _backButton = new Button(exitButtonTexture, new Point(45, Globals.ScreenH - 200), null);
        _backButton.OnClick += (args) =>
        {
            var isPressed = args.Length > 0 && args[0] is bool pressed && pressed;
            if (!isPressed) return;

            Globals.CurrentScene = EnumScenes.Start;
        };
        
        foreach (var box in _boxList)
            box.LoadContent(contentManager);
    }

    private void RefreshBoxes()
    {
        var savesBoxes = JsonManager.Deserialize<List<Box>>(BoxSaveFilePath);

        if (savesBoxes != null && savesBoxes.Count > 0)
        {
            _boxList = savesBoxes;
        }
        else
        {
            _boxList = GenerateRandomBoxes(4);
            JsonManager.Serialize(BoxSaveFilePath, _boxList);
        }

        foreach (var box in _boxList)
            box.OnClick += OnBoxClick;
    }

    private List<Box> GenerateRandomBoxes(int count)
    {
        var boxes = new List<Box>();
        for (var i = 0; i < count; i++)
            boxes.Add(new Box(_random.Next(Globals.ScreenW/2), _random.Next(Globals.ScreenH/2)));
        return boxes;
    }

    private void OnBoxClick(params object[] args)
    {
        var isPressed = args.Length > 0 && args[0] is bool pressed && pressed;
        if (!isPressed) return;

        _soundEffect.Play();
        JsonManager.Serialize(BoxSaveFilePath, _boxList);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var box in _boxList)
            box.Update(gameTime);
        
        _backButton.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var box in _boxList)
            box.Draw(spriteBatch);
        
        _backButton.Draw(spriteBatch);
    }

}