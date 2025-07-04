using UnityEngine;

public class FrameLimiter : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
