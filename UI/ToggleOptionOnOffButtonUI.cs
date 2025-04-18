using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleOptionOnOffButtonUI : MonoBehaviour
{
    [SerializeField] private Option option;
    [SerializeField] private ToggleType toggleType;

    [SerializeField] private Image checkmark;
    [SerializeField] private GameObject onGO;
    [SerializeField] private GameObject offGO;
    [SerializeField] private Button button;
    

    public bool On;
    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
    private IEnumerator Start()
    {
        button.onClick.AddListener(Toggle);
        yield return new WaitForSeconds(0.25f);
        switch (option)
        {
            case Option.Music:
                On = SettingsManager.i.MusicOn();
                break;
            case Option.Sound:
                On = SettingsManager.i.SoundOn();
                break;
            case Option.Vibration:
                On = SettingsManager.i.VibrationOn();
                break;
        }
        SetStartState();
    }

    private void Toggle()
    {
        float duration = 0.2f;
        if (On)
        {
            if(toggleType == ToggleType.Checkmark)
            {
                checkmark.DOFade(0f, duration);
            }
            onGO.SetActive(false);
            offGO.SetActive(true);
            print(UiSoundPlayer.i);
            UiSoundPlayer.i.PlayClick();
        }
        else
        {
            if (toggleType == ToggleType.Checkmark)
            {
                checkmark.DOFade(1f, duration);
            }
            offGO.SetActive(false);
            onGO.SetActive(true);
            UiSoundPlayer.i.PlayClick();
        }
        On = !On;
    }
    private void SetStartState()
    {
        if (On)
        {
            if (toggleType == ToggleType.Checkmark)
            {
                checkmark.DOFade(1f, 0f);
            }
            offGO.SetActive(false);
            onGO.SetActive(true);
        }
        else
        {
            if (toggleType == ToggleType.Checkmark)
            {
                checkmark.DOFade(0f, 0f);
            }
            offGO.SetActive(true);
            onGO.SetActive(false);
        }
    }
    private enum Option
    {
        Music,
        Sound,
        Vibration
    }
    private enum ToggleType
    {
        Checkmark,
        GameObjectSwap
    }
}
