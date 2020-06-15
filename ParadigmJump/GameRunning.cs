using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio.Wave;
using SplashKitSDK;

namespace ParadigmJump
{
    public class GameRunning : IState
    {
        public void Loop()
        {
            SplashKit.ProcessEvents();
            if (SplashKit.KeyDown(KeyCode.EscapeKey)) Game.getInstance().SetState(new Paused());
            Game.getInstance().Update();
            Game.getInstance().HandleInput();
            SplashKit.ClearScreen();
            Game.getInstance().Draw();
            SplashKit.RefreshScreen(144);
        }
        public void TopClicked() { }

        public void Exit() { }
    }
}
