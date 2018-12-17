using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    [SerializeField] Image healthBar;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
          healthBar.fillAmount = gameObject.GetComponent<HealthSystem>().GetFillAmount();
	}
}
