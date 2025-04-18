using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSO : ScriptableObject
{
    public string skillName;
    [Space,TextArea]
    public string skillDescription;
    [Space]
    public Sprite icon;
    [Header("Skill prefab containing ISkill")]
    public GameObject skillPrefab;
    [Space]
    public bool canBeStacked = true;
}
