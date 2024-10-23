using UnityEngine;

public class FPSUnlocker : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 10000;
    }
}
