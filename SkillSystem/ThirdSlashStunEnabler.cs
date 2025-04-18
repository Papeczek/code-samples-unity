using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;


public class ThirdSlashStunEnabler : MonoBehaviour, MMEventListener<MMGameEvent>
{
    private const string THIRD_SLASH_TAG = "ThirdSlash";
    private const string THIRD_SLASH_STUN_ENABLE_EVENT = "ThirdSlashStunEnable";
    [SerializeField] private GameObject damageZoneGO;

    public void OnMMEvent(MMGameEvent eventType)
    {
       if(eventType.EventName == THIRD_SLASH_STUN_ENABLE_EVENT)
        {
            StunEnabled();
        }
    }
    public void StunEnabled()
    {
        damageZoneGO.tag = THIRD_SLASH_TAG;
        print("tag changed");
    }
    private void OnEnable()
    {
        this.MMEventStartListening();
    }
    private void OnDisable()
    {
        this.MMEventStopListening();
    }
}
