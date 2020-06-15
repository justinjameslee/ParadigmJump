using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParadigmJump
{
    public interface IState
    {
        void Loop();
        void TopClicked();
        void Exit();
    }
}
