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
    // TO DO:
    //  - Vibrate Controllers
    //  - Piles have depth
    //  - Adjust Play Again menu



    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        SpriteFont instructionsfont;
        Card[] cards;
        Drawable background;
        Texture2D selector;
        Speed speed;
        Levels levels;
        KeyboardState oldState;
        GamePadState elderState1;
        GamePadState elderState2;
        Menu MainMenu;
        Menu GameMenu1;
        Menu GameMenu2;
        Menu MarathonMenu;
        Menu TimedMenu;
        Menu PlayAgain;
        Menu Pause;
        Settings settings;
        Settings settings2;
        Switch SoundSwitch;
        Switch PowerUpSwitch;
        Switch DifficultySwitch;
        PowerUp freeze;
        List<Texture2D> textures;
        ParticleEngine test;
        ParticleEngine test2;
        ParticleEngine test3;
        Random random = new Random();
        Menu InstructionsMenu;
        Instructions Controls;
        Instructions Rules;
        Instructions PowerUps;
        Instructions Winning;
        SoundEffect shuffle;
        SoundEffect playcard;
        SoundEffectInstance instance;
        float aspectRatio;
        Player player1;
        Player player2;
        Player computer;
        Player computer2;
        Drawable BadTime;
        SoundEffectInstance shuffleinstance;
        bool soundOn = true;
        bool powerUpsOn = true;
        Mode myMode;
        Drawable checkMark;
        Levels.Difficulty difficulty;

        public enum Mode
        {
            none,
            onePlayer,
            twoPlayer,
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set screen size to 1024x800
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1024;
            myMode = Mode.twoPlayer;

            difficulty = Levels.Difficulty.Medium;
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

            // loads up badaboom font, since it is "SpriteFont3"
            font = Content.Load<SpriteFont>("SpriteFont3");
            SpriteFont instructionsfont = this.Content.Load<SpriteFont>("SpriteFont1");

            // loads up background as drawable
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

            Drawable PauseBackground = new Drawable()
            {
                attributes = new Attributes()
                {
                    texture = this.Content.Load<Texture2D>("PlainWhite"),
                    color = new Color(0, 100, 100, 23),
                    position = new Vector2(512, 400),
                    depth = .05f,
                    height = 802,
                    width = 1026,
                    rotation = 0
                }
            };

            // creates players
            player1 = new HumanPlayer(Keys.Up, Keys.Down, Keys.Left, Keys.Right, "Ben", true);
            player2 = new HumanPlayer(Keys.W, Keys.S, Keys.A, Keys.D, "Ben", false);
            computer = new ComputerPlayer("computer", false);
            computer2 = new ComputerPlayer("computer2", true);

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

            // loads up playable music, creates instance also, which may not be necessary..
            shuffle = Content.Load<SoundEffect>("Audio\\Waves\\shuffle1");
            playcard = Content.Load<SoundEffect>("Audio\\Waves\\slapcard");
            shuffleinstance = shuffle.CreateInstance();
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

            // loads up badtime meme as a joke (for if instructions are clicked during gameplay)
            BadTime = new Drawable()
            {
                attributes = new Attributes()
                {
                    texture = this.Content.Load<Texture2D>("BadTime"),
                    color = Color.White,
                    position = new Vector2(512, 400),
                    depth = 0,
                    scale = new Vector2(1.5f,1.5f),
                }, 
                isSeeable = false,
            };

            // loads up image of a checkmark
            checkMark = new Drawable()
            {
                attributes = new Attributes()
                {
                    texture = this.Content.Load<Texture2D>("checkMark"),
                    color = Color.White,
                    depth = 0,
                },
                isSeeable = false,
            };
            
            // loads card selector
            selector = this.Content.Load<Texture2D>("CardSelector");

            // loads circles, diamonds, stars for the particle generator
            textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("circle"));
            textures.Add(Content.Load<Texture2D>("diamond"));
            textures.Add(Content.Load<Texture2D>("star"));

            // creates instance of freeze powerup
            freeze = new PowerUp(Color.LightBlue, textures,powerUpsOn)
            {
                attributes = new Attributes()
                {
                    texture = this.Content.Load<Texture2D>("Freeze"),
                    position = new Vector2(100, 100),
                    color = Color.White
                }
            };
            freeze.isSeeable = false;

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
            
            // button names for play again menu
            String[] PAButtonNames = new String[2];
            PAButtonNames[0] = "Yes";
            PAButtonNames[1] = "No";
            
            // starts new game or goes to main menu depending on choice
            Button.ClickHandler[] playAgainAction = new Button.ClickHandler[2];
            // if playagain is chosen from the menu, following options are available
            playAgainAction[0] = delegate() {
                player1.Reset(); player2.Reset(); computer.Reset();
                if (speed != null)
                {
                    // switch based on what speed game type is being played
                    switch (speed.myType)
                    {
                        // 
                            // game length?
                        case Speed.gameType.Timed:
                            int x = speed.gameLength;
                            speed = new Speed(cards, background, selector, font, speed.you, speed.opp, textures, speed.myType, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), freeze);
                            speed.gameLength = x;
                            break;
                        //
                        case Speed.gameType.Marathon:
                            int y = speed.winningscore;
                            speed = new Speed(cards, background, selector, font, speed.you, speed.opp, textures, speed.myType, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), freeze);
                            speed.winningscore = y;
                            break; 
                        // if neither of the other cases apply, do this default action
                        default:
                            speed = new Speed(cards, background, selector, font, speed.you, speed.opp, textures, speed.myType, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), freeze);
                            break;
                    }
                    speed.TurnOn(); PlayAgain.isPaused = true;
                }

                if (levels != null) {
                    levels = new Levels(cards, background, selector, font, levels._player1, textures, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), (Levels.Difficulty) DifficultySwitch.getState(), freeze);
                    levels.StartGame();
                    PlayAgain.isPaused = true;
                }
            };
            // if exit is chosen, nullifies levels or speed, returns to main menu
            playAgainAction[1] = delegate()
            {
                if (speed != null) speed = null;
                if (levels != null) levels = null; 
                PlayAgain.isPaused = true; MainMenu.isPaused = false;
            };
            
            // makes an example in which the background is transparent
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
                height = 300
            };

            // makes names for each button on pause menu
            String[] pauseNames = new String[5];
            pauseNames[0] = "Resume";
            pauseNames[1] = "Restart";
            pauseNames[2] = "Instructions";
            pauseNames[3] = "Settings";
            pauseNames[4] = "Main Menu";
            // sets up an array of events based on which button is clicked
            Button.ClickHandler[] pauseActions = new Button.ClickHandler[5];
            // disables pause menu & resumes speed goes to instructions if that's chosen, and goes to main menu if these aren't the case
            pauseActions[0] = delegate()
            {
                if (speed != null && speed.isPaused == false)
                {
                    speed.Resume();
                    Pause.isPaused = true;
                }
                if (levels != null && levels.isPaused == false)
                {
                    levels.Resume();
                    Pause.isPaused = true;
                }
            };
            // disables pause menu & displays instructions
            pauseActions[1] = delegate()
            { 
                player1.Reset(); player2.Reset(); computer.Reset();
                // restarts SPEED game if restart is chosen from pause menu
                if (speed != null)
                {
                    speed = new Speed(cards, background, selector, font, speed.you, speed.opp, textures,
                        speed.myType, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), freeze); speed.TurnOn();
                }
                // restarts LEVELS game if restart chosen from pause menu
                else if (levels != null)
                {
                    player1.Reset();
                    levels = new Levels(cards, background,selector,font, player1, textures, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(),
                        PowerUpSwitch.getOnOff(), (Levels.Difficulty)DifficultySwitch.getState(), freeze); levels.StartGame();
                }
                    Pause.isPaused = true;
            };
            // disables pause menu & shows (fake) instructions.. just a funny image
            pauseActions[2] = delegate()
            {
                BadTime.isSeeable = true; Pause.keysOff = true; Timer timer = new Timer(1);
                Pause.Add(timer); timer.SetTimer(0, 4, delegate() { BadTime.isSeeable = false; Pause.keysOff = false; });
            };

            pauseActions[3] = delegate()
            {
                Pause.isPaused = true; settings2.isPaused = false;
            };
            // returns to main menu
            pauseActions[4] = delegate() { MainMenu.isPaused = false; Pause.isPaused = true; speed = null; levels = null; };
            // creates instance of pause menu
            Pause = new Menu(PauseBackground, 5, pause, pauseNames, pauseActions, font);
            Pause.Add(BadTime);

            #endregion

            #region SettingsMenu
            // settings title
            Text settingsText = new Text("Settings", font)
            {
           attributes = new Attributes()
                {
                    color = Color.Black,

                    position = new Vector2(512, 100),
                    depth = 0f,
                },
                scale = new Vector2(.5f, .5f)
            };

            // creates settings buttons

            SoundSwitch = new Switch("Sound", 300, font, 0);
            PowerUpSwitch = new Switch("Power Ups", 400, font, 0);
            String[] names = new String[4];
            names[0] = "Baby";
            names[1] = "Easy";
            names[2] = "Medium";
            names[3] = "Hard";
            DifficultySwitch = new Switch("Difficulty", names, 500, font, 2);
            Switch[] settingSwitches = new Switch[3];
            settingSwitches[0] = SoundSwitch;
            settingSwitches[1] = PowerUpSwitch;
            settingSwitches[2] = DifficultySwitch;

            settings = new Settings(settingsText, settingSwitches, background, font);
            settings.SetButton(delegate() { settings.isPaused = true; MainMenu.isPaused = false; });
            
            #endregion

            #region SettingsMenu2

            // creates settings buttons
            Switch[] settingSwitches2 = new Switch[2];
            settingSwitches2[0] = SoundSwitch;
            settingSwitches2[1] = DifficultySwitch;

            settings2 = new Settings(settingsText, settingSwitches2, PauseBackground, font);
            settings2.SetButton(delegate() { settings2.isPaused = true; Pause.isPaused = false; });
            

            #endregion

            #region InstructionsMenu
            // makes the text for the instructions menu 
            Text Instructions = new Text("Instructions", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,

                    rotation = -.05f,
                    depth = 0f,
                },
                scale = new Vector2(.4f, .4f)
            };

            // makes names for instructions buttons
            String[] InstructionsMenuString = new String[5];
            InstructionsMenuString[0] = "Controls";
            InstructionsMenuString[1] = "Rules";
            InstructionsMenuString[2] = "Winning";
            InstructionsMenuString[3] = "PowerUps";
            InstructionsMenuString[4] = "Back";
            Button.ClickHandler[] InstructionsMenuAction = new Button.ClickHandler[5];

            // displays controls section of instructions
            InstructionsMenuAction[0] = delegate()
            {
                Controls.isPaused = false;
                InstructionsMenu.isPaused = true;
            };

            // displays rules section of instructions
            InstructionsMenuAction[1] = delegate()
            {
                InstructionsMenu.isPaused = true;
                Rules.isPaused = false;
            };

            // displays winning sections of instructions
            InstructionsMenuAction[2] = delegate()
            {
                InstructionsMenu.isPaused = true;
                Winning.isPaused = false;
            };

            // displays powerups section of instructions
            InstructionsMenuAction[3] = delegate()
            {
                InstructionsMenu.isPaused = true;
                PowerUps.isPaused = false;
            };

            // goes back to the main menu from instructions menu
            InstructionsMenuAction[4] = delegate()
            {
                InstructionsMenu.isPaused = true; 
                MainMenu.isPaused = false;
            };

            // creates instance of instructions menu
            InstructionsMenu = new Menu(background, 5, Instructions, InstructionsMenuString, InstructionsMenuAction, font);
            #endregion

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
            String[] mainMenuString = new String[5];
            mainMenuString[0] = "Single Player";
            mainMenuString[1] = "Multiplayer";
            mainMenuString[2] = "Instructions";
            mainMenuString[3] = "Settings";
            mainMenuString[4] = "Exit";
            Button.ClickHandler[] mainMenuAction = new Button.ClickHandler[5];

            // initializes mode to oneplayer, moves on to the one player game menu
            mainMenuAction[0] = delegate()
            {
                MainMenu.isPaused = true; GameMenu1.isPaused = false;
                myMode = Mode.onePlayer;
            };

            // initializes mode to twoplayer, moves on to the two player game menu
            mainMenuAction[1] = delegate()
            {
                MainMenu.isPaused = true; GameMenu2.isPaused = false;
                myMode = Mode.twoPlayer;
            };
            // if "instructions" chosen, displays instructions
            mainMenuAction[2] = delegate()
            {
                InstructionsMenu.isPaused = false; MainMenu.isPaused = true;
            };

            // if "settings" chosen, displays settings
            mainMenuAction[3] = delegate()
            {
                MainMenu.isPaused = true; settings.isPaused = false;
            };

            // exits game
            mainMenuAction[4] = delegate() { this.Exit(); };

            //makes main menu and turns it on
            MainMenu = new Menu(background, 5, title, mainMenuString, mainMenuAction, font);
            MainMenu.TurnOn();
            #endregion

            #region GameMenu1
            // creates playgame text
            Text playgame1 = new Text("Select Game", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    rotation = -.05f,
                    depth = 0f,
                },
                scale = new Vector2(.5f, .5f)
            };
            
            // makes menu buttons for one player
            String[] gameMenuString1 = new String[5];
            gameMenuString1[0] = "Normal";
            gameMenuString1[1] = "Marathon";
            gameMenuString1[2] = "Timed";
            gameMenuString1[3] = "Levels";
            gameMenuString1[4] = "Back";
            Button.ClickHandler[] gameMenuAction1 = new Button.ClickHandler[5];

            // begins normal game vs the computer
            gameMenuAction1[0] = delegate()
            {
                player1.Reset(); player2.Reset(); computer.Reset();
                speed = new Speed(cards, background, selector, font, player1, computer, textures, Speed.gameType.Normal, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), freeze);
                speed.TurnOn(); GameMenu1.isPaused = true;
            };

            // goes to 1 player marathon menu from main menu
            gameMenuAction1[1] = delegate()
            {
                player1.Reset(); player2.Reset(); computer.Reset();
                speed = new Speed(cards, background, selector, font, player1, computer, textures, Speed.gameType.Marathon, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), freeze);
                MarathonMenu.isPaused = false; GameMenu1.isPaused = true;
            };

            // goes to 1 player timed menu from main menu
            gameMenuAction1[2] = delegate()
            {
                player1.Reset(); player2.Reset(); computer.Reset();
                speed = new Speed(cards, background, selector, font, player1, computer, textures, Speed.gameType.Timed, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), freeze);
                TimedMenu.isPaused = false; GameMenu1.isPaused = true;
            };

            // begins levels mode vs computer
            gameMenuAction1[3] = delegate()
            {
                player1.Reset(); computer.Reset();
                levels = new Levels(cards, background, selector, font, player1, textures, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), (Levels.Difficulty) DifficultySwitch.getState(), freeze);
                GameMenu1.isPaused = true;
                levels.StartGame();
            };
            
            // goes back to main menu from player 1 game menu
            gameMenuAction1[4] = delegate() { GameMenu1.isPaused = true; MainMenu.isPaused = false; };

            // makes instance of game menu for single player
            GameMenu1 = new Menu(background, 5, playgame1, gameMenuString1, gameMenuAction1, font);
            #endregion

            #region GameMenu2
            // creates playgame text 
            Text playgame2 = new Text("Select Game", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    rotation = -.05f,
                    depth = 0f,
                },
                scale = new Vector2(.5f, .5f)
            };

            // creates button names for 2 player game menu
            String[] gameMenuString2 = new String[4];
            gameMenuString2[0] = "Normal";
            gameMenuString2[1] = "Marathon";
            gameMenuString2[2] = "Timed";
            gameMenuString2[3] = "Back";
            Button.ClickHandler[] gameMenuAction2 = new Button.ClickHandler[4];

            // begin normal game vs another player
            gameMenuAction2[0] = delegate()
            {
                player1.Reset(); player2.Reset(); computer.Reset();
                speed = new Speed(cards, background, selector, font, player1, player2, textures, Speed.gameType.Normal, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), freeze);
                speed.TurnOn(); GameMenu2.isPaused = true;
            };

            // goes to 2 player marathon menu from 2 player game menu
            gameMenuAction2[1] = delegate()
            {
                player1.Reset(); player2.Reset(); computer.Reset();
                speed = new Speed(cards, background, selector, font, player1, player2, textures, Speed.gameType.Marathon, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), freeze);
                MarathonMenu.isPaused = false; GameMenu2.isPaused = true;
            };

            // goes to 2 player timed menu from 2 player game menu
            gameMenuAction2[2] = delegate()
            {
                player1.Reset(); player2.Reset(); computer.Reset();
                speed = new Speed(cards, background, selector, font, player1, player2, textures, Speed.gameType.Timed, shuffle, playcard, shuffleinstance, SoundSwitch.getOnOff(), PowerUpSwitch.getOnOff(), freeze);
                TimedMenu.isPaused = false; GameMenu2.isPaused = true;
            };

            // returns to main menu from 2 player game menu
            gameMenuAction2[3] = delegate() { GameMenu2.isPaused = true; MainMenu.isPaused = false; };

            // makes new instance of 2 player game menu
            GameMenu2 = new Menu(background, 4, playgame2, gameMenuString2, gameMenuAction2, font);
            #endregion

            #region Controls

            // makes all the drawable text to display when control screen is chosen from instructions menu
            Drawable[][] ControlsText = new Drawable[1][];
            #region FirstPage
            
            ControlsText[0] = new Drawable[9];
            ControlsText[0][0] = new Text("Controls", instructionsfont)
            {
                height = 200,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 150),
                }
            };
            ControlsText[0][1] = new Text("Bottom Player:", instructionsfont)
            {
                height = 100,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 225),
                }
            };
            ControlsText[0][2] = new Text("Scroll through your cards - left and right arrow keys", instructionsfont)
            {
                height = 75,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 285),
                }
            };
            ControlsText[0][3] = new Text("Card to left pile - up arrow key.", instructionsfont)
            {
                height = 75,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 335),
                }
            };
            ControlsText[0][4] = new Text("Card to right pile - down arrow key.", instructionsfont)
            {
                height = 75,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 385),
                }
            };
            ControlsText[0][5] = new Text("Top Player:", instructionsfont)
            {
                height = 100,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 485),
                }
            };
            ControlsText[0][6] = new Text("Scroll through your cards - 'A' and 'D' keys", instructionsfont)
            {
                height = 75,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 545),
                }
            };
            ControlsText[0][7] = new Text("Card to left pile - 'W' key.", instructionsfont)
            {
                height = 75,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 595),
                }
            };
            ControlsText[0][8] = new Text("Card to right pile - 'S' key.", instructionsfont)
            {
                height = 75,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 645),
                }
            };
            #endregion

            // makes instance of the control screen with its text
            Controls = new Instructions(background, ControlsText, font);
            Controls.setButton(delegate() { Controls.isPaused = true; InstructionsMenu.isPaused = false; });

            #endregion

            #region Rules

            // makes all the drawable text to display when control screen is chosen from instructions menu
            Drawable[][] RulesText = new Drawable[1][];
            #region FirstPage
            
            RulesText[0] = new Drawable[8];

            RulesText[0][0] = new Text("Rules", instructionsfont)
            {
                height = 200,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 150),
                }
            };
            RulesText[0][1] = new Text("Each player has 5 cards in their hand at a time.", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 250),
                }
            };
            RulesText[0][2] = new Text("You can play a card if its value is one higher or lower", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 310),
                }
            };
            RulesText[0][3] = new Text("than a card in one of the middle piles.", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 350),
                }
            };
            RulesText[0][4] = new Text("Aces are both low and high, so they can be played", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 430),
                }
            };
            RulesText[0][5] = new Text("on both a King and a Two.", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 470),
                }
            };
            RulesText[0][6] = new Text("If neither player has any moves, two new middle cards", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 550),
                }
            };
            RulesText[0][7] = new Text("will be drawn and gameplay will continue as before.", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 590),
                }
            };
            #endregion

            // makes instance of rules text
            Rules = new Instructions(background, RulesText, font);
            Rules.setButton(delegate() { Rules.isPaused = true; InstructionsMenu.isPaused = false; });

            #endregion

            #region Winning
            // makes all the drawable text to display when control screen is chosen from instructions menu
            Drawable[][] WinningText = new Drawable[1][];

            #region FirstPage
            WinningText[0] = new Drawable[8];
            WinningText[0][0] = new Text("Winning", instructionsfont)
            {
                height = 200,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 150),
                }
            };
            WinningText[0][1] = new Text("In Normal Mode: The first person to play all", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 250),
                }
            };
            WinningText[0][2] = new Text("of their cards or the player with the highest score when", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 290),
                }
            };
            WinningText[0][3] = new Text("no more cards can be drawn wins the game.", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 330),
                }
            };
            WinningText[0][4] = new Text("In Marathon Mode: The first player to play a chosen", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 410),
                }
            };
            WinningText[0][5] = new Text("number of cards wins the game.", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 450),
                }
            };
            WinningText[0][6] = new Text("In Timed Mode: The player who plays the most cards", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 530),
                }
            };
            WinningText[0][7] = new Text("before time runs out wins the game.", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 570),
                }
            };
            #endregion

            // makes instance of winning text
            Winning = new Instructions(background, WinningText, font);
            Winning.setButton(delegate() { Winning.isPaused = true; InstructionsMenu.isPaused = false; });
            #endregion

            #region Winning
            // makes all the drawable text to display when control screen is chosen from instructions menu
            Drawable[][] PowerUpsText = new Drawable[1][];

            #region FirstPage
            PowerUpsText[0] = new Drawable[8];
            PowerUpsText[0][0] = new Text("PowerUps", instructionsfont)
            {
                height = 200,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 150),
                }
            };
            PowerUpsText[0][1] = new Text("PowerUps appear on game piles randomly during gameplay.", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 250),
                }
            };
            PowerUpsText[0][2] = new Text("If a card is played on a powerup, the powerup", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 290),
                }
            };
            PowerUpsText[0][3] = new Text("will immediately take effect in favor of who played the card.", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 330),
                }
            };
            PowerUpsText[0][4] = new Text("Freeze - Renders the opponent's selector immovable for around 2 seconds.", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 410),
                }
            };
            PowerUpsText[0][5] = new Text(" ", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 450),
                }
            };
            PowerUpsText[0][6] = new Text(" ", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 530),
                }
            };
            PowerUpsText[0][7] = new Text(" ", instructionsfont)
            {
                height = 80,
                attributes = new Attributes()
                {
                    color = Color.Black,
                    depth = 0f,
                    position = new Vector2(512, 570),
                }
            };
            #endregion

            // makes instance of winning text
            PowerUps = new Instructions(background, PowerUpsText, font);
            // goes back to instructions menu if back is pressed from powerups instructions
            PowerUps.setButton(delegate() { PowerUps.isPaused = true; InstructionsMenu.isPaused = false; });
            #endregion

            #region MarathonMenu
            Text ChooseLength = new Text("Set Win Score", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    rotation = -.05f,
                    depth = 0f,
                },
                scale = new Vector2(.5f, .5f)
            };

            String[] marathonMenuString = new String[4];
            marathonMenuString[0] = "Half Marathon (30)";
            marathonMenuString[1] = "Marathon (50)";
            marathonMenuString[2] = "Ultra Marathon (100)";
            marathonMenuString[3] = "Back";
            Button.ClickHandler[] marathonMenuAction = new Button.ClickHandler[4];

            marathonMenuAction[0] = delegate()
            {
                speed.winningscore = 30; speed.TurnOn(); MarathonMenu.isPaused = true;
            };

            marathonMenuAction[1] = delegate()
            {
                speed.winningscore = 50; speed.TurnOn(); MarathonMenu.isPaused = true;
            };

            marathonMenuAction[2] = delegate()
            {
                speed.winningscore = 100; speed.TurnOn(); MarathonMenu.isPaused = true;
            };

            marathonMenuAction[3] = delegate() { MarathonMenu.isPaused = true; MainMenu.isPaused = false; };

            MarathonMenu = new Menu(background, 4, ChooseLength, marathonMenuString, marathonMenuAction, font);
            #endregion

            #region TimedMenu
            Text ChooseTime = new Text("Set Time", font)
            {
                attributes = new Attributes()
                {
                    color = Color.Black,
                    rotation = -.05f,
                    depth = 0f,
                },
                scale = new Vector2(.5f, .5f)
            };

            String[] TimedMenuString = new String[5];
            TimedMenuString[0] = "30 seconds";
            TimedMenuString[1] = "1 minute";
            TimedMenuString[2] = "2 minutes";
            TimedMenuString[3] = "5 minutes";
            TimedMenuString[4] = "Back";
            Button.ClickHandler[] TimedMenuAction = new Button.ClickHandler[5];

            TimedMenuAction[0] = delegate()
            {
                speed.gameLength = 30; speed.TurnOn(); TimedMenu.isPaused = true;
            };

            TimedMenuAction[1] = delegate()
            {
                speed.gameLength = 60; speed.TurnOn(); TimedMenu.isPaused = true;
            };

            TimedMenuAction[2] = delegate()
            {
                speed.gameLength = 120; speed.TurnOn(); TimedMenu.isPaused = true;
            };

            TimedMenuAction[3] = delegate()
            {
                speed.gameLength = 300; speed.TurnOn(); TimedMenu.isPaused = true;
            };

            TimedMenuAction[4] = delegate() { TimedMenu.isPaused = true; MainMenu.isPaused = false; };

            TimedMenu = new Menu(background, 5, ChooseTime, TimedMenuString, TimedMenuAction, font);
            #endregion

            // makes the freeze icon
            freeze = new PowerUp(Color.LightBlue, textures, powerUpsOn)
            {
                attributes = new Attributes()
                {
                    texture = this.Content.Load<Texture2D>("Freeze"),
                    position = new Vector2(300, 300),
                    color = Color.White
                }
            };
            freeze.isSeeable = false;

            test = new ParticleEngine(textures, new Vector2(512, 400), new Vector2(1000,1000), .99f, Color.Blue);
            test2 = new ParticleEngine(textures, new Vector2(256, 400), new Vector2(1000, 1000), .99f, Color.Red);
            test3 = new ParticleEngine(textures, new Vector2(768, 400), new Vector2(1000, 1000), .99f, Color.White);

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

                ComputerPlayer.ChangeDifficulty((Levels.Difficulty)DifficultySwitch.getState()); 
                speed.Update(gameTime);
                if (speed.speedState == Speed.gameState.PlayAgain)
                {
                    if (PlayAgain.isPaused) PlayAgain.isPaused = false;
                    speed.isHalted = true;
                }
            }
            if (levels != null)
            {
                levels.Update(gameTime);
                if (levels.myState == Levels.LevelState.PlayAgain)
                {
                    if (PlayAgain.isPaused) PlayAgain.isPaused = false;
                    levels.Halt();
                }
            }
            // updates particle generator positions
            test.attributes.position = new Vector2(random.Next(0, 1024), random.Next(0, 800));
            test2.attributes.position = new Vector2(random.Next(0, 1024), random.Next(0, 800));
            test3.attributes.position = new Vector2(random.Next(0, 1024), random.Next(0, 800));

            // updates different menus and screens
            GameMenu1.Update(gameTime);
            GameMenu2.Update(gameTime);
            //test.Update(gameTime);
            //test2.Update(gameTime);
            //test3.Update(gameTime);
            InstructionsMenu.Update(gameTime);
            Controls.Update(gameTime);
            Rules.Update(gameTime);
            Winning.Update(gameTime);
            PowerUps.Update(gameTime);
            MainMenu.Update(gameTime);
            MarathonMenu.Update(gameTime);
            TimedMenu.Update(gameTime);
            Pause.Update(gameTime);
            PlayAgain.Update(gameTime);
            settings.Update(gameTime);
            settings2.Update(gameTime);
            KeyUpdate(gameTime);
            base.Update(gameTime);
        }

        protected void KeyUpdate(GameTime gameTime)
        {
            // checks if buttons recently pressed
            KeyboardState newState = Keyboard.GetState();
            GamePadState youngState1 = GamePad.GetState(PlayerIndex.One);
            GamePadState youngState2 = GamePad.GetState(PlayerIndex.Two);

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

                    else if (levels != null && levels.myState != Levels.LevelState.PlayAgain)
                    {
                        levels.Halt();
                        Pause.isPaused = false;
                    }
                }
            }

            if (youngState1.IsButtonDown(Buttons.Start))
            {
                if (!elderState1.IsButtonDown(Buttons.Start))
                {
                    if (speed != null && speed.isPaused == false && speed.speedState != Speed.gameState.PlayAgain)
                    {
                        speed.Halt();
                        Pause.isPaused = false;
                    }

                    else if (levels != null && levels.myState != Levels.LevelState.PlayAgain)
                    {
                        levels.Halt();
                        Pause.isPaused = false;
                    }
                }
            }

            if (youngState2.IsButtonDown(Buttons.Start))
            {
                if (!elderState2.IsButtonDown(Buttons.Start))
                {
                    if (speed != null && speed.isPaused == false && speed.speedState != Speed.gameState.PlayAgain)
                    {
                        speed.Halt();
                        Pause.isPaused = false;
                    }

                    else if (levels != null && levels.myState != Levels.LevelState.PlayAgain)
                    {
                        levels.Halt();
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
            elderState1 = youngState1;
            elderState2 = youngState2;
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
            if ((speed != null && speed.isShaking && Pause.isPaused) || levels!= null && levels.isShaking && Pause.isPaused ) 
                spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, translate);
            else spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            
            // draws screens
            if (speed != null) speed.Draw(spriteBatch);
            if (levels != null) levels.Draw(spriteBatch);
            MainMenu.Draw(spriteBatch);
            InstructionsMenu.Draw(spriteBatch);
            GameMenu1.Draw(spriteBatch);
            GameMenu2.Draw(spriteBatch);
            Pause.Draw(spriteBatch);
            MarathonMenu.Draw(spriteBatch);
            PlayAgain.Draw(spriteBatch);
            Controls.Draw(spriteBatch);
            TimedMenu.Draw(spriteBatch);
            Rules.Draw(spriteBatch);
            Winning.Draw(spriteBatch);
            PowerUps.Draw(spriteBatch);
            freeze.Draw(spriteBatch, SpriteEffects.None);
            //test.Draw(spriteBatch, SpriteEffects.None);
            //test2.Draw(spriteBatch, SpriteEffects.None);
            //test3.Draw(spriteBatch, SpriteEffects.None);
            settings.Draw(spriteBatch);
            settings2.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // method to shake screen
       private Vector3 Shake()
       {
           return new Vector3((float)random.NextDouble() * 4 - 2, (float)random.NextDouble() * 4 - 2, 0);
       }

       // toggle sound method
       public void toggleSound()
       {
           if (soundOn) soundOn = false;
           else soundOn = true;
       }
       
       // toggle powerups method
       public void togglePowerUps()
       {
           if (powerUpsOn) powerUpsOn = false;
           else powerUpsOn = true;
       }
    }
}