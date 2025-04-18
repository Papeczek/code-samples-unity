using UnityEngine;
using DG.Tweening;
public class MeleeRangeIndicatorVisuals : MonoBehaviour
{
    [SerializeField] private WeaponSwitchHandler weaponSwitchHandler;
    [SerializeField] private SpriteRenderer indicatorRenderer;
    [Range(0,255)]
    [SerializeField] private int inMeleeRangeAlpha;
    private float outMeleeRangeAlpha;
    private float fadeDuration = 0.25f;
    #region Event Subscription
    void OnEnable()
    {
        weaponSwitchHandler.OnMeleeRangeEnter += WeaponSwitchHandler_OnMeleeRangeEnter;
        weaponSwitchHandler.OnMeleeRangeExit += WeaponSwitchHandler_OnMeleeRangeExit;
    }
    private void OnDisable()
    {
        weaponSwitchHandler.OnMeleeRangeEnter -= WeaponSwitchHandler_OnMeleeRangeEnter;
        weaponSwitchHandler.OnMeleeRangeExit -= WeaponSwitchHandler_OnMeleeRangeExit;
    }
    #endregion
    private void Start()
    {
        outMeleeRangeAlpha = indicatorRenderer.color.a;
    }

    private void WeaponSwitchHandler_OnMeleeRangeEnter()
    {
        float meleeAlpha = (float)inMeleeRangeAlpha / 255;
        indicatorRenderer.DOFade(meleeAlpha, fadeDuration);
    }
    private void WeaponSwitchHandler_OnMeleeRangeExit()
    {
        indicatorRenderer.DOFade(outMeleeRangeAlpha, fadeDuration);
    }

}
