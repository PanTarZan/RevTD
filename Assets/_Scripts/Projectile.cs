using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float damagePerHit = 0.15f;

    public GameObject target;
    public float speed = 1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            TravelToTarget();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void DoDamage(float damage)
    {
        target.gameObject.GetComponent<HealthSystem>().currentHealth -= damage;
        Destroy(gameObject);
    }

    public void TravelToTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        if (Vector3.Distance(transform.position, target.transform.position) <= 0.1f)
        {
            DoDamage(damagePerHit);
        }
    }
}
