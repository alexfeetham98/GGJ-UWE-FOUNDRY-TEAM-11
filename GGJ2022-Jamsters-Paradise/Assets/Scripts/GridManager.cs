using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private GameObject cell;

    [SerializeField]
    private Transform cellHolder;

    public GameObject[,] grid;

    private int width, height;
    private int cellWidth = 1;

    
    public Material matRed;
    public Material matWhite;

    // Start is called before the first frame update
    void Start() {
        width = 25;
        height = 25;
        grid = new GameObject[width, height];

        InitialiseBoard(width, height);
        GetComponent<PlayerManager>().Setup();
    }

    private void InitialiseBoard(int width, int height) {

        for (int x = 0; x < width; x += cellWidth) {
            for (int y = 0; y < height; y += cellWidth) {

                GameObject go = Instantiate(cell, new Vector3(x, 0, y), Quaternion.identity);
                grid[x, y] = go;
                go.name = "cell_" + x + "_" + y;
                go.transform.parent = cellHolder;
            }
        }
    }

    public void UpdateCellColours(GameObject previous, GameObject current) {

        previous.GetComponent<MeshRenderer>().material = matWhite;
        current.GetComponent<MeshRenderer>().material = matRed;

    }

    public GameObject GetCell(int x, int y) {
        return grid[x, y];
    }
}
