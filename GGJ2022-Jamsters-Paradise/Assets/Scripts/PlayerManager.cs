using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public enum Direction { Up, Down, Left, Right}


    [SerializeField]
    private GameObject player;  //Reference to player

    


    public int playerLocationX; //Player's X location on the board
    public int playerLocationZ; // Player's Y location on the board

    public Direction lastInput = Direction.Up; // Player's Last Movement direction
    public GameObject lookingAt; // Cell the player is looking at
    public GameObject lastLookedAt;
    
    

    private GridManager gm;
    private bool gmCheck;

    

    
    //Setting up variables (called from the GridManager)
    public void Setup() {
        gm = (GridManager)FindObjectOfType(typeof(GridManager));
        gmCheck = true;


        lookingAt = gm.GetCell(0, 0);
    }

    // Update is called once per frame
    void Update() {
        UpdateCellLookingAt();
        
    }

    public void UpdateCellLookingAt() {
        
        
        
        if (gmCheck) {

            playerLocationX = Mathf.RoundToInt(player.transform.position.x);
            playerLocationZ = Mathf.RoundToInt(player.transform.position.z);

            try {

                switch (lastInput) {
                    case Direction.Up:

                        if (gm.GetCell(playerLocationX, playerLocationZ + 1) != lookingAt) {
                            UpdateVariablesForLooking(playerLocationX, playerLocationZ + 1);
                        }

                        break;

                    case Direction.Down:

                        if (gm.GetCell(playerLocationX, playerLocationZ - 1) != lookingAt) {
                            UpdateVariablesForLooking(playerLocationX, playerLocationZ - 1);
                        }

                        break;

                    case Direction.Left:

                        if (gm.GetCell(playerLocationX - 1, playerLocationZ) != lookingAt) {
                            UpdateVariablesForLooking(playerLocationX - 1, playerLocationZ);
                        }

                        break;

                    case Direction.Right:

                        if (gm.GetCell(playerLocationX + 1, playerLocationZ) != lookingAt) {
                            UpdateVariablesForLooking(playerLocationX + 1, playerLocationZ);
                        }

                        break;

                }

            } catch (System.IndexOutOfRangeException ex) {
                lookingAt = gm.GetCell(playerLocationX, playerLocationZ);

            }



        }
    }

    public void UpdateVariablesForLooking(int x, int y) {
        lastLookedAt = lookingAt;
        lookingAt = gm.GetCell(x, y);

        gm.UpdateCellColours(lastLookedAt, lookingAt);


    }

    public void PlaceCurrentObject() {

        Vector3 posToPlace = lookingAt.transform.position;
        posToPlace.y += 0.5f;  //Hardcoded Value

        if (!lookingAt.GetComponent<Cell>().CheckForImprovement()) {
            GameObject go = Instantiate(BuildingManager.currentlySelected, posToPlace, Quaternion.identity);
            lookingAt.GetComponent<Cell>().SetImprovement(go);
        }

        
    }

        
}
