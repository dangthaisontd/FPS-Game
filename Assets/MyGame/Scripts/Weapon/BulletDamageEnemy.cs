using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("DangSon/BulletDamageEnemy")]
public class BulletDamageEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectHit)
    {
        if(objectHit != null)
        {
            if(objectHit.collider.CompareTag("Wall"))
            {
                CreateBulletInpactEffect(objectHit);
                Destroy(gameObject);
            }
        }
    }
    private void CreateBulletInpactEffect(Collision objectHit)
    {
       ContactPoint contact = objectHit.contacts[0];
       GameObject hole = Instantiate(GameReferences.Instance.bulletsEffectImpactPrefabs, contact.point, Quaternion.LookRotation(contact.normal));
       hole.transform.SetParent(objectHit.gameObject.transform);
    }
}
