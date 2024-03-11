using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Sprites;

namespace MyGame {

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D Mesa;
    private Texture2D Bola;

    Player player;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Mesa = Content.Load<Texture2D>("Mesa");
        Bola = Content.Load<Texture2D>("Bola");

        player = new Player(Content.Load<Texture2D>("Barra"));

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(Mesa, new Rectangle(0, 0, 800, 480), Color.White);
        _spriteBatch.Draw(Bola, new Vector2(40, 40), Color.White);
        player.Draw(_spriteBatch);
        
        _spriteBatch.End();




        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}

}