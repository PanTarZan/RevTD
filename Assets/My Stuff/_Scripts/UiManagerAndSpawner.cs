using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;

public class UiManagerAndSpawner : MonoBehaviour {

    [SerializeField] GameObject spawnLocation;
    [SerializeField] public Unit selectedUnit;

    GameMasterScript gamemaster;

    [Header("UI Management Stuff")]
    [SerializeField] Transform queuePanel;
    [SerializeField] Transform TopUnitListPanel;
    [SerializeField] List<Unit> spawnQueue;
    [SerializeField] GameObject imagePrefab;
    [SerializeField] GameObject buttonPrefab;


    // Use this for initialization
    void Start () {
        PopulateUnitButtonSelection();
        gamemaster = FindObjectOfType<GameMasterScript>();
    }

    // Update is called once per frame
    void Update () {
            DisplayCurrentSpawnQueue();
	}

    

    public void spawnFighter()
    {
        int i = 0;
        if (spawnQueue.Count == 0)
        {
            Debug.Log("Add something to queue first!! ");
            return;
        }
        foreach (Unit prefab in spawnQueue)
        {
            Debug.Log("Spawning..." + spawnLocation.transform.position);
            Instantiate(prefab.GetFighterPrefab(), spawnLocation.transform.position+new Vector3(i,0,0), spawnLocation.transform.rotation);
            i++;
        }
        spawnQueue.Clear();
    }

    public void addToSpawnQueue()
    {
        if(selectedUnit.GetFighterCost() <= gamemaster.money)
        {
            spawnQueue.Add(selectedUnit);
            gamemaster.money -= selectedUnit.GetFighterCost();
        }

    }

    public void removeFromSpawnQueue(GameObject button, Unit unitOnList)
    {
        Debug.Log("Removing");
        spawnQueue.Remove(unitOnList);
        gamemaster.money += unitOnList.GetFighterCost();
        Destroy(button);
    }

    private void DisplayCurrentSpawnQueue()
    {
        if (queuePanel.childCount == spawnQueue.Count)
        {
            return;
        }
        else
        {
                foreach (Transform t in queuePanel)
                {
                    Destroy(t.gameObject);
                }
                foreach (Unit item in spawnQueue)
                {
                    var bttButton = Instantiate(buttonPrefab, queuePanel);
                    var bttImage = bttButton.GetComponent<Image>();
                    bttImage.sprite = item.GetFighterImage();
                    bttImage.type = Image.Type.Simple;
                    bttImage.preserveAspect = true;

                    var events = bttButton.GetComponentInChildren<Button>();
                    events.onClick.AddListener(delegate { removeFromSpawnQueue(bttButton, item); });
                }
         }
    }

    private void PopulateUnitButtonSelection()
    {
        string path = "_Data/PlayerUnits";
        var items = Resources.LoadAll<Unit>(path);


        foreach (var item in items)
        {
            var bttButton = Instantiate(buttonPrefab, TopUnitListPanel);
            var bttImage = bttButton.GetComponent<Image>();
            bttImage.sprite = item.GetFighterImage();
            bttImage.type = Image.Type.Simple;
            bttImage.preserveAspect = true;
            Debug.Log("Done Configuring a button");

            var events = bttButton.GetComponentInChildren<Button>();
            events.onClick.AddListener(item.SelectPrefab);
            events.onClick.AddListener(addToSpawnQueue);
        }
    }
    }
