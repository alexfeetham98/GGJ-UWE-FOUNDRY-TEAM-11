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

    public void Setup(List<Vector2> path) {
        gm = (GridManager)FindObjectOfType(typeof(GridManager));
        gmCheck = true;

        this.path = path;

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

        currentPos.y = 0.5f;
        previousPos.y = 0.5f;
        targetPos.y = 0.5f;

        gameObject.transform.position += (targetPos - previousPos) * speedModifier * Time.deltaTime;

        RotateObjectToTarget.RotateObject(gameObject, nextCell.transform.position);


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
