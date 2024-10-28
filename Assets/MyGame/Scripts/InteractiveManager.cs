using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("DangSon/InteractiveManager")]
public class InteractiveManager : MonoBehaviour
{
    public static InteractiveManager Instance
    {
        get => instance;
    }
    private static InteractiveManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }
    [Header("Weapon")]
    public AmmoBox hoveredAmmo;
    public Weapon hoveredWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            GameObject objectHitRaycast = hit.transform.gameObject;
            ///Hom dan
            if (objectHitRaycast.GetComponent<AmmoBox>())
            {
             if(hoveredAmmo != null) 
                    {
                        hoveredAmmo.GetComponent<Outline>().enabled = false;
                    }
             hoveredAmmo = objectHitRaycast.GetComponent<AmmoBox>();
             hoveredAmmo.GetComponent<Outline>().enabled = true;
            if(Input.GetKeyDown(KeyCode.F))
                {
                    if(objectHitRaycast.CompareTag("AmmoBoxAK")&&(WeaponManager.Instance.GetRifleAmmo()<WeaponManager.Instance.GetMaxAmmo()))
                    {
                        //nhat hom dan
                        WeaponManager.Instance.PikUpAmmo(hoveredAmmo);
                        Destroy(objectHitRaycast);
                    }
                    if (objectHitRaycast.CompareTag("AmmoBoxPistol") && (WeaponManager.Instance.GetPistolAmmo() < WeaponManager.Instance.GetMaxAmmo()))
                    {
                        //nhat hom dan
                        WeaponManager.Instance.PikUpAmmo(hoveredAmmo);
                        Destroy(objectHitRaycast);
                    }
                }
            }
            else
            {
                if (hoveredAmmo)
                {
                    hoveredAmmo.GetComponent<Outline>().enabled = false;
                }
            }
            //Sung nhe
            if (objectHitRaycast.GetComponent<Weapon>())
            {
                if(hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
                hoveredWeapon = objectHitRaycast.GetComponent<Weapon>();
                hoveredWeapon.GetComponent <Outline>().enabled = true;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickUpWeapon(objectHitRaycast);
                }
            }
            else
            {
                if(hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}
