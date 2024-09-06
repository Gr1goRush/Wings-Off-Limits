using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button), typeof(AudioSource))]
public class ToggleButton : MonoBehaviour
{
    [Space(20f)]
    [SerializeField] protected Image image;
    [SerializeField] protected Button button;
    [SerializeField] protected AudioSource audioSource;

    [SerializeField] private AudioClip _clickSound;
    [Space(20f)]
    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private Sprite _disabledSprite;

    protected bool switchState;

    protected virtual void OnValidate()
    {
        image ??= GetComponent<Image>();
        button ??= GetComponent<Button>();
        audioSource ??= GetComponent<AudioSource>();
    }

    protected virtual void Awake()
    {
        button.onClick.AddListener(SwitchState);
    }

    public void SwitchState()
    {
        Debug.Log("Switch");
        switchState = !switchState;
        PlayClickSound();
        UpdateView();
    }

    private void PlayClickSound()
    {
        if (_clickSound != null)
        {
            audioSource.PlayOneShot(_clickSound);
        }
    }

    private void UpdateView()
    {
        image.transform.DOScale(0, 0.1f).
            SetEase(Ease.InOutCirc).
            onComplete = () =>
            {
                image.sprite = switchState ? _enabledSprite : _disabledSprite;
                image.transform.DOScale(1, 0.1f).
                SetEase(Ease.InOutCirc);
            };
    }

    public void ForceUpdateView()
    {
        image.sprite = switchState ? _enabledSprite : _disabledSprite;
    }
}
