using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSkill : MonoBehaviour, ISkill
{
    [SerializeField] private StatSkillSO statSkillSO;

    public void Activate()
    {
        AddStat(statSkillSO.stat, statSkillSO.amount);   
    }
    protected void AddStat(Stat stat,float amount)
    {
        CharacterStatsFunctions.AddTemporaryStat(CharacterStatsManager.i.characterStats,stat, amount);
    }
}
