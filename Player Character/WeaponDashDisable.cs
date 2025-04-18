using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;


public class WeaponDashDisable : MonoBehaviour, MMEventListener<MMGameEvent>
{
    //Merge with WeaponSwitchHandler

    [SerializeField] private GameObject attachedWeaponModel;
    public void OnMMEvent(MMGameEvent eventType)
    {
        //    if (eventType.EventName == GameEvents.DASH_START || eventType.EventName == GameEvents.PLAYER_STUNNED)
        //    {
        //        if (WeaponManager.i.GetCharacterEquippedWeapon().name!=WeaponManager.i.unarmed.name)
        //        {
        //            if(WeaponManager.i.skillWeapon == null)
        //            {
        //                WeaponManager.i.UnequipWeapon();
        //            }
        //            else
        //            {
        //                if (WeaponManager.i.GetCharacterEquippedWeapon().name != WeaponManager.i.skillWeapon.name)
        //                {
        //                    WeaponManager.i.UnequipWeapon();
        //                }
        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //}
        //private void OnEnable()
        //{
        //    this.MMEventStartListening();
        //}
        //private void OnDisable()
        //{
        //    this.MMEventStopListening();
        //}
    }
}
