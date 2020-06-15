using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public class Paused : IState
    {
        private bool startup = false;
        public void Loop()
        {
            Display();
            Game.getInstance().CheckHover(SplashKit.MousePosition());
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Game.getInstance().Clicked(SplashKit.MousePosition());
            }
        }
        public void TopClicked()
        {
            Game.getInstance().Buttons.Clear();
            Game.getInstance().Player.Color = Color.OrangeRed;
            Game.getInstance().Player.Speed = 0.2f;
            Game.getInstance().SetState(new Countdown(true));
        }

        public void Exit()
        {
            Game.getInstance().Timer.Stop();
            SplashKit.StopMusic();
            SplashKit.FreeAllMusic();
            Game.getInstance().LoadLevel(false);
            Game.getInstance().SetState(new MainMenu());
        }

        public void Display()
        {
            Game.getInstance().Timer.Pause();
            SplashKit.PauseMusic();
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen();
            SplashKit.DrawBitmap("Background", 0, 0);
            Game.getInstance().ProgressBar();
            Game.getInstance().DisplayText.Clear();
            if (!startup)
            {
                Game.getInstance().Buttons.Clear();
                Game.getInstance().ButtonColor.Clear();
                Game.getInstance().ButtonColor.AddRange(Enumerable.Repeat(Game.getInstance().DefaultColor, 3));
                startup = true;
            }
            Game.getInstance().DisplayText.Add(new Text("Resume", Game.getInstance().ButtonColor[0], Game.getInstance().DefaultFont, 35, 325, 175));
            Game.getInstance().DisplayText.Add(new Text("Exit", Game.getInstance().ButtonColor[1], Game.getInstance().DefaultFont, 35, 355, 250));
            Game.getInstance().Player.Draw();
            Game.getInstance().Modifier.drawScore();
            int index = Game.getInstance().Buttons.FindIndex(f => f.Width == 160);
            if (index == -1)
            {
                Game.getInstance().Buttons.Add(new Button(Game.getInstance().DefaultColor, 320, 170, 160, 50));
                Game.getInstance().Buttons.Add(new Button(Game.getInstance().DefaultColor, 320, 245, 160, 50));
            }
            foreach (GameObject o in Game.getInstance().Objects) o.Draw();
            foreach (HP o in Game.getInstance().HP) o.Draw();
            foreach (GameObject o in Game.getInstance().HitObjects) o.Draw();
        }
    }
}
