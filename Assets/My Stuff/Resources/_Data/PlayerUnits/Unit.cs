using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Unit : ScriptableObject {
    [SerializeField] GameObject unitPrefab;
    [SerializeField] Sprite unitImage;
    [SerializeField] int unitCost;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectPrefab()
    {
        FindObjectOfType<UiManagerAndSpawner>().selectedUnit = this;
    }

    public GameObject GetFighterPrefab()
    {
        return unitPrefab;
    }

    public Sprite GetFighterImage()
    {
        return unitImage;
    }
    public int GetFighterCost()
    {
        return unitCost;
    }
}
