using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class UiSoundPlayer : MonoBehaviour
{
    [SerializeField] private MMF_Player barClick;
    [SerializeField] private MMF_Player click;
    public static UiSoundPlayer i { get; private set; }
    private void Awake()
    {
        if (i != null && i != this)
        {
            Destroy(this);
        }
        else
        {
            i = this;
        }
        DontDestroyOnLoad(transform.gameObject);
    }
    public void PlayBarClick()
    {
        barClick.PlayFeedbacks();
    }
    public void PlayClick()
    {
        click.PlayFeedbacks();
    }
}
