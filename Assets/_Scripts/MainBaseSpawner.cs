using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBaseSpawner : MonoBehaviour {

    [SerializeField] GameObject spawnLocation;
    [SerializeField] GameObject fighterPrefab;

    public bool canISpawn = true;


	// Use this for initialization
	void Start () {
        InvokeRepeating("spawnFighter", 4, 4);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void spawnFighter()
    {
        if (canISpawn)
        {
        Debug.Log("Spawning...");
        Instantiate(fighterPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
        }
    }
}
