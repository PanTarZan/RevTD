using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileRocket : ScriptableObject {

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileDamage;
    //[SerializeField] int unitCost;
    public GameObject GetProjectilePrefab()
    {
        return projectilePrefab;
    }
}
