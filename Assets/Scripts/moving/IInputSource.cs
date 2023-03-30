using UnityEngine;

namespace moving
{
    public interface IInputSource
    {
        bool IsDown(InputCode code);
        bool IsPressed(InputCode code);
        bool IsUp(InputCode code);
        
        RuntimePlatform[] GetAllowPlatforms();
    }
}