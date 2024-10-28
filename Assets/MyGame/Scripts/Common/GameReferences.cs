using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("DangSon/GameReferences")]
public class GameReferences : MonoBehaviour
{
    public GameObject bulletsEffectImpactPrefabs;
    private static GameReferences instance;
    public static GameReferences Instance
    {
        get => instance;
    }
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;    
    }
}
