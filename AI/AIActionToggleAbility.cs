using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class AIActionToggleAbility : AIAction
{
    [SerializeField] private CharacterAbility characterAbility;
    private int uses = 0;

    public override void PerformAction()
    {
        if (uses > 0) return;
        if (!characterAbility.AbilityPermitted)
            PermitAbility();
        else
            UnpermitAbility();
        uses++;
    }

    public void PermitAbility()
    {
      //  if (characterAbility.AbilityPermitted) return;
        characterAbility.PermitAbility(true);
    }
    public void UnpermitAbility()
    {
      //  if (!characterAbility.AbilityPermitted) return;
        characterAbility.PermitAbility(false);
    }

    public override void OnExitState()
    {
        base.OnExitState();
        uses = 0;
    }
}
