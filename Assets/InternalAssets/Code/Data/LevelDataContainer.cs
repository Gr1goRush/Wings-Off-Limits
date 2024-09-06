using UnityEngine;

public class LevelDataContainer : MonoBehaviour
{
    public static LevelDataContainer Instance;
    public LevelData[] levelDataArray;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
