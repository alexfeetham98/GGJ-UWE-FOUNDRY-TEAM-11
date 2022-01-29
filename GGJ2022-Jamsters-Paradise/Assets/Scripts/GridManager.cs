using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private GameObject cell;

    private GameObject[,] grid;

    private int width, height;
    private int cellWidth = 1;
    
    // Start is called before the first frame update
    void Start() {
        width = 10;
        height = 10;

        InitialiseBoard(width, height);
    }

    private void InitialiseBoard(int width, int height) {

        for (int x = 0; x < width; x += cellWidth) {
            for (int y = 0; y < height; y += cellWidth) {

                GameObject go = Instantiate(cell, new Vector3(x, 0, y), Quaternion.identity);
                go.name = "cell_" + x + "_" + y;
            }
        }
    }
    // Update is called once per frame
    void Update() {
        
    }
}
