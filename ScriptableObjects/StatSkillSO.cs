using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class StatSkillSO : SkillSO
{
    [Space]
    public Stat stat;
    [Space,Header("Amount in %")]
    public float amount;

}
