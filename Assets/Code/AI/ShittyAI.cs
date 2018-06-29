using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShittyAI : MonoBehaviour {
    public GameObject FrontSensorGameObject;

    private SensorLogic FrontSensorLogic;
    private MovementController AIMovementController;

    private bool RotatingLeft = false;
    private bool RotatingRight = false;
    void Start() {
        FrontSensorLogic = FrontSensorGameObject.GetComponent<SensorLogic>();
        AIMovementController = GetComponent<MovementController>();
    }

    void Update() {
        AIMovementController.MoveGameObjectForward();
        if (FrontSensorLogic.IRRaycastHit.distance < 1f && FrontSensorLogic.IRRaycastHit.distance != 0) {
            RotatingRight = true;
        }
        if (RotatingRight) {
            AIMovementController.RotateGameObjectRight();
            RotatingRight = false;
        }

    }
}
