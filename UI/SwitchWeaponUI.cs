using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWeaponUI : MonoBehaviour
{
    [SerializeField] private Image swordImage;
    [SerializeField] private Image crossbowImage;

    private void Start()
    {
        swordImage.enabled = crossbowImage.enabled = false;
    }
    private void Show(WeaponManager.WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponManager.WeaponType.Unarmed:
                swordImage.enabled = crossbowImage.enabled = false;
                break;
            case WeaponManager.WeaponType.Crossbow:
                swordImage.enabled = false;
                crossbowImage.enabled = true;
                break;
            case WeaponManager.WeaponType.Sword:
                swordImage.enabled = true;
                crossbowImage.enabled = false;
                break;
        }
    }

    private void OnEnable()
    {
        WeaponManager.OnWeaponEquipped += Show;
    }
    private void OnDisable()
    {
        WeaponManager.OnWeaponEquipped -= Show;
    }
}
