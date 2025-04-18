using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using Kryz.CharacterStats;
using MoreMountains.InventoryEngine;
public static class CharacterStatsFunctions 
{
    public static void AddTemporaryStat(CharacterStats characterStats,Stat stat, float value)
    {
        float valueToPercents = value / 100f;
        switch (stat)
        {
            case Stat.MaxHP:
                characterStats.MaxHealth += value;
                break;
            case Stat.Attack:
                characterStats.Attack += valueToPercents;
                break;
            case Stat.MeleeAttack:
                characterStats.MeleeAttack += valueToPercents;
                break;
            case Stat.ProjectileAttack:
                characterStats.ProjectileAttack += valueToPercents;
                break;
            case Stat.AbilityAttack:
                characterStats.AbilityAttack += valueToPercents;
                break;
            case Stat.Armor:
                characterStats.Armor += valueToPercents;
                break;
            case Stat.ProjectileArmor:
                characterStats.ProjectileArmor += valueToPercents;
                break;
            case Stat.MeleeArmor:
                characterStats.MeleeArmor += valueToPercents;
                break;
        }
    }
    public static void AddPermamentStat(CharacterStats characterStats,Stat stat, float value)
    {
        float valueToPercents = value / 100f;
         switch (stat)
        {
            case Stat.MaxHP:
                characterStats.MaxHealth += value;
                break;
            case Stat.Attack:
                characterStats.Attack += valueToPercents;
                break;
            case Stat.MeleeAttack:
                characterStats.MeleeAttack += valueToPercents;
                break;
            case Stat.ProjectileAttack:
                characterStats.ProjectileAttack += valueToPercents;
                break;
            case Stat.AbilityAttack:
                characterStats.AbilityAttack += valueToPercents;
                break;
            case Stat.Armor:
                characterStats.Armor += valueToPercents;
                break;
            case Stat.ProjectileArmor:
                characterStats.ProjectileArmor += valueToPercents;
                break;
            case Stat.MeleeArmor:
                characterStats.MeleeArmor += valueToPercents;
                break;
        }
        SaveStats(characterStats);
    }
    public static void RemovePermamentStat(CharacterStats characterStats,Stat stat, float value)
    {
        float valueToPercents = value / 100f;
        
        switch (stat)
        {
            case Stat.MaxHP:
                characterStats.MaxHealth -= value;
                break;
            case Stat.Attack:
                characterStats.Attack -= valueToPercents;
                break;
            case Stat.MeleeAttack:
                characterStats.MeleeAttack -= valueToPercents;
                break;
            case Stat.ProjectileAttack:
                characterStats.ProjectileAttack -= valueToPercents;
                break;
            case Stat.AbilityAttack:
                characterStats.AbilityAttack -= valueToPercents;
                break;
            case Stat.Armor:
                characterStats.Armor -= valueToPercents;
                break;
            case Stat.ProjectileArmor:
                characterStats.ProjectileArmor -= valueToPercents;
                break;
            case Stat.MeleeArmor:
                characterStats.MeleeArmor -= valueToPercents;
                break;
        }
        SaveStats(characterStats);
    }
    public static void SaveStats(CharacterStats characterStats)
    {
        MMSaveLoadManager.Save(characterStats, "statsSave.txt", "statsSave");
        Debug.Log("Stats Saved");
    }
    public static CharacterStats LoadStats()
    {
        Debug.Log("Stats loaded");
        return (CharacterStats)MMSaveLoadManager.Load(typeof(CharacterStats), "statsSave.txt", "statsSave");
    }
    public static void DeleteSaveFolder()
    {
        MMSaveLoadManager.DeleteSaveFolder("statsSave");
    }
}

[System.Serializable]
public class CharacterStats
{
    public float MaxHealth;
    //In %: if armor + 5 it means all damage types recevied -5%
    public float Attack;
    public float MeleeAttack;
    public float ProjectileAttack;
    public float AbilityAttack;
    public float Armor;
    public float MeleeArmor;
    public float ProjectileArmor;

    public float MeleeAttackSpeed;
    public float RangeAttackSpeed;
    public float CooldownReduction;
    public float MovementSpeed;
}
[System.Serializable]
public enum Stat
{
    Attack,
    MeleeAttack,
    ProjectileAttack,
    AbilityAttack,
    MaxHP,
    Armor,
    MeleeArmor,
    ProjectileArmor,
    MeleeAttackSpeed,
    RangeAttackSpeed,
    CooldownReduction,
    MovementSpeed
}