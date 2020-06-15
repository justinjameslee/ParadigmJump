using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SplashKitSDK;
using osuElements.Beatmaps;
using osuElements.Beatmaps.Difficulty;
using NAudio;
using NAudio.Wave;
using NAudio.MediaFoundation;
using System.Diagnostics.Contracts;
using osuElements.Helpers;
using System.Windows.Forms;
using System.Linq;
using System.Xml;
using System.Reflection;

namespace ParadigmJump
{
    public class Game
    {
        private static Game instance;

        public Game()
        {
            Objects = new List<Block>();
            HitObjects = new List<HitCube>();
            HP = new List<HitCube>();
            Buttons = new List<Button>();
            ButtonColor = new List<Color>();
            DisplayText = new List<Text>();
            Player = new Player();
            Timer = new SplashKitSDK.Timer("level");
            Speed = new int[2];
            SplashKit.LoadSoundEffect("hitsound", Path.Combine(Environment.CurrentDirectory, @"Resources\normal-hitnormal.wav"));
            SplashKit.LoadSoundEffect("miss", Path.Combine(Environment.CurrentDirectory, @"Resources\combobreak.wav"));
            SplashKit.LoadFont("Bungee", Path.Combine(Environment.CurrentDirectory, @"Resources\Bungee-Regular.otf"));
            SplashKit.LoadBitmap("Background", Path.Combine(Environment.CurrentDirectory, @"Resources\Paradigm-Background.png"));
            SetState(new MainMenu());
            SetModifier(new Normal());
        }

        public static Game getInstance()
        {
            if (instance == null) instance = new Game();
            return instance;
        }

        public void SetModifier(IModifiers modifier) => Modifier = modifier;
        public void SetState(IState state) => State = state;

        public void HandleInput()
        {
            Player.HandleInput();
            StayOnWindow();
            LoadHitObjects();
            MoveHitObjects(HitObjects);
        }

        public void Update()
        {
            if (Player.Jump == true)
            {
                if (Player.Force > 0)
                {
                    Player.Y -= Player.Force;
                    Player.Force -= 0.0025f;
                }
            }
            CheckCollision();
            CheckHitObject(HitObjects);
            CheckHitObject(HP);
            if (Lives == 0 && Modifier.GetType() != typeof(NoFail)) SetState(new GameOver());
            if (State.GetType() == typeof(GameRunning) && !SplashKit.MusicPlaying()) SplashKit.PlayMusic("audio");
        }

        public void Draw()
        {
            SplashKit.ClearScreen();
            SplashKit.DrawBitmap("Background", 0, 0);
            foreach (GameObject o in Objects)
            {
                o.Draw();
            }
            if (HP.Count > 0)
            {
                for (int i = 0; i < HP.Count; i++)
                {
                    if (HP[i].Hit)
                    {
                        HP.Remove(HP[i]);
                    }
                    else HP[i].Draw();
                }
            }
            if (HitObjects.Count > 0)
            {
                for (int i = 0; i < HitObjects.Count; i++)
                {
                    if (HitObjects[i].Y >= SplashKit.ScreenHeight() || HitObjects[i].Hit)
                    {
                        HitObjects.Remove(HitObjects[i]);
                    }
                    else HitObjects[i].Draw();
                }
            }
            
            Player.Draw();
            Modifier.drawScore();
            ProgressBar();
        }

        public void Clicked(Point2D pt2D)
        {
            foreach (Button b in Buttons)
            {
                if (b.IsAt(pt2D))
                {
                    if (Buttons.IndexOf(b) == 0)
                    {
                        State.TopClicked();
                        break;
                    }
                    else if (Buttons.IndexOf(b) == 1)
                    {
                        State.Exit();
                        break;
                    }
                    else if (Buttons.IndexOf(b) >= 2)
                    {
                        if (!b.Toggle)
                        {
                            b.Toggle = true;
                            if (Buttons.IndexOf(b) == 2) SetModifier(new NoFail());
                            else if (Buttons.IndexOf(b) == 3) SetModifier(new Fast());
                            else if (Buttons.IndexOf(b) == 4) SetModifier(new Easy()); 
                        }
                        else
                        {
                            b.Toggle = false;
                            SetModifier(new Normal());
                        }

                    }
                }
            }
        }

        public void CheckHover(Point2D pt2D)
        {
            foreach (Button o in Buttons)
            {
                if (o.IsAt(pt2D) || o.Toggle)
                {
                    ButtonColor[Buttons.IndexOf(o)] = HoverColor;
                    o.Hover = true;
                }
                else
                {
                    ButtonColor[Buttons.IndexOf(o)] = DefaultColor;
                    o.Hover = false;
                }
                o.Draw();
            }
            foreach (Text t in DisplayText) t.Draw();
            SplashKit.RefreshScreen(144);
        }

        public void CheckCollision()
        {
            Rectangle player = new Rectangle { X = Player.X, Y = Player.Y, Width = Player.Width, Height = Player.Height };
            foreach (Block o in Objects)
            {

                if (SplashKit.RectanglesIntersect(player, new Rectangle { X = o.X, Y = o.Y, Width = o.Width, Height = o.Height }) && ((Player.Y + Player.Height) <= (o.Y + o.Height)))
                {
                    if (Player.Force <= 0)
                    {
                        Player.Y = o.Y - 50;
                        Player.Jump = false;
                    }
                }
                else if (Player.Y == SplashKit.ScreenHeight() - Player.Height)
                {
                    Player.Jump = false;
                }

            }
            Player.Y += 0.2f;
        }

        public void CheckHitObject(List<HitCube> list)
        {
            Rectangle player = new Rectangle { X = Player.X, Y = Player.Y, Width = Player.Width, Height = Player.Height };
            foreach (HitCube o in list)
            {
                if (SplashKit.RectanglesIntersect(player, new Rectangle { X = o.X, Y = o.Y, Width = o.Width, Height = o.Height }) && !o.Hit)
                {
                    o.Hit = true;
                    if (o.Width == Size && o.Height == Size)
                    {
                        Score += (Combo * Base);
                        Combo += 1;
                        Hits++;
                        SplashKit.PlaySoundEffect("hitsound");
                    }
                    else if (o.Height == 25)
                    {
                        if (Lives == 5) o.Hit = false;
                        else if (o.X == 375) Lives = 5;
                        else Lives++;
                    }
                    else
                    {
                        Combo--;
                        Lives--;
                        LivesUsed++;
                        SplashKit.PlaySoundEffect("miss");
                    }
                }
            }
        }

        public void ProgressBar()
        {
            SplashKit.DrawRectangle(DefaultColor, -1, -1, SplashKit.ScreenWidth() + 2, 10);
            float percentage = (float)Timer.Ticks / (float)Duration.TotalMilliseconds;
            float progress = percentage * SplashKit.ScreenWidth();
            SplashKit.FillRectangle(DefaultColor, -1, -1, (int)progress, 10);
        }

        public void LoadLevel(bool player)
        {
            Modifier.modifyGame();
            HitObjects.Clear();
            Objects.Clear();
            HP.Clear();
            Score = 0;
            Combo = 0;
            Lives = Modifier.Lives();
            Block.RegisterBlock("Platform", typeof(Platform));
            Block.RegisterBlock("Player", typeof(Player));
            Objects.Add(Block.CreateBlock("Platform", 100, 10, 0, 525, DefaultColor));
            Objects.Add(Block.CreateBlock("Platform", 100, 10, 700, 525, DefaultColor));
            Objects.Add(Block.CreateBlock("Platform", 100, 10, 150, 450, DefaultColor));
            Objects.Add(Block.CreateBlock("Platform", 100, 10, 550, 450, DefaultColor));
            Objects.Add(Block.CreateBlock("Platform", 200, 10, 300, 375, DefaultColor));
            HP.Add(new HP(Color.OrangeRed, 25, 490, 50, 25, false, "HP"));
            HP.Add(new HP(Color.OrangeRed, 725, 490, 50, 25, false, "HP"));
            HP.Add(new HP(Color.OrangeRed, 175, 415, 50, 25, false, "HP"));
            HP.Add(new HP(Color.OrangeRed, 575, 415, 50, 25, false, "HP"));
            HP.Add(new HP(Color.IndianRed, 375, 340, 50, 25, false, "MAX"));
            if (player) Objects.Add(Block.CreateBlock("Player", 75, 50, 200, 550, Color.OrangeRed));
        }

        public void LoadHitObjects()
        {
            if (Timer.Ticks < Duration.TotalMilliseconds && Timer.IsStarted == true)
            {
                foreach (HitObject o in Level.Beatmap.HitObjects)
                {
                    if (Timer.Ticks == o.StartTime - (Level.Beatmap.DifficultyApproachRate * Speed[0]))
                    {
                        int index = HitObjects.FindIndex(f => f.X == o.StartPosition.X);
                        if (index == -1)
                        {
                            TotalHits++;
                            HitObjects.Add(new Cube(DefaultColor, o.StartPosition.X, -50, Size, Size, false, false));
                            Random gen = new Random();
                            if (gen.Next(100) < 40) HitObjects.Add(new Cube(DefaultColor, o.StartPosition.X + 10, -150, 20, 20, false, true));

                        }
                    }
                }
            }
            else if (Timer.Ticks > Duration.TotalMilliseconds && State.GetType() != typeof(GameEnded))
            {
                Timer.Stop();
                SplashKit.StopMusic();
                SetState(new GameEnded());
            }
        }

        public void MoveHitObjects(List<HitCube> list)
        {
            foreach (GameObject o in list)
            {
                if (o.Y < SplashKit.ScreenHeight()) o.Y += Level.Beatmap.DifficultyApproachRate / Speed[1];
            }
        }

        public void StayOnWindow()
        {
            if (Player.Y > SplashKit.ScreenHeight() - Player.Height)
            {
                Player.Y = SplashKit.ScreenHeight() - Player.Height;
            }
            if (Player.X < 0)
            {
                Player.X = 0;
            }
            else if (Player.X > SplashKit.ScreenWidth() - Player.Width)
            {
                Player.X = SplashKit.ScreenWidth() - Player.Width;
            }
        }

        public IState State { get; set; }
        public IModifiers Modifier { get; set; }
        public Level Level { get; set; }
        public SplashKitSDK.Timer Timer { get; set; }
        public Player Player { get; }
        public Mp3FileReader Reader { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Block> Objects { get; set; }
        public List<HitCube> HitObjects { get; set; }
        public List<HitCube> HP { get; set; }
        public List<Button> Buttons { get; set; }
        public List<Text> DisplayText { get; set; }
        public List<Color> ButtonColor { get; set; }
        public int Base { get; set; }
        public int Score { get; set; }
        public int Lives { get; set; }
        public int Combo { get; set; }
        public int Hits { get; set; }
        public int TotalHits { get; set; }
        public int LivesUsed { get; set; }
        public int Size { get; set; }
        public int[] Speed { get; set;}
        public Color DefaultColor { get; set; } = Color.White;
        public Color HoverColor { get; set; } = Color.Black;
        public string DefaultFont { get; set; } = "Bungee";
    }
}
