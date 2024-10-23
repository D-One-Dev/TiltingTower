using UnityEngine;
using Zenject;

public class FPSUnlocker : IInitializable
{
    public void Initialize()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 10000;
    }
}