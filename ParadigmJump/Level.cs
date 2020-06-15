using System;
using System.Collections.Generic;
using System.Text;
using osuElements;
using osuElements.Beatmaps;

namespace ParadigmJump
{
    public class Level
    {
        public Level(string filename)
        {
            Beatmap = new Beatmap(filename);
        }

        public Beatmap Beatmap { get; set; }
    }
}
