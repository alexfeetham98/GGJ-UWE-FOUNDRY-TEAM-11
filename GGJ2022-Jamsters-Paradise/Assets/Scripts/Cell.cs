using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    [SerializeField]
    private GridManager gridManager;

    private GameObject improvement;

    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetImprovement(GameObject improvement) {
        this.improvement = improvement;
    }
}
