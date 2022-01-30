using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private GameObject cell;

    [SerializeField]
    private Transform cellHolder;

    [SerializeField]
    private GameObject battery;

    [SerializeField]
    private GameObject enemySpawner;

    [SerializeField]
    private GameObject solarPanel;

    public GameObject[,] grid;

    private int width, height;
    private int cellWidth = 1;

    
    public Material matRed;
    public Material matWhite;
    public Material matPath;

    public List<Vector2> path1;
    public List<Vector2> path2;

    // Start is called before the first frame update
    void Start() {
        width = 20;
        height = 20;
        grid = new GameObject[width, height];

        GeneratePaths();




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

                Vector2 pos = new Vector2(go.transform.position.x, go.transform.position.z);
                if (path1.Contains(pos) || path2.Contains(pos)) {
                    go.GetComponent<Cell>().isPath = true;
                    go.GetComponent<MeshRenderer>().material = matPath;

                } else {
                    go.GetComponent<Cell>().isPath = false;
                    go.GetComponent<MeshRenderer>().material = matWhite;
                }


            }
        }

        // Spawning the batteries at the end of the path
        GameObject go_battery = Instantiate(battery, new Vector3(path1[path1.Count - 1].x, 0.5f, path1[path1.Count - 1].y), Quaternion.identity);
        go_battery = Instantiate(battery, new Vector3(path2[path2.Count - 1].x, 0.5f, path2[path2.Count - 1].y), Quaternion.identity);

        // Setting the enemy spawners
        GameObject go_EnemySpawner = Instantiate(enemySpawner, new Vector3(path1[0].x, 1f, path1[0].y), Quaternion.identity);
        go_EnemySpawner.GetComponent<EnemySpawner>().Setup(path1);

        go_EnemySpawner = Instantiate(enemySpawner, new Vector3(path2[0].x, 1f, path2[0].y), Quaternion.identity);
        go_EnemySpawner.GetComponent<EnemySpawner>().Setup(path2);


        // Setting up initial solar panel - HARDCODED VALUE

        GameObject go_SolarPanel = Instantiate(solarPanel, new Vector3(15, 0.5f, 5), Quaternion.identity);
        GetCell(15, 5).GetComponent<Cell>().SetImprovement(solarPanel);

        
    }

    public void UpdateCellColours(GameObject previous, GameObject current) {

        if (previous.GetComponent<Cell>().isPath) {
            previous.GetComponent<MeshRenderer>().material = matPath;
        } else {
            previous.GetComponent<MeshRenderer>().material = matWhite;
        }

        current.GetComponent<MeshRenderer>().material = matRed;

    }

    public GameObject GetCell(int x, int y) {
        return grid[x, y];
    }

    private void GeneratePaths() {

        // -- Path 1 --

        path1.Add(new Vector2(0,15));
        path1.Add(new Vector2(1,15));
        path1.Add(new Vector2(2,15));
        path1.Add(new Vector2(3,15));
        path1.Add(new Vector2(4,15));
        path1.Add(new Vector2(5,15));
        path1.Add(new Vector2(6,15));
        path1.Add(new Vector2(7,15));
        path1.Add(new Vector2(8,15));
        path1.Add(new Vector2(9,15));
        path1.Add(new Vector2(10,15));
        path1.Add(new Vector2(11,15));
        path1.Add(new Vector2(12,15));
        path1.Add(new Vector2(13,15));
        path1.Add(new Vector2(14,15));
        path1.Add(new Vector2(15,15));
        path1.Add(new Vector2(16,15));
        path1.Add(new Vector2(17,15));

        path1.Add(new Vector2(17,14));
        path1.Add(new Vector2(17,13));
        path1.Add(new Vector2(17,12));
        path1.Add(new Vector2(17,11));
        path1.Add(new Vector2(17,10));
        path1.Add(new Vector2(17,9));

        path1.Add(new Vector2(16,9));
        path1.Add(new Vector2(15,9));
        path1.Add(new Vector2(14,9));
        path1.Add(new Vector2(13,9));
        path1.Add(new Vector2(12,9));
        path1.Add(new Vector2(11,9));
        path1.Add(new Vector2(10,9));
        path1.Add(new Vector2(9,9));
        path1.Add(new Vector2(8,9));
        path1.Add(new Vector2(7,9));
        path1.Add(new Vector2(6,9));
        path1.Add(new Vector2(5,9));

        path1.Add(new Vector2(5,8));
        path1.Add(new Vector2(5,7));
        path1.Add(new Vector2(5,6));
        path1.Add(new Vector2(5,5));

        path1.Add(new Vector2(6,5));
        path1.Add(new Vector2(7,5));
        path1.Add(new Vector2(8,5));
        path1.Add(new Vector2(9,5));
        path1.Add(new Vector2(10,5));

        path1.Add(new Vector2(10,4));
        path1.Add(new Vector2(10,3));
        path1.Add(new Vector2(10,2));
        path1.Add(new Vector2(10,1));
        path1.Add(new Vector2(10,0));


        // -- Path 2 --
        path2.Add(new Vector2(0,14));
        path2.Add(new Vector2(1,14));
        path2.Add(new Vector2(2,14));
        path2.Add(new Vector2(3,14));
        path2.Add(new Vector2(4,14));
        path2.Add(new Vector2(5,14));
        path2.Add(new Vector2(6,14));
        path2.Add(new Vector2(7,14));
        path2.Add(new Vector2(8,14));
        path2.Add(new Vector2(9,14));
        path2.Add(new Vector2(10,14));
        path2.Add(new Vector2(11,14));
        path2.Add(new Vector2(12,14));
        path2.Add(new Vector2(13,14));
        path2.Add(new Vector2(14,14));
        path2.Add(new Vector2(15,14));
        path2.Add(new Vector2(16,14));

        path2.Add(new Vector2(16,13));
        path2.Add(new Vector2(16,12));
        path2.Add(new Vector2(16,11));
        path2.Add(new Vector2(16,10));

        path2.Add(new Vector2(15,10));
        path2.Add(new Vector2(14,10));
        path2.Add(new Vector2(13,10));
        path2.Add(new Vector2(12,10));
        path2.Add(new Vector2(11,10));
        path2.Add(new Vector2(10,10));
        path2.Add(new Vector2(9,10));
        path2.Add(new Vector2(8,10));
        path2.Add(new Vector2(7,10));
        path2.Add(new Vector2(6,10));
        path2.Add(new Vector2(5,10));
        path2.Add(new Vector2(4,10));

        path2.Add(new Vector2(4,9));
        path2.Add(new Vector2(4,8));
        path2.Add(new Vector2(4,7));
        path2.Add(new Vector2(4,6));
        path2.Add(new Vector2(4,5));
        path2.Add(new Vector2(4,4));

        path2.Add(new Vector2(5,4));
        path2.Add(new Vector2(6,4));
        path2.Add(new Vector2(7,4));
        path2.Add(new Vector2(8,4));
        path2.Add(new Vector2(9,4));

        path2.Add(new Vector2(9,3));
        path2.Add(new Vector2(9,2));
        path2.Add(new Vector2(9,1));
        path2.Add(new Vector2(9,0));

    }
}
