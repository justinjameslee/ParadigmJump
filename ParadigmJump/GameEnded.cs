using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SplashKitSDK;

namespace ParadigmJump
{
    public class GameEnded : IState
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
            Game.getInstance().Timer.Stop();
            SplashKit.StopMusic();
            Game.getInstance().LoadLevel(false);
            Game.getInstance().SetState(new Countdown(false));
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
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen();
            SplashKit.DrawBitmap("Background", 0, 0);
            string rank;
            float percentage = (float)Game.getInstance().Hits / (float)Game.getInstance().TotalHits;
            Game.getInstance().DisplayText.Clear();
            if (!startup)
            {
                Game.getInstance().Buttons.Clear();
                Game.getInstance().ButtonColor.Clear();
                Game.getInstance().ButtonColor.AddRange(Enumerable.Repeat(Game.getInstance().DefaultColor, 3));
                startup = true;
            }
            SplashKit.DrawRectangle(Game.getInstance().DefaultColor, 195, 150, 450, 95);
            Game.getInstance().DisplayText.Add(new Text("Level Complete", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 40, 225, 90));
            Game.getInstance().DisplayText.Add(new Text($"Score: {Game.getInstance().Score}", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 15, 200, 160));
            Game.getInstance().DisplayText.Add(new Text($"Highest Combo: {Game.getInstance().Combo}", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 15, 200, 180));
            Game.getInstance().DisplayText.Add(new Text($"Bombs Hit: {Game.getInstance().LivesUsed}", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 15, 200, 200));
            Game.getInstance().DisplayText.Add(new Text($"Hits: {Game.getInstance().Hits} / {Game.getInstance().TotalHits}", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 15, 200, 220));
            if (percentage == 1) rank = "S+";
            else if (percentage >= 0.9) rank = "S";
            else if (percentage >= 0.8) rank = "A";
            else if (percentage >= 0.7) rank = "B";
            else if (percentage >= 0.5) rank = "C";
            else if (percentage >= 0.2) rank = "D";
            else rank = "F";
            Game.getInstance().DisplayText.Add(new Text("Rank", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 15, 580, 170));
            Game.getInstance().DisplayText.Add(new Text(rank, Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 35, 592, 190));
            Game.getInstance().DisplayText.Add(new Text("Retry", Game.getInstance().ButtonColor[0], Game.getInstance().DefaultFont, 35, 245, 270));
            Game.getInstance().DisplayText.Add(new Text("Exit", Game.getInstance().ButtonColor[1], Game.getInstance().DefaultFont, 35, 450, 270));
            int index = Game.getInstance().Buttons.FindIndex(f => f.Width == 160);
            if (index == -1)
            {
                Game.getInstance().Buttons.Add(new Button(Game.getInstance().DefaultColor, 230, 265, 160, 50));
                Game.getInstance().Buttons.Add(new Button(Game.getInstance().DefaultColor, 415, 265, 160, 50));
            }
            foreach (GameObject o in Game.getInstance().Objects) o.Draw();
            foreach (HP o in Game.getInstance().HP) o.Draw();
            foreach (Button o in Game.getInstance().Buttons) o.Draw();
            Game.getInstance().Player.Draw();
        }
    }
}
