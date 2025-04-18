using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
public class DamageResistanceFromStats : DamageResistance
{
    public enum DamageType
    {
        Melee,
        Projectiles,
    }
    public DamageType damageType;
    protected void Start()
    {
        switch (damageType)
        {
            case DamageType.Melee:
                DamageMultiplier = 1 - (CharacterStatsManager.i.characterStats.MeleeArmor);
                break;
            case DamageType.Projectiles:
                DamageMultiplier = 1 - (CharacterStatsManager.i.characterStats.ProjectileArmor);
                break;
        }
        DamageMultiplier += CharacterStatsManager.i.characterStats.Armor;
    }
}
