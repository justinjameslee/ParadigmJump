using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public class NoFail : IModifiers
    {
        private int lives;
        public void drawScore()
        {
            SplashKit.DrawText(Game.getInstance().Score.ToString(), Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 35, 15, 15);
            SplashKit.DrawText(Game.getInstance().Combo.ToString(), Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 25, Game.getInstance().Player.X + 10, Game.getInstance().Player.Y + 10);
        }

        public void modifyGame()
        {
            Game.getInstance().Base = 100;
            Game.getInstance().Size = 50;
            Game.getInstance().Speed[0] = 75;
            Game.getInstance().Speed[1] = 25;
            lives = 5;
        }

        public int Lives() => lives;

        public void Toggle()
        {
            foreach (Button b in Game.getInstance().Buttons)
            {
                b.Toggle = false;
            }
            Game.getInstance().Buttons[2].Toggle = true;
        }
    }
}
