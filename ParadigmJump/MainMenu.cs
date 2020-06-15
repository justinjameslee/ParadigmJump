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
    public class MainMenu : IState
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"F:\Justin\Documents\osu!\Songs";
            openFileDialog.Filter = "osu files (*.osu)|*.osu";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Game.getInstance().Level = new Level(openFileDialog.FileName);
                Game.getInstance().Reader = new Mp3FileReader(Path.GetDirectoryName(openFileDialog.FileName) + @"\" + Game.getInstance().Level.Beatmap.AudioFilename);
                SplashKit.LoadMusic("audio", Path.GetDirectoryName(openFileDialog.FileName) + @"\" + Game.getInstance().Level.Beatmap.AudioFilename);
                Game.getInstance().Duration = Game.getInstance().Reader.TotalTime;
                Game.getInstance().LoadLevel(false);
                Game.getInstance().SetState(new Countdown(false));
            }
        }

        public void Exit()
        {
            Environment.Exit(1);
        }

        public void Display()
        {
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen();
            SplashKit.DrawBitmap("Background", 0,0);
            Game.getInstance().LoadLevel(true);
            Game.getInstance().DisplayText.Clear();
            if(!startup)
            {
                Game.getInstance().Buttons.Clear();
                Game.getInstance().ButtonColor.Clear();
                Game.getInstance().ButtonColor.AddRange(Enumerable.Repeat(Game.getInstance().DefaultColor, 3));
                startup = true;
            }
            Game.getInstance().ButtonColor.AddRange(Enumerable.Repeat(Game.getInstance().DefaultColor, 3));
            Game.getInstance().DisplayText.Add(new Text("Paradigm  Jump", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 75, 50, 50));
            Game.getInstance().DisplayText.Add(new Text("Start", Game.getInstance().ButtonColor[0], Game.getInstance().DefaultFont, 35, 340, 175));
            Game.getInstance().DisplayText.Add(new Text("Exit", Game.getInstance().ButtonColor[1], Game.getInstance().DefaultFont, 35, 355, 250));
            Game.getInstance().DisplayText.Add(new Text("Modifier", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 25, 570, 180));
            Game.getInstance().DisplayText.Add(new Text("NoFail", Game.getInstance().ButtonColor[2], Game.getInstance().DefaultFont, 20, 560, 230));
            Game.getInstance().DisplayText.Add(new Text("Fast", Game.getInstance().ButtonColor[3], Game.getInstance().DefaultFont, 20, 575, 280));
            Game.getInstance().DisplayText.Add(new Text("Easy", Game.getInstance().ButtonColor[4], Game.getInstance().DefaultFont, 20, 575, 330));
            Game.getInstance().DisplayText.Add(new Text("Can't Fail", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 10, 660, 235));
            Game.getInstance().DisplayText.Add(new Text("Faster and", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 10, 660, 280));
            Game.getInstance().DisplayText.Add(new Text("Smaller Cubes", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 10, 660, 290));
            Game.getInstance().DisplayText.Add(new Text("Slower and", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 10, 660, 330));
            Game.getInstance().DisplayText.Add(new Text("Bigger Cubes", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 10, 660, 340));
            Game.getInstance().DisplayText.Add(new Text("How to play", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 25, 50, 180));
            Game.getInstance().DisplayText.Add(new Text("A - Move Left", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 15, 50, 220));
            Game.getInstance().DisplayText.Add(new Text("D - Move Right", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 15, 50, 240));
            Game.getInstance().DisplayText.Add(new Text("S - Drop Down", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 15, 50, 260));
            Game.getInstance().DisplayText.Add(new Text("Space - Jump", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 15, 50, 280));
            Game.getInstance().DisplayText.Add(new Text("RShift - Dash", Game.getInstance().DefaultColor, Game.getInstance().DefaultFont, 15, 50, 300));
            int index = Game.getInstance().Buttons.FindIndex(f => f.Width == 160);
            if (index == -1)
            {
                Game.getInstance().Buttons.Add(new Button(Game.getInstance().DefaultColor, 320, 170, 160, 50));
                Game.getInstance().Buttons.Add(new Button(Game.getInstance().DefaultColor, 320, 245, 160, 50));
                Game.getInstance().Buttons.Add(new Button(Game.getInstance().DefaultColor, 550, 220, 100, 40));
                Game.getInstance().Buttons.Add(new Button(Game.getInstance().DefaultColor, 550, 270, 100, 40));
                Game.getInstance().Buttons.Add(new Button(Game.getInstance().DefaultColor, 550, 320, 100, 40));
            }
            Game.getInstance().Modifier.Toggle();
            foreach (GameObject o in Game.getInstance().Objects) o.Draw();
            foreach (HP o in Game.getInstance().HP) o.Draw();
        }
    }
}
