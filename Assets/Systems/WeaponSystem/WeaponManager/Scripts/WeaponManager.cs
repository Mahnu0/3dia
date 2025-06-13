using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] int initialWeaponToSelect;

    [Header("Debug")]
    [SerializeField] bool debugSelectNextWeapon;
    [SerializeField] bool debugSelectPrevWeapon;
    [SerializeField] bool debugShot;
    [SerializeField] bool debugStartShooting;
    [SerializeField] bool debugStopShooting;

    private void OnValidate()
    {
        if (debugSelectNextWeapon)
        {
            debugSelectNextWeapon = false;
            SelectNextWeapon();
        }
        if (debugSelectPrevWeapon)
        {
            debugSelectPrevWeapon = false;
            SelectPreviousWeapon();
        }
        if (debugShot)
        {
            debugShot = false;
            Shot();
        }
        if (debugStartShooting)
        {
            debugStartShooting = false;
            StartContinuousShooting();
        }
        if (debugStopShooting)
        {
            debugStopShooting = false;
            StopContinuousShooting();
        }
    }

    Weapon[] weapons;
    int currentWeaponIndex = -1;

    private void Awake()
    {
        weapons = GetComponentsInChildren<Weapon>();
    }

    private void Start()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        SelectWeapon(initialWeaponToSelect);
    }

    public void SelectWeapon(int weaponIndex)
    {
        if (weaponIndex < -1) 
            { weaponIndex = weapons.Length - 1; }
        if (weaponIndex >= weapons.Length) 
            { weaponIndex = -1; }

        if (currentWeaponIndex != -1)
        { 
            weapons[currentWeaponIndex].NotifyDeselected();
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }

        currentWeaponIndex = weaponIndex;

        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(true);
            weapons[currentWeaponIndex].NotifySelected();
        }

    }

    public bool HasSelectedWeapon() 
        { return currentWeaponIndex != -1;}

    public bool SelectedWeaponIsShotByShot()
    {
        return currentWeaponIndex != -1 ? 
            weapons[currentWeaponIndex].weaponType == Weapon.WeaponType.ShotByShot :
            false;
    }

    public void Shot()
    {
        if (currentWeaponIndex != -1) 
            { weapons[currentWeaponIndex].Shot(); }
    }

    public void StartContinuousShooting()
    {
        if (currentWeaponIndex != -1)
            { weapons[currentWeaponIndex].StartShooting(); }
    }

    public void StopContinuousShooting()
    {
        if (currentWeaponIndex != -1)
            { weapons[currentWeaponIndex].StopShooting(); }
    }

    public void SelectNextWeapon() { SelectWeapon(currentWeaponIndex + 1); }
    public void SelectPreviousWeapon() { SelectWeapon(currentWeaponIndex - 1); }

    public Weapon GetCurrentWeapon()
    {
        return currentWeaponIndex == -1 ? null :
            weapons[currentWeaponIndex];
    }
}
