using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public List<GameObject> buildings;

    public static GameObject currentlySelected;
    private GameManager gameManager;


    void Start() {
        gameManager = GameManager.gameManager;
        currentlySelected = buildings[0];

    }


    void Update() 
    {

    }
}
