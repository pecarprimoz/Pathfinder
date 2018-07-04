using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float CurrentGameObjectMovementSpeed = 0.0f;
    public float CurrentGameObjectRotationSpeed = 0.0f;

    private float EulerFloatVariable = 0;
    private Rigidbody CurrentGameObjectRigidBody;

    void Start() {
        CurrentGameObjectRigidBody = GetComponent<Rigidbody>();
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
        EulerFloatVariable -= 1;
        gameObject.transform.rotation = Quaternion.Euler(0.0f, EulerFloatVariable, 0.0f);
    }
    public void RotateGameObjectRight() {
        EulerFloatVariable += 1;
        gameObject.transform.rotation = Quaternion.Euler(0.0f, EulerFloatVariable, 0.0f);
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
