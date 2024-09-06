using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;


    private void OnEnable()
    {
        SettingsMonoProvider.OnSettingsChanged += ApplySettings; 
    }

    private void OnDisable()
    {
        SettingsMonoProvider.OnSettingsChanged -= ApplySettings;
    }

    private void Start()
    {
        ApplySettings(SaveDataManager.SettingsData);
    }

    public void ApplySettings(SettingsData data)
    {
        int soundVolume = data.SoundsEnabled ? 0 : -80;
        audioMixer.SetFloat("SoundsVolume", soundVolume);

        int musicVolume = data.MusicEnabled ? 0 : -80;
        audioMixer.SetFloat("MusicVolume", musicVolume);
    }
}
