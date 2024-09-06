using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static event Action<bool> onPauseStateUpdated;
    private static bool _paused;

    public static bool Paused => _paused;

    private void Start()
    {
        Continue();
    }

    public void Pause()
    {
        _paused = true;
        onPauseStateUpdated?.Invoke(Paused);
    }

    public void Continue()
    {
        _paused = false;
        onPauseStateUpdated?.Invoke(Paused);
    }
}
