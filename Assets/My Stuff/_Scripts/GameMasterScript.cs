using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameMasterScript : MonoBehaviour {

    [SerializeField] GameObject raycastingPlayerPrefab;
    [SerializeField] GameObject basePrefab;
    [SerializeField] int money;

    Ray rayFromCamera;
    RaycastHit hitFromCamera;
    Ray playerRay;
    RaycastHit playerHitsObject;
    public GameObject selectedBuilding;
    public GameObject selectedTarget;


    // Use this for initialization
    void Start()
    {
        selectedBuilding = basePrefab;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastForClickable();
        RaycastFromPlayer();
    }


    private void RaycastForClickable()
    {
        rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(rayFromCamera, out hitFromCamera, 999))
        {
            if (hitFromCamera.collider.gameObject.layer == 10 && money > 0)
            {
                BuildSelectedBuilding();
            }

            if (hitFromCamera.collider.gameObject.tag == "Enemy")
            {
                SelectTarget();
            }
        }
    }

    private void SelectTarget()
    {
        //select enemy targets with rmb
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (hitFromCamera.collider.gameObject.tag == "Enemy")
            {
                selectedTarget = hitFromCamera.collider.gameObject;
            }
        }
    }

    private void BuildSelectedBuilding()
    {
        // lpm for selecting and building
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(selectedBuilding, hitFromCamera.point, hitFromCamera.transform.rotation);
            money--;
        }
    }

    private void RaycastFromPlayer()
    {
        playerRay.origin = raycastingPlayerPrefab.transform.position + Vector3.up;
        playerRay.direction = raycastingPlayerPrefab.transform.forward;


        if (Physics.Raycast(playerRay, out playerHitsObject, 5))
        {


            if (playerHitsObject.collider.GetComponent<FightModeTrigger>() != null)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    playerHitsObject.transform.gameObject.GetComponent<FightModeTrigger>().StartFight();
                }
            }
            else
            {
                Debug.Log("Im Hitting Everything Else");
            }
        }
    }
    
}
