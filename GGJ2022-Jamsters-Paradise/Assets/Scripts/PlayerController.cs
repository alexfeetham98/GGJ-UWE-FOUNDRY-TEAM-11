using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float movementMultiplier;
    [SerializeField]
    private PlayerManager pm;

    // Start is called before the first frame update
    void Start() {
        movementMultiplier = 5;
        pm = (PlayerManager)FindObjectOfType(typeof(PlayerManager));
    }



    // Update is called once per frame
    void Update() {

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            HandleMovement();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            PlaceObject();
        }

    }

    public void HandleMovement() {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0) {
            if (horizontal < 0) {
                pm.lastInput = PlayerManager.Direction.Left;
            } else {
                pm.lastInput = PlayerManager.Direction.Right;
            }
        }

        if (vertical != 0) {
            if (vertical < 0) {
                pm.lastInput = PlayerManager.Direction.Down;
            } else {
                pm.lastInput = PlayerManager.Direction.Up;
            }
        }



        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
        movement *= movementMultiplier * Time.deltaTime;

        gameObject.transform.position += movement;


    }

    public void PlaceObject() {
        pm.PlaceCurrentObject();
    }
}
