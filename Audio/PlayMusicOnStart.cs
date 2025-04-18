using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
public class PlayMusicOnStart : MonoBehaviour
{
    [SerializeField] private MMF_Player musicPlayer;
    void Start()
    {
        if(musicPlayer == null)
        {
            musicPlayer = GetComponent<MMF_Player>();
        }
        if (SettingsManager.i.MusicOn())
        {
            musicPlayer.PlayFeedbacks();
        }
        else
        {
            musicPlayer.PlayFeedbacks();
            MMSoundManager.Instance.PauseTrack(MMSoundManager.MMSoundManagerTracks.Music);
        }
    }
}
