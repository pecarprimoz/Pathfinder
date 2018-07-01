using UnityEngine;

public class ShittyAI : MonoBehaviour {

    public GameObject FrontSensorGameObject;
    public GameObject FinishLineGameObject;
    public TilemapController CurrentTilemapController;

    private SensorLogic FrontSensorLogic;
    private MovementController AIMovementController;

    private bool RotatingLeft = false;
    private bool RotatingRight = false;
    private float CurrentDistanceFromFinish = float.MaxValue;
    private Vector3 RoombaStartPosition;
    private Vector3 PreviousPosition;

    void Start() {
        RoombaStartPosition = transform.position;
        PreviousPosition = RoombaStartPosition;
        FrontSensorLogic = FrontSensorGameObject.GetComponent<SensorLogic>();
        AIMovementController = GetComponent<MovementController>();
        AIMovementController.MoveGameObjectForward();
    }

    private bool WillHitWall() {
        return FrontSensorLogic.IRRaycastHit.distance < 1f && FrontSensorLogic.IRRaycastHit.distance != 0;
    }

    void CheckDistanceFromGoal() {
        CurrentDistanceFromFinish = Vector3.Distance(transform.position, FinishLineGameObject.transform.position);
    }

    void Update() {
        MoveForwardATile();
    }

    void MoveForwardATile() {
        Vector3 currentPosition = transform.position;
        if (!WillHitWall()) {
            Debug.LogFormat("{0} {1}", Mathf.Floor(transform.position.x), Mathf.Floor(PreviousPosition.x));
            if (Mathf.Floor(transform.position.x) == Mathf.Floor(PreviousPosition.x))
                AIMovementController.MoveGameObjectForward();
            else {
                PreviousPosition = transform.position;
            }
        } 
    }
}
