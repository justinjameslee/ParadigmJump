using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParadigmJump
{
    public interface IModifiers
    {
        void drawScore();

        void modifyGame();
        int Lives();

        void Toggle();
    }
}
