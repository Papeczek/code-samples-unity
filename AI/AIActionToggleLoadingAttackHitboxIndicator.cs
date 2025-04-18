using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
public class AIActionToggleLoadingAttackHitboxIndicator : AIAction
{
    [Tooltip("the CharacterHandleWeapon ability this AI action should pilot.")]
    public CharacterHandleWeapon TargetHandleWeaponAbility;

    [Tooltip("how long loading indicator animation should take")]
    public float LoadingTime;

    protected Character _character;
    protected int _change = 0;
    protected bool isLoadingAttack = false;

    private LoadingAttackHitboxIndicator loadingAttackHI;
    public override void Initialization()
    {
        if (!ShouldInitialize) return;
        base.Initialization();

        _character = GetComponentInParent<Character>();
        if (TargetHandleWeaponAbility == null)
        {
            Debug.LogError("CharacterHandleWeapon not assigned!");
        }
    }
    public override void PerformAction()
    {
        ToggleLoadingAttack();
    }
    protected void ToggleLoadingAttack()
    {
        if (_change < 1)
        {
            if (loadingAttackHI == null)
            {
                loadingAttackHI = TargetHandleWeaponAbility.CurrentWeapon.gameObject.GetComponent<LoadingAttackHitboxIndicator>();
            }
            if (loadingAttackHI != null)
            {
            isLoadingAttack = !isLoadingAttack;
            loadingAttackHI.ToggleLoadingIndicator(LoadingTime, isLoadingAttack);
            }
            _change++;
        }
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        _change = 0;
    }
}
