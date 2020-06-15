using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SplashKitSDK;

namespace ParadigmJump
{
    public class Normal : IModifiers
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
            Game.getInstance().Base = 300;
            Game.getInstance().Size = 50;
            Game.getInstance().Speed[0] = 75;
            Game.getInstance().Speed[1] = 25;
            lives = 5;
        }

        public int Lives() => lives;

        public void Toggle()
        {
            if(Game.getInstance().State.GetType() == typeof(MainMenu))
            {
                for(int i = 2; i < Game.getInstance().Buttons.Count; i++)
                {
                    Game.getInstance().Buttons[i].Toggle = false;
                }
            }
        }
    }
}
