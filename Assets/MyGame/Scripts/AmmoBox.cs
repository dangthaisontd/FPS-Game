using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("DangSon/AmmoBox")]
public class AmmoBox : MonoBehaviour
{
    public int amountAmo = 200;
    
    public enum AmmoType
    {
        RifleAmmo,
        PistolAmmo
    }
    public AmmoType ammoType;
}
