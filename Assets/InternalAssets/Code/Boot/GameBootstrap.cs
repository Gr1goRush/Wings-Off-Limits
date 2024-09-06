using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    public static int PlaneToLoad;

    public static Action<LevelData> OnDataReceived;
    private static LevelData levelData;
    public LevelData defaultLevelData;

    [SerializeField] private Transform planeSpawnPoint;
    [SerializeField] private PlaneController[] planePrefabs;
    [SerializeField] private GameObject startButton;

    [SerializeField] private ProjectileSpawner _spawner;
    [SerializeField] private GameTimer _gameTimer;


    private PlaneController _chachedPlaneController;

    private void OnValidate()
    {
        _spawner ??= FindObjectOfType<ProjectileSpawner>();
        _gameTimer ??= FindObjectOfType<GameTimer>();
    }

    private void Start()
    {
        _chachedPlaneController = Instantiate(planePrefabs[PlaneToLoad], planeSpawnPoint.transform.position, Quaternion.identity);

        if (levelData.IsEmpty()) levelData = defaultLevelData;

        StartCoroutine(bootProcces());
        OnDataReceived?.Invoke(levelData);

    }

    public void StartLevel()
    {
        _chachedPlaneController.Init();
        _spawner.Execute();
        _gameTimer.Activate();
    }

    private IEnumerator bootProcces()
    {
        BoxContainer boxContainer = Instantiate(levelData.BoxContainer, planeSpawnPoint.transform.position, Quaternion.identity);

        foreach (var box in boxContainer.Boxes)
        {
            box.Init();
        }
        yield return new WaitForSeconds(2);

        startButton.SetActive(true);
        startButton.transform.DOShakeScale(0.5f, 0.3f);
    }

    public static void SetLevelData(LevelData data)
    {
        levelData = data;
    }
}
