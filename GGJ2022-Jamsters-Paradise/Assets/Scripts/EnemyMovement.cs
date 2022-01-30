using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject currentCell;
    public GameObject nextCell;
    private int positionInPath;

    private GridManager gm;
    private bool gmCheck = false;
    
    public List<Vector2> path;
    public float speedModifier = 3f;

    public void Setup() {
        gm = (GridManager)FindObjectOfType(typeof(GridManager));
        gmCheck = true;
        positionInPath = 0;

        currentCell = gm.GetCell((int)path[0].x, (int)path[0].y);
        nextCell = gm.GetCell((int)path[1].x, (int)path[1].y);



    }


    void Update() {
        if (gmCheck) {

            UpdateMovement();

        }
    }

    private void UpdateMovement() {


        Vector3 currentPos = gameObject.transform.position;
        Vector3 previousPos = currentCell.transform.position;
        Vector3 targetPos = nextCell.transform.position;

        currentPos.y = 1f;
        previousPos.y = 1f;
        targetPos.y = 1f;

        gameObject.transform.position += (targetPos - previousPos) * speedModifier * Time.deltaTime;



        if (Vector3.Distance(currentPos, targetPos) <= 0.05f * speedModifier) {
            gameObject.transform.position = targetPos;
            positionInPath++;
            if (positionInPath >= path.Count) {
                gameObject.SetActive(false);
                return;
            }
            currentCell = nextCell;
            nextCell = gm.GetCell((int)path[positionInPath].x, (int)path[positionInPath].y);

        }


    }

}
