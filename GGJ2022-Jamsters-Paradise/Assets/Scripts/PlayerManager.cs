using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public enum Direction { Up, Down, Left, Right}


    [SerializeField]
    private GameObject player;  //Reference to player 
    [SerializeField]
    List<GameObject> buildings = new List<GameObject>();


    public int playerLocationX; //Player's X location on the board
    public int playerLocationZ; // Player's Y location on the board
    public Direction lastInput = Direction.Up; // Player's Last Movement direction
    public GameObject lookingAt; // Cell the player is looking at
    private GameObject currentlyHolding;

    private GridManager gm;
    private bool gmCheck;

    

    
    //Setting up variables (called from the GridManager)
    public void Setup() {
        gm = (GridManager)FindObjectOfType(typeof(GridManager));
        gmCheck = true;


        lookingAt = gm.GetCell(1, 1);
        currentlyHolding = buildings[0];
    }

    // Update is called once per frame
    void Update() {

        if (gmCheck) {
            playerLocationX = Mathf.FloorToInt(player.transform.position.x);
            playerLocationZ = Mathf.FloorToInt(player.transform.position.z);

            try {

                switch (lastInput) {
                    case Direction.Up:
                        lookingAt = gm.GetCell(playerLocationX, playerLocationZ + 1);
                        break;

                    case Direction.Down:
                        lookingAt = gm.GetCell(playerLocationX, playerLocationZ - 1);
                        break;

                    case Direction.Left:
                        lookingAt = gm.GetCell(playerLocationX - 1, playerLocationZ);
                        break;

                    case Direction.Right:
                        lookingAt = gm.GetCell(playerLocationX + 1, playerLocationZ);
                        break;

                    default:
                        lookingAt = gm.GetCell(playerLocationX, playerLocationZ);
                        break;

                }

            } catch (System.IndexOutOfRangeException ex) {
                lookingAt = gm.GetCell(playerLocationX, playerLocationZ);

            }


            

        }
    }

    public void PlaceCurrentObject() {

        Vector3 posToPlace = lookingAt.transform.position;
        posToPlace.y += 0.5f;  //Hardcoded Value

        GameObject go = Instantiate(currentlyHolding, posToPlace, Quaternion.identity);
        lookingAt.GetComponent<Cell>().SetImprovement(go);
    }

        
}
