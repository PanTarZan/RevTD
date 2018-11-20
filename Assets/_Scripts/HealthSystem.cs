using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    [SerializeField] float startingHealth = 100;

    public float currentHealth;
    public float fillAmount;

    // Use this for initialization
    void Start () {
        currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {

        fillAmount = currentHealth / startingHealth;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
	}

    public float GetFillAmount()
    {
        return fillAmount;
    }
}
