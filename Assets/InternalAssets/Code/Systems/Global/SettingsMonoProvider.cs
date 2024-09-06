using System;
using UnityEngine;

public class SettingsMonoProvider : MonoBehaviour
{
    public static event Action<SettingsData> OnSettingsChanged;
    public static SettingsData ChachedSettingsData => SaveDataManager.SettingsData;
    public void SettingsSetMusicEnabled(bool state)
    {
        SaveDataManager.SettingsData.MusicEnabled = state;
        OnSettingsChanged?.Invoke(SaveDataManager.SettingsData);
        SaveDataManager.SaveChachedSettingsData();
    }

    public void SettingsSetVibroEnabled(bool state)
    {
        SaveDataManager.SettingsData.VibroEnabled = state;
        OnSettingsChanged?.Invoke(SaveDataManager.SettingsData);
        SaveDataManager.SaveChachedSettingsData();
    }

    public void SettingsSetSoundsEnabled(bool state)
    {
        SaveDataManager.SettingsData.SoundsEnabled = state;
        OnSettingsChanged?.Invoke(SaveDataManager.SettingsData);
        SaveDataManager.SaveChachedSettingsData();
    }
}
