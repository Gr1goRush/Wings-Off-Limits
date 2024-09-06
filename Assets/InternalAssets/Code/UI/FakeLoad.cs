using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FakeLoad : MonoBehaviour
{
    public Slider slider;


    private void Start()
    {
        slider.DOValue(100, 4f)
            .onComplete = () =>
            {
                SceneHelper.LoadScene(ProjectScene.Menu);
            };
    }
}
