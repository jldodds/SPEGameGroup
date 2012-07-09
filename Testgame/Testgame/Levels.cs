using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    class Levels
    {
        Speed speed;
        ComputerPlayer computer;
        int _level;
        Card[] deck;
        Drawable _background;
        Texture2D _selector;
        SpriteFont _font;
        public readonly Player _player1;
        List<Texture2D> _particles;
        SoundEffect shuffling;
        SoundEffect playingCard;
        SoundEffectInstance shuffleInstance;
        bool isSoundOn;
        bool isPowerUpOn;
        Timer timer;
        public bool isHalted { get; set; }
        public bool isPaused { get; set; }
        public LevelState myState { get; set; }
        public bool isShaking { get; set; }
        Difficulty myDiff;
        PowerUp freeze;

        public enum LevelState
        {
            Starting,
            PlayAgain,
            Playing,
        }

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }
        
        public Levels(Card[] deckOfCards, Drawable background, Texture2D selector, SpriteFont font, Player player1, List<Texture2D> particles, SoundEffect shuffling, SoundEffect playingcard, SoundEffectInstance shuffinstance, bool isSoundOn, bool powerUpsOn, Difficulty difficulty, PowerUp powerup)
        {
            computer = new ComputerPlayer("Computer", false);
            deck = deckOfCards;
            _background = background;
            _selector = selector;
            _font = font;
            _player1 = player1;
            _particles = particles;
            this.shuffling = shuffling;
            this.playingCard = playingcard;
            shuffleInstance = shuffinstance;
            this.isSoundOn = isSoundOn;
            isPowerUpOn = powerUpsOn;
            _level = 1;
            myState = LevelState.Starting;
            myDiff = difficulty;
            freeze = powerup;
        }

        public void StartGame()
        {
            _player1.Reset();
            computer.Reset();
            if (myState == LevelState.Starting) _level = 1;
            myState = LevelState.Playing;

            switch (myDiff)
            {
                case Difficulty.Easy:
                    computer.timeDelay = 2.0f - (.027f * _level);
                    break;
                case Difficulty.Medium:
                    computer.timeDelay = .6f - (.03f * _level);
                    break;
                case Difficulty.Hard:
                    computer.timeDelay = .43f - (.025f * _level);
                    break;
            }

            speed = new Speed(deck, _background, _selector, _font, _player1, computer, _particles, Speed.gameType.Levels, shuffling, playingCard, shuffleInstance, isSoundOn, isPowerUpOn, freeze);

            speed.level = _level;
            speed.TurnOn();
            myState = LevelState.Playing;
            speed.YouWon += delegate() { speed.isHalted = true; timer = new Timer(1); timer.SetTimer(0, 2, delegate() { _level++; StartGame(); }); };
            speed.YouLost += delegate() { speed.isHalted = true; timer = new Timer(1); timer.SetTimer(0, 2, delegate() { Loser(); }); };
            speed.YouTie += delegate() { speed.isHalted = true; timer = new Timer(1); timer.SetTimer(0, 2, delegate() { StartGame(); }); };
        }

        public void Loser()
        {
            myState = LevelState.PlayAgain;
        }

        public void Halt()
        {
            speed.isHalted = true;
        }

        public void Resume()
        {
            speed.isHalted = false;
        }

        public void Pause()
        {
            speed.isPaused = true;
        }

        public void Update(GameTime gameTime)
        {
            if (speed != null)
            {
                speed.Update(gameTime);
                isShaking = speed.isShaking;
            }
            if (timer != null) timer.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (speed != null) speed.Draw(spriteBatch);
        }

    }
}
