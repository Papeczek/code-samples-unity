using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using Kryz.CharacterStats;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;

[System.Serializable]
public class CharacterStatsManager : MonoBehaviour
{
    public CharacterStats characterStats;
    public bool BigAttackTest = false;
    public bool assignStats = true;
    public static CharacterStatsManager i { get; private set; }

    private void Awake()
    {
        if (i != null && i != this)
        {
            Destroy(this);
        }
        else
        {
            i = this;
        }
        if (!assignStats) return;
        if (characterStats.MaxHealth == 0)
        {
            characterStats = CharacterStatsFunctions.LoadStats();
            if(BigAttackTest)
                characterStats.Attack = 50;
            if (characterStats == null)
            {
                AssignBaseStats();
                print("MaxHealth = " + characterStats.MaxHealth);
            }
        }
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {

            //characterStats.Attack.RemoveModifier(new StatModifier(10, StatModType.Flat));
        }
    }
    private void AssignBaseStats()
    {
        characterStats = new CharacterStats();
        characterStats.MaxHealth = 500;
        characterStats.Attack = 0;
        characterStats.Armor = 0;
        characterStats.MeleeAttack = 0;
        characterStats.MeleeArmor = 0;
        characterStats.ProjectileAttack = 0;
        characterStats.ProjectileArmor = 0;
        characterStats.AbilityAttack = 0;
        CharacterStatsFunctions.SaveStats(characterStats);

    }
    //Adds permament stat when skill from skill tree is unlocked
    public void SkillUnlocked(SkillData skill)
    {
        CharacterStatsFunctions.AddPermamentStat(characterStats, skill.statType, skill.skillValue);
    }
    //Adds permament stat when item is equipped
    public void ItemEquipped(InventoryEquipmentItem item)
    {
         
        for (int i = 0; i < item.StatToAddList.Count; i++)
        {
            CharacterStatsFunctions.AddPermamentStat(characterStats, item.StatToAddList[i].stat, item.StatToAddList[i].amount);
        }
        StartCoroutine(InventorySaveDelayed());
    }
    public void ItemUnEquipped(InventoryEquipmentItem item)
    {
        for (int i = 0; i < item.StatToAddList.Count; i++)
        {
            CharacterStatsFunctions.RemovePermamentStat(characterStats, item.StatToAddList[i].stat, item.StatToAddList[i].amount);
        }
        StartCoroutine(InventorySaveDelayed());
    }
    private IEnumerator InventorySaveDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        print("saved");
        MMEventManager.TriggerEvent(new MMGameEvent("Save"));
    }
}

