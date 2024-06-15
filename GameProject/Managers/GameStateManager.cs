using GameProject.Interfaces;
using GameProject.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Managers;

public class GameStateManager : IObject
{
    private StartScene _startScene = new();
    private GameScene _gameScene = new();
    public void LoadContent(ContentManager content)
    {
        _startScene.LoadContent(content);
        _gameScene.LoadContent(content);
    }

    public void Update(GameTime gameTime)
    {
        switch (Globals.CurrentScene)
        {
            case EnumScenes.Start:
                _startScene.Update(gameTime);
                break;
            case EnumScenes.Game:
                _gameScene.Update(gameTime);
                break;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        switch (Globals.CurrentScene)
        {
            case EnumScenes.Start:
                _startScene.Draw(spriteBatch);
                break;
            case EnumScenes.Game:
                _gameScene.Draw(spriteBatch);
                break;
        }
    }
}