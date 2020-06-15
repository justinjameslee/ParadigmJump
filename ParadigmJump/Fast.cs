using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Automation;
using SplashKitSDK;

namespace ParadigmJump
{
    public class Fast : IModifiers
    {
        private int lives;
        public void drawScore()
        {
            SplashKit.DrawText(Game.getInstance().Score.ToString(), Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 35, 15, 15);
            SplashKit.DrawText(Game.getInstance().Combo.ToString(), Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 25, Game.getInstance().Player.X + 10, Game.getInstance().Player.Y + 10);
            SplashKit.DrawText($"Lives", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 25, 360, 420);
            SplashKit.DrawText(Game.getInstance().Lives.ToString(), Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 25, 395, 445);
        }

        public void modifyGame()
        {
            Game.getInstance().Base = 500;
            Game.getInstance().Size = 25;
            Game.getInstance().Speed[0] = 150;
            Game.getInstance().Speed[1] = 25;
            lives = 3;
        }

        public int Lives() => lives;

        public void Toggle()
        {
            foreach(Button b in Game.getInstance().Buttons)
            {
                b.Toggle = false;
            }
            Game.getInstance().Buttons[3].Toggle = true;
        }
    }
}
