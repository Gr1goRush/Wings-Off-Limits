using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsToggleButton : ToggleButton
{
    [SerializeField] private SettingToToggle toToggle;
    [SerializeField] private SettingsMonoProvider monoProvider;

    protected override void OnValidate()
    {
        base.OnValidate();
        try
        {
            monoProvider ??= FindAnyObjectByType<SettingsMonoProvider>();
        } catch 
        {
            Debug.LogWarning("NO MONO PROVIDER FOR TOGGLE BUTTON");
        }
    }

    protected void Start()
    {
        button.onClick.AddListener(ToggleSettingState);
        SetViewBySettings();
    }

    public void SetViewBySettings()
    {
        switch (toToggle)
        {
            case SettingToToggle.Music:
                switchState = SaveDataManager.SettingsData.MusicEnabled;
                ForceUpdateView();
                break;

            case SettingToToggle.Sounds:
                switchState = SaveDataManager.SettingsData.SoundsEnabled;
                ForceUpdateView();
                break;

            case SettingToToggle.Vibro:
                switchState = SaveDataManager.SettingsData.VibroEnabled;
                ForceUpdateView();
                break;
        }
    }

    public void ToggleSettingState()
    {
        switch (toToggle)
        {
            case SettingToToggle.Music:
                monoProvider.SettingsSetMusicEnabled(!SaveDataManager.SettingsData.MusicEnabled);
                break;

            case SettingToToggle.Sounds:
                monoProvider.SettingsSetSoundsEnabled(!SaveDataManager.SettingsData.SoundsEnabled); 
                break;

            case SettingToToggle.Vibro:
                monoProvider.SettingsSetVibroEnabled(!SaveDataManager.SettingsData.VibroEnabled); 
                break;
        }
    }
}

public enum SettingToToggle
{
    Music,
    Sounds,
    Vibro
}
