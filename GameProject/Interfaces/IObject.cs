using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

internal interface IObject
{
    internal void LoadContent(ContentManager contentManager);
    internal void Update(GameTime gameTime);
    internal void Draw(SpriteBatch spriteBatch);
}