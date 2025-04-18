using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class WeaponModelPlayerAdjusted : WeaponModel
{

    [SerializeField] private bool inverseActivation = false;
    [SerializeField] private bool toggleGameobject = true;
    public override void Show(CharacterHandleWeapon handleWeapon)
    {
        if (!toggleGameobject) return;
        if (!inverseActivation)
            base.Show(handleWeapon);
        else
            base.Hide();
    }
    public override void Hide()
    {
        if (!toggleGameobject) return;
        if (!inverseActivation)
            base.Hide();
        else
            TargetModel.SetActive(true);

    }

}
