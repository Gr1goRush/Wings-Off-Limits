using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public static int BGToLoad;

    public Material bgMoveMaterial;
    public Image BgImage;
    public Sprite[] BgSprites;
    private float previousValue;
    private float _bgSpeed = 0.4f;
    private float BgSpeed
    {
        get { return _bgSpeed; }
        set
        {
            previosValue = _bgSpeed;
            _bgSpeed = value;
            bgMoveMaterial.SetFloat("_ScrollSpeed", _bgSpeed);
        }
    }
    private float previosValue = 0.2f;

    private void OnEnable()
    {
        PauseManager.onPauseStateUpdated += PauseBG;
        SpeedBuffSystem.OnSpeedBuffEnded += SetDefaultSpeed;
        Projectile.onSpeedProjecileHit += SetDoubleSpeed;
    }

    private void OnDisable()
    {
        PauseManager.onPauseStateUpdated -= PauseBG;
        SpeedBuffSystem.OnSpeedBuffEnded -= SetDefaultSpeed;
        Projectile.onSpeedProjecileHit -= SetDoubleSpeed;
    }

    private void Start()
    {
        BgImage.sprite = BgSprites[BGToLoad];
        SetDefaultSpeed();
    }

    public void PauseBG(bool state)
    {
        if (state) { BgSpeed = 0; }
        else { BgSpeed = previosValue; }
    }

    public void SetDoubleSpeed()
    {
        BgSpeed = 0.6f;
    }

    public void SetDefaultSpeed()
    {
        BgSpeed = 0.4f;
    }
}
