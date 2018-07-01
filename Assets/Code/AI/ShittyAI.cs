using UnityEngine;

public class ShittyAI : MonoBehaviour {

    public GameObject FrontSensorGameObject;
    public GameObject FinishCubeGameObject;

    private SensorLogic FrontSensorLogic;
    private MovementController AIMovementController;

    private bool RotatingLeft = false;
    private bool RotatingRight = false;
    private float CurrentDistanceFromFinish = float.MaxValue;
    void Start() {
        FrontSensorLogic = FrontSensorGameObject.GetComponent<SensorLogic>();
        AIMovementController = GetComponent<MovementController>();
    }

    void CheckDistanceFromGoal() {
        CurrentDistanceFromFinish = Vector3.Distance(transform.position,FinishCubeGameObject.transform.position);
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
