using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    //background
    private Texture2D _texture;

    //ball
    private Texture2D _ball;
    private Vector2 _ballPosition;
    private Vector2 _ballDirection;
    private float _ballSpeed;

    //player
    private Texture2D _bar;
    private Vector2 _bar1Position;
    private Vector2 _bar2Position;
    private float _barSpeed;
    private SpriteFont font;

    //Score
    private Texture2D _score;
    private Vector2 _scorePosition;
    private int _pointA;
    private int _pointB;
    private Vector2 _pointAPosition;
    private Vector2 _pointBPosition;


    //random lib
    Random _random;

    //random Y
    private float GetRandomY()
    {
        return (_random.NextSingle() * 2.0f) - 1.0f;
    }


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        _random = new Random();

        //score
        _scorePosition = new Vector2((_graphics.PreferredBackBufferWidth - _score.Width) / 2f, 0);
        _pointA = 0;
        _pointB = 0;
        _pointAPosition = new Vector2(_scorePosition.X + 17.5f, 10);
        _pointBPosition = new Vector2(_pointAPosition.X + 80f, 10);

        //ball
        _ballSpeed = 5.0f;
        _ballPosition = new Vector2((_graphics.PreferredBackBufferWidth - _ball.Width) / 2.0f, (_graphics.PreferredBackBufferHeight - _ball.Height) / 2.0f);
        _ballDirection = new Vector2(1.0f, GetRandomY());
        _ballDirection.Normalize();

        //bar
        _barSpeed = 5.0f;
        _bar1Position = new Vector2(0.0f, (_graphics.PreferredBackBufferHeight - _bar.Height) / 2.0f);
        _bar2Position = new Vector2(_graphics.PreferredBackBufferWidth - _bar.Width, (_graphics.PreferredBackBufferHeight - _bar.Height) / 2.0f);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _texture = Content.Load<Texture2D>("Mesa");
        _bar = Content.Load<Texture2D>("Barra");
        _ball = Content.Load<Texture2D>("Bola");
        _score = Content.Load<Texture2D>("Score");

        font = Content.Load<SpriteFont>("File");


    }

    protected override void Update(GameTime gameTime)
    {
        //close game
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        //player1 and player2
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.W))
        {
            _bar1Position.Y -= _barSpeed;
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            _bar1Position.Y += _barSpeed;
        }
        if (keyboardState.IsKeyDown(Keys.Up))
        {
            _bar2Position.Y -= _barSpeed;
        }
        if (keyboardState.IsKeyDown(Keys.Down))
        {
            _bar2Position.Y += _barSpeed;
        }

        //vertical colision
        if (_bar1Position.Y < 0)
        {
            _bar1Position.Y = 0.0f;
        }
        else if (_bar1Position.Y > _graphics.PreferredBackBufferHeight - _bar.Height)
        {
            _bar1Position.Y = _graphics.PreferredBackBufferHeight - _bar.Height;
        }
        if (_bar2Position.Y < 0)
        {
            _bar2Position.Y = 0.0f;
        }
        else if (_bar2Position.Y > _graphics.PreferredBackBufferHeight - _bar.Height)
        {
            _bar2Position.Y = _graphics.PreferredBackBufferHeight - _bar.Height;
        }

        //ball movimentation
        _ballPosition = _ballPosition + (_ballDirection * _ballSpeed);

        //bar colision 1
        if (_ballPosition.X < _bar.Width)
        {
            if ((_ballPosition.Y + _ball.Height > _bar1Position.Y) && (_ballPosition.Y < _bar1Position.Y + _bar.Height))
            {
                _ballPosition.X = _bar.Width;
                _ballDirection = new Vector2(1.0f, GetRandomY());
                _ballDirection.Normalize();
                _ballSpeed++;
            }

        }

        //bar colision 2
        else if (_ballPosition.X + _ball.Width > _graphics.PreferredBackBufferWidth - _bar.Width)
        {
            if ((_ballPosition.Y + _ball.Height > _bar2Position.Y) && (_ballPosition.Y < _bar2Position.Y + _bar.Height))
            {
                _ballPosition.X = _graphics.PreferredBackBufferWidth - _bar.Width - _ball.Width;
                _ballDirection = new Vector2(-1.0f, GetRandomY());
                _ballDirection.Normalize();
                _ballSpeed++;

            }
        }

        //horizontal colision
        if ((_ballPosition.X + _bar.Width < 0.0f) || (_ballPosition.X > _graphics.PreferredBackBufferWidth - _bar.Width))
        {
            if ((_ballPosition.X + _bar.Width < 0.0f))
            {
                _pointB++;
                _ballSpeed = 5.0f;
            }
            else
            {
                _pointA++;
                _ballSpeed = 5.0f;
            }
            _ballPosition = new Vector2((_graphics.PreferredBackBufferWidth - _ball.Width) / 2.0f, (_graphics.PreferredBackBufferHeight - _ball.Height) / 2.0f);
        }

        //vertical ball colision
        if (_ballPosition.Y < 0)
        {
            _ballPosition.Y = 0.0f;
            _ballDirection.Y = -_ballDirection.Y;
        }
        else if (_ballPosition.Y > _graphics.PreferredBackBufferHeight - _ball.Height)
        {
            _ballPosition.Y = _graphics.PreferredBackBufferHeight - _ball.Height;
            _ballDirection.Y = -_ballDirection.Y;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_texture, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_ball, _ballPosition, Color.White);
        _spriteBatch.Draw(_bar, _bar1Position, Color.White);
        _spriteBatch.Draw(_bar, _bar2Position, Color.White);
        _spriteBatch.Draw(_score, _scorePosition, Color.White);
        _spriteBatch.DrawString(font, _pointA.ToString(), _pointAPosition, Color.Yellow);
        _spriteBatch.DrawString(font, _pointB.ToString(), _pointBPosition, Color.Yellow);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
