using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using MoreMountains.TopDownEngine;


public class InventoryEquipmentWeapon : InventoryEquipmentItem
{
    [Tooltip("the weapon to equip")]
    public Weapon EquippableWeapon;
    public override bool Equip(string playerID)
    {
        CharacterStatsManager.i.ItemEquipped(this);
        return true;
    }
    public override bool UnEquip(string playerID)
    {
        CharacterStatsManager.i.ItemUnEquipped(this);
        return true;
    }
}
