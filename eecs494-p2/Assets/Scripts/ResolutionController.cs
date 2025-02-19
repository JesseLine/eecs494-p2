using UnityEngine;

public class ResolutionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Screen.SetResolution(1920, 1080, false);
    }
}
