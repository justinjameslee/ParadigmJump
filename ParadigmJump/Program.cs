using System;
using SplashKitSDK;
using System.IO;

namespace ParadigmJump
{
    public class Program
    {
        public static void Main()
        {
            SplashKit.OpenWindow("Paradigm Jump", 800, 600);
            SplashKit.AudioReady();
            do
            {
                Game.getInstance().State.Loop();
            } while (!SplashKit.WindowCloseRequested("Paradigm Jump"));
        }
    }
}
