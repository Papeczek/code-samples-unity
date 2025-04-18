using UnityEngine;
using MoreMountains.TopDownEngine;
public class ThirdSlashSkill : MonoBehaviour, ISkill
{
    [SerializeField] private SwordSkillSO swordSkillSO;
    public Weapon skillWeapon;    
    public void Activate()
    {
        UseSkillWeapon();
    }

    private void UseSkillWeapon()
    {
        skillWeapon = Instantiate(swordSkillSO.swordWeaponPrefab.GetComponent<Weapon>());
        WeaponManager.i.skillWeapon = skillWeapon;
        SkillsActivationHandler.i.ThirdSlashSkillUnlocked();   
    }
}
