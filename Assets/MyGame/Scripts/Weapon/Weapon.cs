using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum ShotingMode
    {
        Single,
        Burst,
        Auto
    }
    public enum WeaponModel1
    {
        AK,
        Pistol
    }
    [Header("Change Weapon")]
    public WeaponModel1 thisWeaponModel;
    public ShotingMode currentShotingMode =ShotingMode.Auto;
    public int magazineSize = 35;
    public bool allowReset;
    [Range(0f, 2f)]
    public float shootingdelay = 0.5f;
    [Range(0f, 2f)]
    public float spreadIntensity = 2f;
    public bool isActiveWeapon;
    [Header("Bullets Prefabs")]
    public GameObject bulletPrefabs;
    public Transform bulletSpawm;
    public float bulletVelocity = 100f;
    public float distanceBullet=200f;
    // Start is called before the first frame update
    public float bulletPrefbsTime =3f;
    //
    [Header("Fx Muzespash")]
    public ParticleSystem muzlerFlash;
    [Header("Audio Weapon")]
    public AudioClip shootClip;
    public AudioClip reloadClip;
    [Header("Store Gun")]
    public Vector3 localPositionGun;
    public Vector3 localRoationGun;
    //private
    private int bulletsleft;
    private bool isShoting, readyToShoot;
    private int isShotingId;
    private int isReloadingId;
    private int isReloadRightId;
    private int isReloadLeftId;
    [HideInInspector] public Animator anim;
    private bool isReloading=false;
    private void Awake()
    {
        bulletsleft = magazineSize;
        readyToShoot = true;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        isShotingId = Animator.StringToHash("IsShoting");
        isReloadingId = Animator.StringToHash("isReload");
        isReloadRightId = Animator.StringToHash("isReloadRight");
        isReloadLeftId = Animator.StringToHash("isReloadLeft");
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveWeapon)
        {

            if (currentShotingMode == ShotingMode.Auto)
            {
                isShoting = Input.GetKey(KeyCode.Mouse0);
            }
            else if ((currentShotingMode == ShotingMode.Burst) || (currentShotingMode == ShotingMode.Single))
            {
                isShoting = Input.GetKeyDown(KeyCode.Mouse0);
            }
            if (isShoting && readyToShoot && bulletsleft > 0)
            {
                FireWeapon();
            }
            if (Input.GetKeyDown(KeyCode.R) && bulletsleft < magazineSize && !isReloading && WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > 0)
            {
                Reload();
            }
        }
    }

    private void Reload()
    {
        int ran = UnityEngine.Random.Range(0, 3);
        switch(ran)
        {
            case 0:
                anim.SetTrigger(isReloadingId);
                break;
            case 1:
                anim.SetTrigger(isReloadRightId);
                break;
            case 2:
                anim.SetTrigger(isReloadLeftId);
                break;
        }
        AudioManager.Instance.PlaysfxPlayer(reloadClip);
        Invoke("ReloadCompleted",0);
    }
    private void ReloadCompleted()
    {
        if(WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel)>magazineSize)
        {
            bulletsleft = magazineSize;
            WeaponManager.Instance.DecreseTotalAmmo(bulletsleft, thisWeaponModel);
        }
        else
        {
            bulletsleft = WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel);
            WeaponManager.Instance.DecreseTotalAmmo(bulletsleft, thisWeaponModel);
        }
    }
    private void FireWeapon()
    {
        readyToShoot = false;
        bulletsleft--;
        AudioManager.Instance.PlaysfxPlayer(shootClip);
        anim.SetTrigger(isShotingId);
        muzlerFlash.Play();
        Vector3 shotingDirection = CaculateDirectionAndSpred().normalized;
        GameObject bullet = Instantiate(bulletPrefabs, bulletSpawm.position, Quaternion.identity);
        bullet.transform.forward = shotingDirection;
        //bullet.GetComponent<Rigidbody>().AddForce(shotingDirection * bulletVelocity, ForceMode.Impulse);
        bullet.GetComponent<Rigidbody>().velocity = shotingDirection*bulletVelocity*Time.deltaTime;
        StartCoroutine(DestroyBulletAfterTime(bullet,bulletPrefbsTime));
        if(allowReset)
        {
            Invoke("ResetShot", shootingdelay);
            allowReset = false;
        }
        else
        {
            if (bulletsleft > 0)
            {
                Invoke("FireWeapon", shootingdelay);
            }
        }
    }
    void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }
    IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletsTime)
    {
        yield return new WaitForSeconds(bulletsTime);
        Destroy(bullet);
    }
    private Vector3 CaculateDirectionAndSpred()
    {
        Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
          targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(distanceBullet);
        }
        Vector3 direction = targetPoint - bulletSpawm.position;
        float z = UnityEngine.Random.Range(-spreadIntensity,spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity,spreadIntensity);
        return direction + new Vector3(0,y,z);
    }
}
