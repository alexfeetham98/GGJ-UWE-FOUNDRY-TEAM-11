using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public List<GameObject> buildings;

    public int currentlySelected;
    private GameManager gameManager;


    void Start() {
        gameManager = GameManager.gameManager;
        currentlySelected = 0;

    }


    void Update() 
    {

        if (Input.GetKeyDown(KeyCode.Q)) {
            currentlySelected--;
            if (currentlySelected < 0) {
                currentlySelected = buildings.Count - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            currentlySelected++;
            if (currentlySelected >= buildings.Count) {
                currentlySelected = 0;
            }
        }


    }
}
