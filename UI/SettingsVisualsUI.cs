using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SettingsVisualsUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup settingsCanvasGroup;
    [SerializeField] private Canvas settingsCanvas;
    [Space]
    [SerializeField] private float animDuration = 0.4f;
    [SerializeField] private Ease animEase;
    private void Start()
    {
        SettingsManager.i.HideSettingsEvent += Hide;
        SettingsManager.i.ShowSettingsEvent += Show;

        settingsCanvasGroup.DOFade(0f, 0f);
        settingsCanvas.enabled = false;
    }
    private void OnDestroy()
    {
        SettingsManager.i.HideSettingsEvent -= Hide;
        SettingsManager.i.ShowSettingsEvent -= Show;
    }
    public void Show()
    {
        UiSoundPlayer.i.PlayClick();
        settingsCanvas.enabled = true;
        settingsCanvasGroup.DOFade(1f, animDuration).SetEase(animEase);

    }
    public void Hide()
    {
        UiSoundPlayer.i.PlayClick();
        settingsCanvasGroup.DOFade(0f, animDuration).SetEase(animEase);
        settingsCanvas.enabled = false;
    }

}
