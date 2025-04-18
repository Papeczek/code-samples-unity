using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;
public class AdditionalArrowSkill : MonoBehaviour, ISkill
{
    public void Activate()
    {
        AccesCrossbow();
    }

   protected void AccesCrossbow()
    {
        WeaponManager.i.EquipPassedWeaponType(WeaponManager.WeaponType.Unarmed);
        var weapon = WeaponManager.i.crossbow.GetComponent<ProjectileWeapon>();
        weapon.BurstLength++;   
    }
}
