using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

public class AIActionToggleAutoAim : AIAction
{
    [Header("Binding")]
    /// the CharacterHandleWeapon ability this AI action should pilot. If left blank, the system will grab the first one it finds.
    [Tooltip("the CharacterHandleWeapon ability this AI action should pilot.")]
    public CharacterHandleWeapon CharacterHandleWeaponAbility;
    [Tooltip("the CharacterOrientation3D ability this AI action should pilot.")]
    public CharacterOrientation3D CharacterOrientationAbility;
    [Space]
    [Space]
    [Tooltip("if set true, action will only turn Auto Aim on")]
    public bool setOnlyToTrue = false;


    protected Character _character;
    protected WeaponAutoAim3D _weaponAutoAim;
    protected int _change = 0;

    public override void Initialization()
    {
        if (!ShouldInitialize) return;
        base.Initialization();
        _character = GetComponentInParent<Character>();
        if (CharacterHandleWeaponAbility == null || CharacterOrientationAbility == null)
        {
            Debug.LogError("CharacterHandleWeapon or CharacterOrientation3D not assigned!");
        }
    }
    public override void PerformAction()
    {
        if (_weaponAutoAim == null)
        {
            _weaponAutoAim = CharacterHandleWeaponAbility.CurrentWeapon.gameObject.GetComponent<WeaponAutoAim3D>();
        }
        if (!setOnlyToTrue)
        {
            ToggleAutoAim();
        }
        else
        {
            TurnAutoAimOn();
        }
    }
    protected virtual void ToggleAutoAim()
    {
        if(_change < 1)
        {
            if(_weaponAutoAim!= null)
            {
                bool oppositeState = !_weaponAutoAim.enabled;
                _weaponAutoAim.enabled = oppositeState;
                CharacterOrientationAbility.PermitAbility(oppositeState);
            }
            _change++;
        }
    }
    protected virtual void TurnAutoAimOn()
    {
        if(_change < 1)
        {
            if (_weaponAutoAim != null)
            {
                if (!CharacterOrientationAbility.AbilityPermitted)
                {
                  //  print("czy sie wywoluje");
                    _weaponAutoAim.enabled = true;
                    CharacterOrientationAbility.PermitAbility(true);
                }
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
