using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class WeaponManager : MonoBehaviour,MMEventListener<MMGameEvent>
{
    public static event Action<WeaponType> OnWeaponEquipped;

    [SerializeField] private CharacterHandleWeapon characterHandleWeapon;

    [SerializeField] private Weapon crossbowPrefab;
    [SerializeField] private Weapon swordPrefab;
    [SerializeField] private Weapon unarmedPrefab;

    [SerializeField] private Transform spawnPointBelowGround;

    public Weapon crossbow { get; private set; }
    public Weapon sword { get; private set; }
    public Weapon unarmed { get; private set; }

    [HideInInspector] public Weapon skillWeapon;

    [field:SerializeField]public bool powerSlashReady { get; private set; } = false;

    public enum WeaponType
    {
        Unarmed,
        Crossbow,
        Sword,
        Skill
    }
    private WeaponType currentWeaponType;
    public static WeaponManager i { get; private set; }
    private void Awake()
    {
        i = this;
        crossbow = Instantiate(crossbowPrefab,spawnPointBelowGround);
        crossbow.gameObject.SetActive(false);
        sword = Instantiate(swordPrefab, spawnPointBelowGround);
        sword.gameObject.SetActive(false);
        unarmed = characterHandleWeapon.InitialWeapon;
       
    }
    public void SetWeaponInQueue(WeaponType weaponType)
    {
        currentWeaponType = weaponType;
    }
    public void EquipQueuedWeapon()
    {
        switch (currentWeaponType)
        {
            case WeaponType.Unarmed:
                characterHandleWeapon.ChangeWeapon(unarmed, "Unarmed");
                break;
            case WeaponType.Crossbow:
                crossbow.gameObject.SetActive(true);
                characterHandleWeapon.ChangeWeapon(crossbow, "Crossbow");
                crossbow.gameObject.SetActive(false);
                break;
            case WeaponType.Sword:
                characterHandleWeapon.ChangeWeapon(sword, "Sword");
                break;
            case WeaponType.Skill:
                characterHandleWeapon.ChangeWeapon(skillWeapon, "SkillWeapon");
                break;
        }
        characterHandleWeapon.CurrentWeapon.gameObject.SetActive(true);
        OnWeaponEquipped?.Invoke(currentWeaponType);
    }
    public void EquipPassedWeaponType(WeaponType weaponType)
    {
        currentWeaponType = weaponType;
        EquipQueuedWeapon();
    }
    //public void EquipPassedWeapon(Weapon weapon, string weaponID)
    //{
    //    characterHandleWeapon.ChangeWeapon(weapon, weaponID);
    //}
    public void UnequipWeapon()
    {
        characterHandleWeapon.ChangeWeapon(null, null);
    }
    public WeaponType GetCharacterCurrentWeaponType()
    {
        return currentWeaponType;
    }
    public Weapon GetCharacterEquippedWeapon()
    {
        return characterHandleWeapon.CurrentWeapon;
    }
    public CharacterHandleWeapon GetCharacterHandleWeapon()
    {
        return characterHandleWeapon;
    }

    public void OnMMEvent(MMGameEvent eventType)
    {
        if(eventType.EventName == GameEvents.POWER_SLASH_READY)
        {
            powerSlashReady = true;
        }
        if (eventType.EventName == GameEvents.POWER_SLASH_USED)
        {
            powerSlashReady = false;
        }
    }
    private void OnEnable()
    {
        this.MMEventStartListening();
    }
    private void OnDisable()
    {
        this.MMEventStopListening();
    }
}
