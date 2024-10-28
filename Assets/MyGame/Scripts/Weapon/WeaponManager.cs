using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
[AddComponentMenu("DangSon/WeaponManager")]
public class WeaponManager : MonoBehaviour
{
    
    private static WeaponManager instance;
    public static WeaponManager Instance
    {
        get => instance;
    }
    [Header("Ammo Weapon")]
    public int totalRifleAmmo = 0;
    public int totalPistolAmmo = 0;
    [Header("Select Weapon")]
    public List<GameObject> weaponSlot = new List<GameObject>();
    public GameObject activeWeaponSlot;
    public int maxTotalAmmo = 200;
    private void Awake()
    {
        if(instance != null) 
            {
                DestroyImmediate(gameObject);
                return;
            }
        instance = this;
    }
    private void Start()
    {
        activeWeaponSlot = weaponSlot[1];
        ActiveWeaponModel();
    }
    public int GetRifleAmmo()
    {
        return totalRifleAmmo;
    }
    public int GetPistolAmmo()
    {
        return totalPistolAmmo;
    }
    public int GetMaxAmmo()
    {
        return maxTotalAmmo;
    }
    private void ActiveWeaponModel()
    {
        foreach (var item in weaponSlot)
        {
            if(item == activeWeaponSlot)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }
    private void Update()
    {
        Getkey();
    }

    private void Getkey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveWeaponSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveWeaponSlot(1);
        }
    }

    private void SwitchActiveWeaponSlot(int slotNumber)
    {
        activeWeaponSlot = weaponSlot[(int)slotNumber];
        ActiveWeaponModel();
    }

    public int CheckAmmoLeftFor(Weapon.WeaponModel1 model1)
    {
        switch (model1)
        {
            case Weapon.WeaponModel1.AK:
                return totalRifleAmmo;
                break;
            case Weapon.WeaponModel1.Pistol:
                return totalPistolAmmo;
                break;
            default:
                break;
        }

        return 0;
    }
    public void DecreseTotalAmmo(int bulletsleft, Weapon.WeaponModel1 thisWeaponModel)
    {
        switch (thisWeaponModel)
        {
            case Weapon.WeaponModel1.AK:
                totalRifleAmmo -= bulletsleft;
                break;
            case Weapon.WeaponModel1.Pistol:
                totalPistolAmmo-= bulletsleft;
                break;
            default:
                break;
        }
    }

    public void PikUpAmmo(AmmoBox ammo)
    {
        switch (ammo.ammoType)
        {
            case AmmoBox.AmmoType.RifleAmmo:
                totalRifleAmmo += ammo.amountAmo;
                break;
            case AmmoBox.AmmoType.PistolAmmo:
                totalPistolAmmo += ammo.amountAmo;
                break;
            default:
                break;
        }
        if (totalRifleAmmo > maxTotalAmmo)
        {
            totalRifleAmmo = maxTotalAmmo;
        }
        if(totalPistolAmmo>maxTotalAmmo)
        {
            totalPistolAmmo = maxTotalAmmo;
        }
    }

    public void PickUpWeapon(GameObject objectHitRaycast)
    {
       AddWeaponActiveSlot(objectHitRaycast);
    }

    private void AddWeaponActiveSlot(GameObject objectHitRaycast)
    {
        DropCurrentWeapon(objectHitRaycast); //ha vu khi tren tay xuong 
        PickCurrentWeapon(objectHitRaycast);  //lay vu khi kia len
    }

    private void PickCurrentWeapon(GameObject pickUpWeapon)
    {
        pickUpWeapon.transform.SetParent(activeWeaponSlot.transform, false);
        
        Weapon weapon = pickUpWeapon.GetComponent<Weapon>();
        weapon.isActiveWeapon = true;
        weapon.anim.enabled = true;
        pickUpWeapon.transform.localPosition = weapon.localPositionGun;
    }

    private void DropCurrentWeapon(GameObject pickUpWeapon)
    {
      if(activeWeaponSlot.transform.childCount>0)
        {
            var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;
            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false;
            weaponToDrop.GetComponent<Weapon>().anim.enabled = false;
            weaponToDrop.transform.SetParent(pickUpWeapon.transform.parent);
            weaponToDrop.transform.localPosition = pickUpWeapon.transform.localPosition;
            weaponToDrop.transform.localRotation = pickUpWeapon.transform.localRotation;         
        }
    }
}
