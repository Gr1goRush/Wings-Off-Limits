using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    public UnityEvent OnTimerReach;

    [SerializeField] private TextMeshProUGUI _timerText;
    private int timeRemain;
    private WaitForSeconds await = new WaitForSeconds(1);
    private void OnEnable()
    {
        GameBootstrap.OnDataReceived += SetupData;
    }

    private void OnDisable()
    {
        GameBootstrap.OnDataReceived -= SetupData;
    }

    public void SetupData(LevelData data)
    {
        timeRemain = data.Time;
    }

    public void Activate()
    {
        StartCoroutine(timerRoutine());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }


    private IEnumerator timerRoutine()
    {
        for (int i = timeRemain; i >= 0; i--)
        {
            if (!PauseManager.Paused)
            {
                if (SpeedBuffSystem.DoubleSpeed) i--;
                i = Math.Clamp(i, 0, int.MaxValue);
                _timerText.text = i.ToString() + " s";
                yield return await;
            }
            else
            {
                while (PauseManager.Paused)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        OnTimerReach?.Invoke();
    }
}
