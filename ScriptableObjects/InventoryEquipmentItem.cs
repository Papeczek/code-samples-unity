using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

namespace MoreMountains.TopDownEngine
{
    public class InventoryEquipmentItem : InventoryItem
    {
        [System.Serializable]
        public struct StatToAdd
        {
            public Stat stat;
            public StatType statType;
            public int amount;
            public bool useDefDesc;
            public string statDescription;

            public StatToAdd(Stat stat, int amount,bool useDefDesc ,string statDescription, StatType statType)
            {
                this.stat = stat;
                this.statType = statType;
                this.amount = amount;
                this.useDefDesc = useDefDesc;
                this.statDescription = statDescription;
                
            }
        }
        public List<StatToAdd> StatToAddList;
        public Rarity itemRarity;
        public Sprite itemTypeIcon;
        public enum StatType
        {
            Offensive,
            Deffesnive,
        }
        public enum Rarity
        {
            Common,
            Uncommon,
            Rare,
            Legendary
        }
    }
}