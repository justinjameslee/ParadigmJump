using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public class Player : Block
    {
        public Player(Color clr, float x, float y, int width, int height) : base (clr, x, y, width, height)
        {
            Color = clr;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Player() : this(Color.OrangeRed, 200, 550, 75, 50) { }

        public override void Draw()
        {
            SplashKit.FillRectangle(Color, X, Y, Width, Height);
        }

        public void HandleInput()
        {
            if (SplashKit.KeyDown(KeyCode.AKey)) X -= Speed;
            if (SplashKit.KeyDown(KeyCode.DKey)) X += Speed;
            if (SplashKit.KeyDown(KeyCode.RightShiftKey))
            {
                Speed = 0.5f;
                Color = Color.Blue;
            }
            if (SplashKit.KeyReleased(KeyCode.RightShiftKey))
            {
                Speed = 0.2f;
                Color = Color.OrangeRed;
            }
            if (Jump != true)
            {
                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    Jump = true;
                    Force = Gravity;
                }
            }
            if(SplashKit.KeyDown(KeyCode.SKey))
            {
                Y = SplashKit.ScreenHeight() - Height;
            }
        }

        public bool Jump { get; set; } = false;

        public float Gravity { get; set; } = 1f;

        public float Force { get; set; }

        public float Speed { get; set; } = 0.2f;
    }
}
