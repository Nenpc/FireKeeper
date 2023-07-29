using UnityEngine;

namespace FireKeeper.Config
{
    public interface IMovement
    {
        void Move(Transform view, Vector3 target);
        void Stop();
    }
}