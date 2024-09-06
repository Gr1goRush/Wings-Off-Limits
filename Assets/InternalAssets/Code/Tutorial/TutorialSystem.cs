using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    public GameObject plane;
    public Image guide;

    public string[] Hints;
    public TextMeshProUGUI HintsText;

    private int iterator = -1;

    private void Start()
    {
        SaveDataManager.CompletedLevels = 1;

        DOTween.Sequence()
            .Append(guide.DOFade(0, 0))
            .Append(plane.transform.DOMove(Vector3.zero, 3f).SetEase(Ease.InOutBack))
            .Append(guide.DOFade(1, 2f))
            .Append(guide.transform.DOShakeScale(2, 0.3f));
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                
                iterator++;

                if (iterator >= Hints.Length)
                {
                    SceneHelper.LoadScene(ProjectScene.Gameplay);
                    return;
                }

                HintsText.text = Hints[iterator];
            }
        }
    }
}
