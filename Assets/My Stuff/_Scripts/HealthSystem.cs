using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    [SerializeField] float startingHealth = 100;
    [SerializeField] int moneyUponDeath;
    GameMasterScript gamemaster;

    public float currentHealth;
    public float fillAmount;

    // Use this for initialization
    void Start () {
        currentHealth = startingHealth;
        gamemaster = FindObjectOfType<GameMasterScript>();
    }
	
	// Update is called once per frame
	void Update () {

        fillAmount = currentHealth / startingHealth;
        if (currentHealth <= 0)
        {
            gamemaster.money += moneyUponDeath;
            Destroy(gameObject);
        }
	}

    public float GetFillAmount()
    {
        return fillAmount;
    }
}
