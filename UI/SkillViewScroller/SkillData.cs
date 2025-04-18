using System;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public Stat statType;
    public SkillCategory skillCategory;

  //  public Sprite iconSprite;
    public bool isSpecialAbility;
    public float skillValue;
    public string skillTitle;
    [TextArea]
    public string skillDescription;
    [Space]
    public bool unlocked;
    public bool canBeUnlocked;
    public int id;



    public enum SkillCategory
    {
        Melee,
        Deffense,
        Range
    }
   

    public SkillData(SkillData other)
    {
        this.statType = other.statType;
        this.skillCategory = other.skillCategory;
        this.isSpecialAbility = other.isSpecialAbility;
        this.skillValue = other.skillValue;
        this.skillTitle = other.skillTitle;
        this.skillDescription = other.skillDescription;
        this.unlocked = other.unlocked;
        this.canBeUnlocked = other.canBeUnlocked;
        this.id = other.id;
    }
    public string GetIconSprite()
    {
        string spriteName;
        switch (statType)
        {
            case Stat.Attack:
                spriteName = "icon_hammer";
                break;
            case Stat.MeleeAttack:
                spriteName = "icon_sword";
                break;
            case Stat.ProjectileAttack:
                spriteName = "icon_target";
                break;
            case Stat.AbilityAttack:
                spriteName = "icon_hat_green";
                break;
            case Stat.MaxHP:
                spriteName = "icon_heart_simple";
                break;
            case Stat.Armor:
                spriteName = "icon_shield";
                break;
            case Stat.ProjectileArmor:
                spriteName = "icon_helm_red";
                break;
            case Stat.MeleeArmor:
                spriteName = "icon_helm_purple";
                break;
            case Stat.MeleeAttackSpeed:
                spriteName = "icon_hat_purple";
                break;
            case Stat.RangeAttackSpeed:
                spriteName = "icon_hat_green";
                break;
            case Stat.CooldownReduction:
                spriteName = "icon_compass";
                break;
            case Stat.MovementSpeed:
                spriteName = "icon_potion_green";
                break;
            default:
                spriteName = "icon_scroll";
                break;
        }
        return spriteName;
    }
}
//switch (statType)
//{
//    case SkillType.ATK:
//        spriteName = "icon_hammer";
//        break;
//    case SkillType.MELEE_ATK:
//        spriteName = "icon_sword";
//        break;
//    case SkillType.RANGE_ATK:
//        spriteName = "icon_target";
//        break;
//    case SkillType.HP:
//        spriteName = "icon_heart_simple";
//        break;
//    case SkillType.DEF:
//        spriteName = "icon_shield";
//        break;
//    case SkillType.MELEE_DEF:
//        spriteName = "icon_helm_purple";
//        break;
//    case SkillType.RANGE_DEF:
//        spriteName = "icon_helm_red";
//        break;
//    case SkillType.MELEE_ATK_SPEED:
//        spriteName = "icon_hat_purple";
//        break;
//    case SkillType.RANGE_ATK_SPEED:
//        spriteName = "icon_hat_green";
//        break;
//    case SkillType.COOLDOWN_REDUCTION:
//        spriteName = "icon_compass";
//        break;
//    case SkillType.MOVEMENT_SPEED:
//        spriteName = "icon_potion_green";
//        break;
//    default:
//        spriteName = "icon_scroll";
//        break;
//}