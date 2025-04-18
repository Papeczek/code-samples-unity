using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdSlashStunSkill : MonoBehaviour,ISkill
{
    public void Activate()
    {
        StunEnabled();
    }

    private void StunEnabled()
    {
        SkillsActivationHandler.i.ThirdSlashStunSkillUnlocked();
    }
}
