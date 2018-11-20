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

    public GameObject selectedTarget;



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
        var objectsWithinRange = Physics.OverlapSphere(transform.position, attackDistance);
        foreach (var singleObject in objectsWithinRange)
        {
            if ((singleObject.tag == "Enemy" && gameObject.tag == "Player") || (singleObject.tag == "Player" && gameObject.tag == "Enemy"))
            {
                if (singleObject.gameObject.GetComponentInParent<HealthSystem>())
                {
                    AttackTarget(singleObject.gameObject);
                    if (GetComponent<AICharacterControl>())
                    {
                        gameObject.GetComponent<AICharacterControl>().SetTarget(singleObject.transform);
                    }
                    return true;
                }
            }

        }
        return false;
    }

    private void AttackTarget(GameObject target)
    {
        target.gameObject.GetComponent<HealthSystem>().currentHealth -= damagePerHit;
        Debug.Log(gameObject + " is attacking " + target.gameObject);
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

