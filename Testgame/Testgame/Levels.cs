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
        Timer timer;
        Text YouLose;
        public bool isHalted { get; set; }
        public bool isPaused { get; set; }
        public LevelState myState { get; set; }
        public bool isShaking { get; set; }


        public enum LevelState
        {
            Playing,
            PlayAgain,
        }
        
        public Levels(Card[] deckOfCards, Drawable background, Texture2D selector, SpriteFont font, Player player1, List<Texture2D> particles, SoundEffect shuffling, SoundEffect playingcard, SoundEffectInstance shuffinstance, bool isSoundOn)
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
            _level = 1;
            myState = LevelState.Playing;
        }

        public void StartGame()
        {
            _player1.Reset();
            computer.Reset();
            computer.timeDelay = .5f - (.03f * _level);
            speed = new Speed(deck, _background, _selector, _font, _player1, computer, _particles, Speed.gameType.Levels, shuffling, playingCard, shuffleInstance, isSoundOn);
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
            if (speed != null) speed.Update(gameTime);
            if (timer != null) timer.Update(gameTime);
            isShaking = speed.isShaking;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (speed != null) speed.Draw(spriteBatch);
        }

    }
}
