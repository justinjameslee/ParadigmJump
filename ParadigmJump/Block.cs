using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public abstract class Block : GameObject
    {
        private static Dictionary<string, Type> _BlockClassRegistry = new Dictionary<string, Type>();

        public Block (Color clr, float x, float y, int width, int height) : base (clr)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public Block() : this ( Color.Black, 0,0, 100, 10 ) { }

        public override void Draw()
        {
            SplashKit.FillRectangle(Color, X, Y, Width, Height);
        }

        public static Block CreateBlock(string name, int width, int height, float x, float y, Color clr)
        {
            Block newBlock = (Block)Activator.CreateInstance(_BlockClassRegistry[name]);
            newBlock.Width = width;
            newBlock.Height = height;
            newBlock.X = x;
            newBlock.Y = y;
            newBlock.Color = clr;
            return newBlock;
        }
        public static Block CreateBlock(string name)
        {
            return (Block)Activator.CreateInstance(_BlockClassRegistry[name]);
        }

        public static void RegisterBlock(string name, Type t)
        {
            _BlockClassRegistry[name] = t;
        }

        public static string GetKey(Type t)
        {
            foreach (KeyValuePair<string, Type> ele in _BlockClassRegistry)
            {
                if (ele.Value == t)
                {
                    return ele.Key;
                }
            }
            return "Unknown";
        }

        public override bool IsAt(Point2D pt2D)
        {
            return (pt2D.X >= X && pt2D.X <= (X + Width) && pt2D.Y >= Y && pt2D.Y <= (Y + Height));
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
