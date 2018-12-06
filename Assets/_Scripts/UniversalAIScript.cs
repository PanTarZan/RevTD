using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class UniversalAIScript : MonoBehaviour {
    
    [SerializeField] float attackDistance;
    [SerializeField] float damagePerHit;
    [SerializeField] GameObject currentTarget;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] bool isTower;

    public float timeBetweenAttacks = 5;
    public GameObject selectedTarget;
    public float timeToAttack = 1;



    // Use this for initialization
    void Start () {
        //to set movement in one place for now
        if (GetComponent<NavMeshAgent>())
        {
            GetComponent<NavMeshAgent>().stoppingDistance = attackDistance - 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        ProcessTarget();
	}

    private void ProcessTarget()
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

    private void SetTargetForPlayerUnits()
    {
        selectedTarget = FindObjectOfType<GameMasterScript>().selectedTarget;
        if (selectedTarget)
        {
            currentTarget = selectedTarget;
            GoAndAttackTarget();
        }
    }

    private void SetTargetForEnemyUnits()
    {
        //find first available target
        //TODO change to focus always on MainBasePrefab
        if (isTower)
        {
            //attack closest enemy
            var objectsWithinRange = Physics.OverlapSphere(transform.position, attackDistance);
            foreach (var singleObject in objectsWithinRange)
            {
                if (singleObject.tag == "Player")
                {
                    currentTarget = singleObject.gameObject;
                    GoAndAttackTarget();
                    break;
                }
            }
        }
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            currentTarget = GameObject.FindGameObjectsWithTag("Player")[0];
            GoAndAttackTarget();
        }
    }

    private void GoAndAttackTarget()
    {
        //countdown to reduce cooldown
        timeToAttack -= Time.deltaTime;

        //tower does not walk :D (this works only for infrantry)
        if (GetComponent<AICharacterControl>())
        {
            GetComponent<AICharacterControl>().target = currentTarget.transform;
        }
        //target in range detector
        var objectsWithinRange = Physics.OverlapSphere(transform.position, attackDistance);
        foreach (var singleObject in objectsWithinRange)
        {
            if (singleObject.gameObject == currentTarget)
            {
                if (timeToAttack <= 0)
                {
                    AttackTarget();
                }
            }
        }
    }

    private void AttackTarget()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().SetTarget(currentTarget);
        timeToAttack = timeBetweenAttacks;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}   

