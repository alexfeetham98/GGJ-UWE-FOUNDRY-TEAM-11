using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public List<GameObject> buildings;

    public static GameObject currentlySelected;


    void Start() {

        currentlySelected = buildings[0];

    }


    void Update() {
        
    }
}
