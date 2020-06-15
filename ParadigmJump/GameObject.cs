using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SplashKitSDK;

namespace ParadigmJump
{
    public abstract class GameObject
    {
        private Color _color;

        public GameObject(Color color)
        {
            Color = color;
        }

        public abstract void Draw();
        public abstract bool IsAt(Point2D pt2D);

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public float X { get; set; }
        public float Y { get; set; }

    }
}
