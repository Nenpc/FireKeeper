using System;


namespace FireKeeper.Core.Engine
{
    public interface IProgressController
    {
        event Action<float> ProgressAction;
        event Action WinAction;
        event Action DefeatAction;
        int GetWinTime();
    }
}