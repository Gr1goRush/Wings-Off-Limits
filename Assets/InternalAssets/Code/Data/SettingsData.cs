using System;

[Serializable]
public class SettingsData
{
    public bool MusicEnabled;
    public bool SoundsEnabled;
    public bool VibroEnabled;

    public SettingsData(bool musicEnabled, bool soundsEnabled, bool vibroEnabled)
    {
        MusicEnabled = musicEnabled;
        SoundsEnabled = soundsEnabled;
        VibroEnabled = vibroEnabled;
    }

    public SettingsData() 
    {
        MusicEnabled = true;
        SoundsEnabled = true;
        VibroEnabled = true;
    }
}
