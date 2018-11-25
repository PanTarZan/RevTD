using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBaseSpawner : MonoBehaviour {

    [SerializeField] GameObject spawnLocation;
    [SerializeField] GameObject fighterPrefab;

    public bool canISpawn = true;
    public float firstSpawnTime = 1;
    public float spawnInterval = 10;


	// Use this for initialization
	void Start () {
        InvokeRepeating("spawnFighter", firstSpawnTime, spawnInterval);
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
