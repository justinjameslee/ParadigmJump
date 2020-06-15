using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public class Countdown : IState
    {
        private int _countdown;
        private System.Windows.Forms.Timer _countdownTimer;
        private bool _paused;

        public Countdown(bool paused)
        {
            _countdownTimer = new System.Windows.Forms.Timer();
            _countdownTimer.Tick += new EventHandler(countdown_Tick);
            _countdownTimer.Interval = 1000; // 1 second
            _paused = paused;
        }
        public void Loop()
        {
            _countdown = 3;
            _countdownTimer.Start();
            while (_countdown > 0)
            {
                SplashKit.ProcessEvents();
                Game.getInstance().Draw();
                SplashKit.DrawText(_countdown.ToString(), Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 75, 375, 225);
                SplashKit.RefreshScreen();
            }
            if (_paused)
            {
                Game.getInstance().Timer.Resume();
                SplashKit.ResumeMusic();
            }
            else
            {
                Game.getInstance().Timer.Start();
                SplashKit.PlayMusic("audio");
                SplashKit.SetMusicVolume(0.6f);
            }
            Game.getInstance().SetState(new GameRunning());
        }
        public void TopClicked() { }

        public void Exit() { }

        private void countdown_Tick(object sender, EventArgs e)
        {
            _countdown--;
            if (_countdown == 0) _countdownTimer.Stop();
        }
    }
}
