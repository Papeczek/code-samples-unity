using UnityEngine;
using System;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
public class SettingsManager : MonoBehaviour
{
    public event Action ShowSettingsEvent;
    public event Action HideSettingsEvent;
    public static SettingsManager i { get; private set; }
    [SerializeField] private MMF_Player musicSoundResume;
    private bool settingsOn = false;
    private void Awake()
    {
        i = this;
        settingsOn = false;
        Application.targetFrameRate = 60;
    }
    public void ToggleSoundTrack(MMSoundManager.MMSoundManagerTracks track)
    {
        if(track == MMSoundManager.MMSoundManagerTracks.Music)
        {
            if (MMSoundManager.Instance.IsMuted(MMSoundManager.MMSoundManagerTracks.Music))
            {
                MMSoundManager.Instance.UnmuteTrack(MMSoundManager.MMSoundManagerTracks.Music);
                musicSoundResume.PlayFeedbacks();
            }
            else
            {
                MMSoundManager.Instance.MuteTrack(MMSoundManager.MMSoundManagerTracks.Music);
                MMSoundManager.Instance.PauseTrack(MMSoundManager.MMSoundManagerTracks.Music);
            }
        }
        else
        {
            if (MMSoundManager.Instance.IsMuted(track))
            {
                MMSoundManager.Instance.UnmuteTrack(track);
                MMSoundManager.Instance.PlayTrack(track);
            }
            else
            {
                MMSoundManager.Instance.MuteTrack(track);
                MMSoundManager.Instance.PauseTrack(track);
            }
        }
    }
    public void ToggleVibrations()
    {

    }
    public void ToggleSettings()
    {
        if (!settingsOn)
        {
            ShowSettingsEvent?.Invoke();
        }
        else
        {
            HideSettingsEvent?.Invoke();
        }
        settingsOn = !settingsOn;
    }
    public bool MusicOn()
    {
        return !MMSoundManager.Instance.IsMuted(MMSoundManager.MMSoundManagerTracks.Music);
    }
    public bool SoundOn()
    {
        return !MMSoundManager.Instance.IsMuted(MMSoundManager.MMSoundManagerTracks.Sfx);
    }
    public bool VibrationOn()
    {
        return true;
    }
    
}
