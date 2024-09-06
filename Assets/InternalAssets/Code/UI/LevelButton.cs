using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class LevelButton : MonoBehaviour
{
    public UnityEvent OnLockedClick;

    [SerializeField, HideInInspector] private Image _image;
    [SerializeField, HideInInspector] private Button _button;

    [SerializeField] private int _levelID;
    [SerializeField] private Image _lockIcon;
    [SerializeField] private TextMeshProUGUI _levelIDtext;

    public bool UnlockedFromStart = false;

    private void OnValidate()
    {
        _image ??= GetComponent<Image>();
        _button ??= GetComponent<Button>();

            _levelIDtext.text = _levelID.ToString();
        
    }

    private void Start()
    {
        if (_levelID <= SaveDataManager.CompletedLevels || UnlockedFromStart)
        {
            _lockIcon.gameObject.SetActive(false);
            _levelIDtext.gameObject.SetActive(true);

            _button.onClick.AddListener(LoadGame);
        }
        else
        {
            _button.onClick.AddListener(LockedEffect);
        }
    }

    public void LoadGame()
    {
        GameBootstrap.SetLevelData(FindObjectOfType<LevelDataContainer>().levelDataArray[_levelID]);

        if (PlayerPrefs.GetInt("SawTutorial") == 5) 
        SceneHelper.LoadScene(ProjectScene.Gameplay);
        else
        {
            PlayerPrefs.SetInt("SawTutorial", 5);
            SceneHelper.LoadScene(ProjectScene.Tutorial);
        }
    }

    public void LockedEffect()
    {
        OnLockedClick?.Invoke();

        _lockIcon.transform.DOShakeScale(0.5f, 0.5f).
            onComplete = () =>
            {
                _lockIcon.transform.DOScale(Vector2.one, 0.2f);
            };

    }


}
