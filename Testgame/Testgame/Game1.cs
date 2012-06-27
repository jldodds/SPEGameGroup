using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Testgame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Card[] cards;
        Drawable background;
        Texture2D selector;
        Speed speed;
        KeyboardState oldState;
        Menu MainMenu;
        Menu PlayAgain;
        Menu Pause;
        Drawable freeze;
        List<Texture2D> textures;
        ParticleEngine test;
        Random random = new Random();
        Instructions instructions;
        SoundEffect soeffect;
        SoundEffectInstance instance;
        float aspectRatio;
        Player player1;
        Player player2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set screen size to 1024x800
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1024;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // makes cursor visible when on screen
            IsMouseVisible = true;
            base.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // loads up the background
            background = new Drawable()
            {
                attributes = new Attributes()
                {
                    texture = this.Content.Load<Texture2D>("PossibleBackground"),
                    color = Color.White,
                    position = new Vector2(512, 400),
                    depth = 1,
                    height = 802,
                    width = 1026,
                    rotation = 0
                }
            };

            player1 = new HumanPlayer(Keys.Up, Keys.Down, Keys.Left, Keys.Right, "Player 1", true);
            player2 = new HumanPlayer(Keys.W, Keys.S, Keys.A, Keys.D, "Player 2", false);
            // TODO: use this.Content to load your game content here

            // loads up cards & assigns values
            #region Create cards[]
            cards = new Card[52];
            cards[0] = new Card(0, this.Content.Load<Texture2D>("AceClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[1] = new Card(1, this.Content.Load<Texture2D>("TwoClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[2] = new Card(2, this.Content.Load<Texture2D>("ThreeClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[3] = new Card(3, this.Content.Load<Texture2D>("FourClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[4] = new Card(4, this.Content.Load<Texture2D>("FiveClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[5] = new Card(5, this.Content.Load<Texture2D>("SixClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[6] = new Card(6, this.Content.Load<Texture2D>("SevenClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[7] = new Card(7, this.Content.Load<Texture2D>("EightClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[8] = new Card(8, this.Content.Load<Texture2D>("NineClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[9] = new Card(9, this.Content.Load<Texture2D>("TenClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[10] = new Card(10, this.Content.Load<Texture2D>("JackClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[11] = new Card(11, this.Content.Load<Texture2D>("QueenClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[12] = new Card(12, this.Content.Load<Texture2D>("KingClubs"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[13] = new Card(13, this.Content.Load<Texture2D>("AceDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[14] = new Card(14, this.Content.Load<Texture2D>("TwoDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[15] = new Card(15, this.Content.Load<Texture2D>("ThreeDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[16] = new Card(16, this.Content.Load<Texture2D>("FourDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[17] = new Card(17, this.Content.Load<Texture2D>("FiveDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[18] = new Card(18, this.Content.Load<Texture2D>("SixDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[19] = new Card(19, this.Content.Load<Texture2D>("SevenDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[20] = new Card(20, this.Content.Load<Texture2D>("EightDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[21] = new Card(21, this.Content.Load<Texture2D>("NineDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[22] = new Card(22, this.Content.Load<Texture2D>("TenDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[23] = new Card(23, this.Content.Load<Texture2D>("JackDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[24] = new Card(24, this.Content.Load<Texture2D>("QueenDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[25] = new Card(25, this.Content.Load<Texture2D>("KingDiamonds"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[26] = new Card(26, this.Content.Load<Texture2D>("AceHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[27] = new Card(27, this.Content.Load<Texture2D>("TwoHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[28] = new Card(28, this.Content.Load<Texture2D>("ThreeHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[29] = new Card(29, this.Content.Load<Texture2D>("FourHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[30] = new Card(30, this.Content.Load<Texture2D>("FiveHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[31] = new Card(31, this.Content.Load<Texture2D>("SixHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[32] = new Card(32, this.Content.Load<Texture2D>("SevenHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[33] = new Card(33, this.Content.Load<Texture2D>("EightHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[34] = new Card(34, this.Content.Load<Texture2D>("NineHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[35] = new Card(35, this.Content.Load<Texture2D>("TenHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[36] = new Card(36, this.Content.Load<Texture2D>("JackHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[37] = new Card(37, this.Content.Load<Texture2D>("QueenHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[38] = new Card(38, this.Content.Load<Texture2D>("KingHearts"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[39] = new Card(39, this.Content.Load<Texture2D>("AceSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[40] = new Card(40, this.Content.Load<Texture2D>("TwoSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[41] = new Card(41, this.Content.Load<Texture2D>("ThreeSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[42] = new Card(42, this.Content.Load<Texture2D>("FourSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[43] = new Card(43, this.Content.Load<Texture2D>("FiveSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[44] = new Card(44, this.Content.Load<Texture2D>("SixSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[45] = new Card(45, this.Content.Load<Texture2D>("SevenSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[46] = new Card(46, this.Content.Load<Texture2D>("EightSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[47] = new Card(47, this.Content.Load<Texture2D>("NineSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[48] = new Card(48, this.Content.Load<Texture2D>("TenSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[49] = new Card(49, this.Content.Load<Texture2D>("JackSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[50] = new Card(50, this.Content.Load<Texture2D>("QueenSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            cards[51] = new Card(51, this.Content.Load<Texture2D>("KingSpades"), this.Content.Load<Texture2D>("cardBack2.0"), new Vector2(-100, 100), true);
            #endregion Create cards[]

            // plays music
            soeffect = Content.Load<SoundEffect>("Audio\\Waves\\engine_2");
            instance = soeffect.CreateInstance();

            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            
            // loads card selector
            selector = this.Content.Load<Texture2D>("CardSelector");
            // loads BadaBoom font
            font = Content.Load<SpriteFont>("SpriteFont3");

            #region MainMenu
            // game title
            Text title = new Text("Speed!", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    rotation = -.2f,
                    depth = 0f,
                },
                scale = new Vector2(.5f, .5f)
            };

            // creates main menu buttons
            String[] mainMenuString = new String[4];
            mainMenuString[0] = "Play Game";
            mainMenuString[1] = "Instructions";
            mainMenuString[2] = "Settings";
            mainMenuString[3] = "Exit";
            Button.ClickHandler[] mainMenuAction = new Button.ClickHandler[4];
            
            // if "play game" is chosen from main menu, starts the game
            mainMenuAction[0] = delegate() 
            {
                speed = new Speed(cards, background, selector, font, player1, player2); speed.TurnOn(); MainMenu.isPaused = true; 
            };
            
            // if "instructions" chosen, displays instructions
            mainMenuAction[1] = delegate() 
            {
                instructions = new Instructions(background, font); instructions.Start(); MainMenu.isPaused = true; 
            };

            // if "settings" chosen, displays settings
            mainMenuAction[2] = delegate() { Console.WriteLine("Settings"); };
            
            //exits game
            mainMenuAction[3] = delegate() { this.Exit(); };
            
            //makes main menu and turns it on
            MainMenu = new Menu(background, 4, title, mainMenuString, mainMenuAction, font);
            MainMenu.TurnOn();
            soeffect.Play();
            #endregion

            #region PlayAgainMenu
            // title for play again menu
            Text playAgain = new Text("Play Again?", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    rotation = -.1f,
                    depth = 0f,
                },
                height = 200
            };
            
            // button names
            String[] PAButtonNames = new String[2];
            PAButtonNames[0] = "Yes";
            PAButtonNames[1] = "No";
            
            // starts new game or goes to main menu depending on choice
            Button.ClickHandler[] playAgainAction = new Button.ClickHandler[2];
            playAgainAction[0] = delegate() { speed = new Speed(cards, background, selector, font, player1, player2); speed.TurnOn(); PlayAgain.isPaused = true; };
            playAgainAction[1] = delegate() { speed.isPaused = true; PlayAgain.isPaused = true; speed.speedState = Speed.gameState.PlayingCard; MainMenu.isPaused = false; };
            
            // makes transparent background
            Drawable playAgainBackground = new Drawable()
            {
                attributes = new Attributes()
                {
                    texture = this.Content.Load<Texture2D>("PossibleBackground"),
                    color = Color.Transparent,
                    position = new Vector2(512, 400),
                    depth = 1,
                    height = 802,
                    width = 1026,
                    rotation = 0
                }
            };
            
            // makes playagain menu
            PlayAgain = new Menu(playAgainBackground, 2, playAgain, PAButtonNames, playAgainAction, font, 150);
            #endregion

            #region PauseMenu
            // creates pause menu title
            Text pause = new Text("Pause", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    rotation = -.1f,
                    depth = 0f,
                },
            };
            // makes names for each button on pause menu
            String[] pauseNames = new String[3];
            pauseNames[0] = "Resume";
            pauseNames[1] = "Instructions";
            pauseNames[2] = "Main Menu";
            // sets up an array of events based on which button is clicked
            Button.ClickHandler[] pauseActions = new Button.ClickHandler[3];
            // resumes speed if that option is chosen, goes to instructions if that's chosen, and goes to main menu if these aren't the case
            pauseActions[0] = delegate() {if(speed != null && speed.isPaused == false){
                speed.Resume();
                Pause.isPaused = true;
            }};
            pauseActions[1] = delegate() { Console.WriteLine("Instructions"); };
            pauseActions[2] = delegate() { MainMenu.isPaused = false; Pause.isPaused = true; if (speed != null) speed.isHalted = false; speed.isPaused = true; };
            // creates instance of pause menu
            Pause = new Menu(playAgainBackground, 3, pause, pauseNames, pauseActions, font);

            #endregion

            // makes the freeze icon
            freeze = new Drawable()
            {
                attributes = new Attributes()
                {
                    texture = this.Content.Load<Texture2D>("Freeze"),
                    position = new Vector2(300, 300),
                    color = Color.White
                }
            };
            freeze.isSeeable = false;

            // load images for particleengine and make a new one
            textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("circle"));
            textures.Add(Content.Load<Texture2D>("diamond"));
            textures.Add(Content.Load<Texture2D>("star"));
            //test = new ParticleEngine(textures, new Vector2(512, 400), .99f);

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].textures = textures;
            }
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            
            // updates game screens and all 
            if (speed != null)
            {
                speed.Update(gameTime);
                if (speed.speedState == Speed.gameState.PlayAgain)
                {
                    if (PlayAgain.isPaused) PlayAgain.isPaused = false;
                    speed.isHalted = true;
                }
            }
            //test.Update(gameTime);
            MainMenu.Update(gameTime);
            Pause.Update(gameTime);
            PlayAgain.Update(gameTime);
            KeyUpdate(gameTime);
            base.Update(gameTime);
        }

        protected void KeyUpdate(GameTime gameTime)
        {
            // checks if buttons recently pressed
            KeyboardState newState = Keyboard.GetState();

            // toggles full screen if escape is pressed
            if (newState.IsKeyDown(Keys.Escape))
            {
                if (!oldState.IsKeyDown(Keys.Escape))
                {
                    graphics.IsFullScreen = !graphics.IsFullScreen;
                    graphics.ApplyChanges();
                }
            }

            // pauses game if p is pressed
            if (newState.IsKeyDown(Keys.P))
            {
                if (!oldState.IsKeyDown(Keys.P))
                {
                    if(speed != null && speed.isPaused == false && speed.speedState != Speed.gameState.PlayAgain)
                    {
                        speed.Halt();
                        Pause.isPaused = false;
                    }
                }
            }

            // displays freeze image..?
            if (newState.IsKeyDown(Keys.H))
            {
                if (!oldState.IsKeyDown(Keys.H))
                {
                    freeze.isSeeable = !freeze.isSeeable;
                }
            }

            // updates oldstate of keyboard
            oldState = newState;
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix translate = Matrix.CreateTranslation(Shake());

            // TODO: Add your drawing code here
            // shakes screen if card is played
            if (speed != null && speed.isShaking) spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, translate);
            else spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            
            // draws screens
            if (speed != null) speed.Draw(spriteBatch);
            MainMenu.Draw(spriteBatch);
            Pause.Draw(spriteBatch);
            PlayAgain.Draw(spriteBatch);
            if (instructions != null) instructions.Draw(spriteBatch);
            freeze.Draw(spriteBatch, SpriteEffects.None);
            //test.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // method to shake screen
       private Vector3 Shake()
       {
           return new Vector3((float)random.NextDouble() * 4 - 2, (float)random.NextDouble() * 4 - 2, 0);
       }

    }
}