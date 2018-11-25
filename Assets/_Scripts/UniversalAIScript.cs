using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class UniversalAIScript : MonoBehaviour {

    [SerializeField] float attackDistance;
    [SerializeField] float damagePerHit;
    [SerializeField] GameObject currentTarget;
    [SerializeField] GameObject projectilePrefab;
    public float timeBetweenAttacks = 5;

    public GameObject selectedTarget;
    public float timeToAttack = 1;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        selectedTarget = FindObjectOfType<GameMasterScript>().selectedTarget;
        SetTargetForUnit();
	}

    private void SetTargetForUnit()
    {
        if (gameObject.tag == "Enemy")
        {
            SetTargetForEnemyUnits();
        }
        if (gameObject.tag == "Player")
        {
            SetTargetForPlayerUnits();
        }
    }

    private bool AttackClosestTarget()
    {
        timeToAttack -= Time.deltaTime;
        var objectsWithinRange = Physics.OverlapSphere(transform.position, attackDistance);
        foreach (var singleObject in objectsWithinRange)
        {
            if ((singleObject.tag == "Enemy" && gameObject.tag == "Player") || (singleObject.tag == "Player" && gameObject.tag == "Enemy"))
            {
                if (singleObject.gameObject.GetComponentInParent<HealthSystem>())
                {
                    currentTarget = singleObject.gameObject;
                    if (GetComponent<AICharacterControl>())
                    {
                        GetComponent<AICharacterControl>().SetTarget(singleObject.transform);
                    }
                    if (timeToAttack <= 0)
                    {
                        AttackTarget();
                    }
                    return true;
                }
            }

        }
        return false;
    }

    private void AttackTarget()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectilePrefab.GetComponent<Projectile>().SetTarget(currentTarget);
        timeToAttack = timeBetweenAttacks;
    }

    private void SetTargetForPlayerUnits()
    {
            if (selectedTarget)
            {
               if (!AttackClosestTarget())
                {
                    if (GetComponent<AICharacterControl>())
                    {
                        GetComponent<AICharacterControl>().SetTarget(selectedTarget.transform);
                    }
                }
            }
            else
            {
                AttackClosestTarget();
            }
        
    }

    private void SetTargetForEnemyUnits()
    {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                if (!AttackClosestTarget())
                {
                    if (GetComponent<AICharacterControl>())
                    {
                        GetComponent<AICharacterControl>().SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
                    }
                }
            }
        
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}   

