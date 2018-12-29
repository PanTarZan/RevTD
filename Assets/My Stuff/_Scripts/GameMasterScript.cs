using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class GameMasterScript : MonoBehaviour {
    
    [SerializeField] public int money;
    [SerializeField] GameObject moneyPanel;

    Ray rayFromCamera;
    RaycastHit hitFromCamera;
    public GameObject selectedTarget;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastForClickable();
        DisplayMoney();
    }

    private void DisplayMoney()
    {
        moneyPanel.GetComponentInChildren<Text>().text = "Money: "+money.ToString();
    }

    private void RaycastForClickable()
    {
        rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(rayFromCamera, out hitFromCamera, 999))
        {
            if (hitFromCamera.collider.gameObject.tag == "Enemy")
            {
                //consider onhover
                //hitFromCamera.collider.GetComponentInChildren<UnitSelector>().SelectionMarkerOnHover();

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    selectedTarget = hitFromCamera.collider.gameObject;
                }
            }
        }
    }
    
}
