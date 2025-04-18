using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MoreMountains.Tools;

public class CharacterSkillManager : MonoBehaviour
{
    public event Action<float> OnLevelUp;
    public event Action<float> OnExpAdd;

    [SerializeField] private List<SkillSO> generalSkillList;
    [SerializeField] private List<SkillSO> crossbowSkillList;
    [SerializeField] private List<SkillSO> swordSkillList;

    private List<SkillSO> activatedSkillList;
    private List<List<SkillSO>> allSkillsList;

    public int characterLevel = 0;
    public float experience;
    public float lvlMaxExp = 7f;
    public static CharacterSkillManager i { get; private set; }
    private void Awake()
    {
        i = this;
        activatedSkillList = new List<SkillSO>();
        allSkillsList = new List<List<SkillSO>>();
        allSkillsList.Add(generalSkillList);
        allSkillsList.Add(crossbowSkillList);
        allSkillsList.Add(swordSkillList);
    }
    public void ActivateSkill(SkillSO skillSO)
    {
        skillSO.skillPrefab.GetComponent<ISkill>().Activate();
        activatedSkillList.Add(skillSO);
        //If skill can't be stacked, remove from list
        print(skillSO);
        if (!skillSO.canBeStacked)
        {
            foreach (List<SkillSO> skillSOList in allSkillsList)
            {
                bool foundSkill = false;
                foreach (SkillSO skill in skillSOList)
                {
                    if(skill == skillSO)
                    {
                        foundSkill = true;
                        skillSOList.Remove(skill);
                        break;
                    }
                }
                if (foundSkill)
                    break;
            }
        }
    }
    public void AddExp(float exp)
    {
        experience += exp;
        OnExpAdd?.Invoke(experience);
        if(experience >= lvlMaxExp)
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        characterLevel++;
        experience = 0f;
        OnLevelUp?.Invoke(lvlMaxExp);
        Invoke("SelectNewAbility",1f);
    }
    public void SelectNewAbility()
    {
        MMGameEvent.Trigger("PickAbility");
    }
    public SkillSO GetNextSkillSOFromList(SkillCategory skillCategory)
    {
        if (skillCategory == SkillCategory.General)
            return generalSkillList[0];
        else if (skillCategory == SkillCategory.Sword)
            return swordSkillList[0];
        else if (skillCategory == SkillCategory.Crossbow)
            return crossbowSkillList[0];
        else
            return null;
    }
    public SkillSO GetRandomGeneralSkill()
    {
        return generalSkillList[UnityEngine.Random.Range(0,generalSkillList.Count)];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
      //      SelectNewAbility();
        }
    }
    public enum SkillCategory
    {
        General,
        Sword,
        Crossbow
    }
}
