using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

namespace MoreMountains.TopDownEngine
{
[CreateAssetMenu]
[System.Serializable]
    public class InventoryEquipment : InventoryEquipmentItem
    {
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
}