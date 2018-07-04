using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float CurrentGameObjectMovementSpeed = 0.0f;
    public float CurrentGameObjectRotationSpeed = 0.0f;

    private float ObjectYRotation = 0;

    public float objectYRotation {
        get { return ObjectYRotation; }
    }

    void Start() {
        ObjectYRotation = transform.rotation.eulerAngles.y;
    }

    void FixedUpdate() {
        HandlePlayerControlls();
    }

    // Movement without tilesets
    public void MoveGameObjectForward() {
        transform.position += transform.forward * CurrentGameObjectMovementSpeed * Time.deltaTime; 
    }
    public void MoveGameObjectBackwards() {
        transform.position += -transform.forward * CurrentGameObjectMovementSpeed * Time.deltaTime;
    }
    public void RotateGameObjectLeft() {
        ObjectYRotation -= 1;
        gameObject.transform.rotation = Quaternion.Euler(0.0f, ObjectYRotation, 0.0f);
    }
    public void RotateGameObjectRight() {
        ObjectYRotation += 1;
        gameObject.transform.rotation = Quaternion.Euler(0.0f, ObjectYRotation, 0.0f);
    }

    void HandlePlayerControlls() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            MoveGameObjectForward();
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            MoveGameObjectBackwards();
        } if (Input.GetKey(KeyCode.LeftArrow)) {
            RotateGameObjectLeft();
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            RotateGameObjectRight();
        }
    }
}
