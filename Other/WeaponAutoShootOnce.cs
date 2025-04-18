using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
public class WeaponAutoShootOnce : WeaponAutoShoot
{
	private int uses = 0;
	private bool canAttack = true;
	protected override void Start()
	{
		base.Start();
		uses = 0;
	}

	protected override void LateUpdate()
	{
		if (uses == 0&& canAttack)
		{
			base.LateUpdate();
		}
	}
	protected override void HandleAutoShoot()
	{
		if (!_hasWeaponAndAutoAim)
		{
			return;
		}

		if (_weaponAutoAim.Target != null)
		{
			if (_lastTarget != _weaponAutoAim.Target)
			{
				_targetAcquiredAt = Time.time;
			}

			if (Time.time - _targetAcquiredAt >= DelayBeforeShootAfterAcquiringTarget)
			{
				_weapon.WeaponInputStart();
				uses++;
			}
			_lastTarget = _weaponAutoAim.Target;
		}
	}
}
