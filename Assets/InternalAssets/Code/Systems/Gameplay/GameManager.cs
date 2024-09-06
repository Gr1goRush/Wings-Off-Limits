using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnGameWinned;
    public UnityEvent OnGameLost;

    private LevelData levelData;

    private int _brokenBoxLimit;
    private int _brokenBoxCount;

    private void OnEnable()
    {
        GameBootstrap.OnDataReceived += SetupData;
        Box.OnBoxDestroyed += CheckBoxes;
    }

    private void OnDisable()
    {
        GameBootstrap.OnDataReceived -= SetupData;
        Box.OnBoxDestroyed -= CheckBoxes;
    }

    public void SetupData(LevelData data)
    {
        levelData = data;

        foreach (var item in data.BoxContainer.Boxes)
        {
            if (!item.IsStrong)
            {
                _brokenBoxLimit++;
            }
        }
    }

    public void CheckBoxes(Box box)
    {
        if (box.IsRich) LoseGame();
        else
        {
            _brokenBoxCount++;
            if (_brokenBoxCount >= _brokenBoxLimit - 1) LoseGame();
        }
    }

    public void WinGame()
    {
        if (levelData.LevelID >= SaveDataManager.CompletedLevels)
        {
            SaveDataManager.UnlockNewLevel();
        }

        OnGameWinned?.Invoke();
    }

    public void LoseGame()
    {
        OnGameLost?.Invoke();
    }
}
