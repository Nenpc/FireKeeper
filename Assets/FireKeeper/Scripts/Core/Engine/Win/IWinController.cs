using System;


namespace FireKeeper.Core.Engine
{
    public interface IWinController
    {
        event Action<float> ProgressAction;
        event Action WinAction;
        int GetWinTime();
    }
}