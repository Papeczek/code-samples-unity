using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using UnityEngine.UI;
public class ToggleSoundTrackButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private MMSoundManager.MMSoundManagerTracks track;

    private void Awake()
    {
        if(button == null)
        {
            button = GetComponent<Button>();
        }
    }
    private void Start()
    {
        button.onClick.AddListener(()=> SettingsManager.i.ToggleSoundTrack(track));
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
