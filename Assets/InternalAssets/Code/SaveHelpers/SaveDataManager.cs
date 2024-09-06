using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveDataManager
{
    private static string settingsFilePath = Application.persistentDataPath + "/save.playersettings";
    public static SettingsData SettingsData { get; private set; }
    
    public static int CompletedLevels
    {
        get
        {
            return PlayerPrefs.GetInt("CompLevels");
        }
        set
        {
            value = Mathf.Clamp(value, 1, 11);
            PlayerPrefs.SetInt("CompLevels", value);
        }
    }

    public static void SaveSettingsData(SettingsData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(settingsFilePath, FileMode.Create);

        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Settings data saved succes");
        
    }

    public static void SaveChachedSettingsData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(settingsFilePath, FileMode.Create);

        bf.Serialize(fs, SettingsData);
        fs.Close();
        Debug.Log("Settings data saved succes");
        
    }

    public static IEnumerator LoadSettingsData()
    {
        if (!File.Exists(settingsFilePath))
        {
            SettingsData newData = new SettingsData();
            SaveSettingsData(newData);
            SettingsData = newData;
            Debug.LogWarning("Settings data load error, create new one");
            yield break;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(settingsFilePath, FileMode.Open);

        SettingsData sd = (SettingsData)bf.Deserialize(fs);
        SettingsData = sd;
        fs.Close();
        Debug.Log("Settings data load succes");
        yield break;
    }

    public static void UnlockNewLevel()
    {
        CompletedLevels++;
    }
}
