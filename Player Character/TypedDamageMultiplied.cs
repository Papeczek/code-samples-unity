using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
[System.Serializable]
public class TypedDamageMultiplied : TypedDamage
{
    public enum DamageType
    {
        Projectile,
        Melee,
        Ability
    }
    public DamageType damageType;
	protected float multiplier = 1f;
	public void GetMultiplier()
	{
		switch (damageType)
		{
			case DamageType.Melee:
				multiplier += CharacterStatsManager.i.characterStats.MeleeAttack;
				break;
			case DamageType.Projectile:
				multiplier += CharacterStatsManager.i.characterStats.ProjectileAttack;
				break;
			case DamageType.Ability:
				multiplier += CharacterStatsManager.i.characterStats.AbilityAttack;
				break;
		}
	}
	public override float DamageCaused
	{
		
		get
		{
			GetMultiplier();
			if (Time.frameCount != _lastRandomFrame)
			{
				_lastRandomValue = Random.Range(MinDamageCaused, MaxDamageCaused);
				_lastRandomFrame = Time.frameCount;
			}
			return _lastRandomValue * multiplier;
		}
	}
}
