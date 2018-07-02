using UnityEngine;

public class ShittyAI : MonoBehaviour {

    public GameObject FrontSensorGameObject;
    public GameObject FinishLineGameObject;
    public TilemapController CurrentTilemapController;

    public enum MovementAction { kMoveForward, kMoveBackwards, kRotateLeft, kRotateRight };

    private SensorLogic FrontSensorLogic;
    private MovementController AIMovementController;

    private bool RotatingLeft = false;
    private bool RotatingRight = false;

    private float CurrentDistanceFromFinish = float.MaxValue;

    private Vector3 RoombaStartPosition;
    private Vector3 PreviousPosition;
    private Vector3 RoombaMeshRendererBoundsSize;

    private int StartRow;
    private bool IsMoving;
    private int StartColumn;
    
    void Start() {
        StartRow = -1;
        StartColumn = -1;
        IsMoving = false;
        RoombaMeshRendererBoundsSize = GetComponent<MeshRenderer>().bounds.size;
        RoombaStartPosition = transform.position;
        PreviousPosition = RoombaStartPosition;
        FrontSensorLogic = FrontSensorGameObject.GetComponent<SensorLogic>();
        AIMovementController = GetComponent<MovementController>();
    }

    private bool WillHitWall() {
        return FrontSensorLogic.IRRaycastHit.distance < 1f && FrontSensorLogic.IRRaycastHit.distance != 0;
    }

    void CheckDistanceFromGoal() {
        CurrentDistanceFromFinish = Vector3.Distance(transform.position, FinishLineGameObject.transform.position);
    }

    void Update() {
        Debug.Log(CurrentTilemapController.GetTileSetForPosition(transform.position));
    }
    public void MoveRoomba(MovementAction movement, int tiles) {
        IsMoving = true;
        switch (movement) {
            case MovementAction.kMoveForward:
                TileMoveForward(tiles);
                break;
            case MovementAction.kMoveBackwards:
                break;
            default:
                break;
        }
    }

    void TileMoveForward(int numOftiles) {
        TilemapController.TileSet currentPositionTileSet = CurrentTilemapController.GetTileSetForPosition(transform.position);
        if (StartRow == -1 && StartColumn == -1) {
            StartRow = currentPositionTileSet.row;
            StartColumn = currentPositionTileSet.column;
        }
        if (!WillHitWall()) {
            if (IsMoving) {
                if (currentPositionTileSet.row != StartRow + numOftiles &&
                    currentPositionTileSet.column != StartColumn + numOftiles) {
                    AIMovementController.MoveGameObjectForward();
                } else {
                    StartRow = -2;
                    StartColumn = -2;
                    IsMoving = false;
                }
            }
        }
    }
}
