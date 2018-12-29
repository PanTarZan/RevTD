using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour {
    [SerializeField] GameObject selector;

    public void TurnSelectionMarkerOn()
    {
        if (gameObject == FindObjectOfType<GameMasterScript>().selectedTarget)
        selector.SetActive(true);
        else
        {
            selector.SetActive(false);
        }
    }
    public void SelectionMarkerOnHover()
    {

    }

    void Update()
    {
        TurnSelectionMarkerOn();
    }
}
