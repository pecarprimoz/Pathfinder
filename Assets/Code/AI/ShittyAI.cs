using System.Collections;
using UnityEngine;

public class ShittyAI : MonoBehaviour {
    public enum MovementAction { kMoveForward, kMoveBackwards, kRotateLeft, kRotateRight };

    public class MovementCommand {
        // Tells us what type of movement we are executing
        private MovementAction CurrentMovementAction;
        // A flag for the AI, need to know if we are done moving
        private bool IsBeingExecuted;
        // The start position when the command got called
        private Vector3 StartPosition;
        // The end position we want to reach
        private Vector3 EndPosition;

        // StatRow and StartColumn represent our position on the tilemap
        private int StartRow = -1;
        private int StartColumn = -1;
        // NumberOfTiles define how much we are gonna move
        private int NumberOfTiles;

        // TODO, implement tileset movement based on start and end pos
        // will need an algorithm that can resolve the best path based on the vectors
        public MovementCommand(MovementAction movementAction, Vector3 startPosition, Vector3 endPosition) {
            IsBeingExecuted = true;
            CurrentMovementAction = movementAction;
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public MovementCommand(MovementAction movementAction, Vector3 startPosition, int numberOfTiles) {
            IsBeingExecuted = true;
            CurrentMovementAction = movementAction;
            StartPosition = startPosition;
            NumberOfTiles = numberOfTiles;
        }

        public MovementAction currentMovementAction {
            get { return CurrentMovementAction; }
            set { CurrentMovementAction = value; }
        }

        public bool isBeingExecuted { get { return IsBeingExecuted; } }

        // We check if its possible to hit the wall based on the sensor
        private bool WillHitWall(SensorLogic[] sensorLogic) {
            for(int i=0; i<sensorLogic.Length; ++i){
                if (sensorLogic[i].IRRaycastHit.distance < 1f && sensorLogic[i].IRRaycastHit.distance != 0) {
                    Debug.LogWarningFormat("Sensor {0} returned true !",i);
                    return true;
                }
            }
            return false;
        }


        public void TileMoveForward(TilemapController tilemapController, Transform transform, MovementController movementController, SensorLogic[] sensorLogic) {
            // StartRow and StartColumn get initialised when it first enters the function, they represent our position in the tilemap
            TilemapController.TileSet currentPositionTileSet = tilemapController.GetTileSetForPosition(transform.position);
            if (currentPositionTileSet == null) { IsBeingExecuted = false; return; }
            if (StartRow == -1 && StartColumn == -1) {
                StartRow = currentPositionTileSet.row;
                StartColumn = currentPositionTileSet.column;
            }
            // First we check if its possible to hit the wall based on our position
            if (!WillHitWall(sensorLogic)) {
                // Then we check if our current position is not the same as our end position, this is used for tilebased movement, THIS MIGHT NOT BE RIGHT, PROLLY NEED TO SPLIT THE ROW AND COLUMN CHECKS !
                if (currentPositionTileSet.row != StartRow + NumberOfTiles &&
                    currentPositionTileSet.column != StartColumn + NumberOfTiles) {
                    // If its okay, we can move the game object forward
                    movementController.MoveGameObjectForward();
                } else {
                    // We are on our end position, we can stop exectuing the current comman, flag to tell the AI that we are done with this command
                    IsBeingExecuted = false;
                }
            } else {
                // If we are gonna hit a wall we need to tell the ai that we wont execute this command
                IsBeingExecuted = false;
            }
        }

        public void TileMoveBackwards(TilemapController tilemapController, Transform transform, MovementController movementController, SensorLogic[] sensorLogic) {

            TilemapController.TileSet currentPositionTileSet = tilemapController.GetTileSetForPosition(transform.position);
            if (currentPositionTileSet == null) { IsBeingExecuted = false; return; }
            if (StartRow == -1 && StartColumn == -1) {
                StartRow = currentPositionTileSet.row;
                StartColumn = currentPositionTileSet.column;
            }
            if (!WillHitWall(sensorLogic)) {
                if (currentPositionTileSet.row != StartRow - NumberOfTiles &&
                    currentPositionTileSet.column != StartColumn - NumberOfTiles) {
                    movementController.MoveGameObjectBackwards();
                } else {
                    IsBeingExecuted = false;
                }
            } else {
                IsBeingExecuted = false;
            }
        }

        public void TileRotateRight(TilemapController tilemapController, Transform transform, MovementController movementController, SensorLogic[] sensorLogic) {

        }

    }

    public TilemapController CurrentTilemapController;
    public SensorLogic[] AllSensorsLogic;

    // CommandQueue is used to tell the AI how to move
    private Queue CommandQueue;
    // Current command that is being executed, need for update
    private MovementCommand ExectuingCommand = null;
    private MovementController CurrentMovementController;

    void Start() {
        CurrentMovementController = GetComponent<MovementController>();
        CommandQueue = new Queue();
        // TESTING //
        /*
        */
        //CommandQueue.Enqueue(new MovementCommand(MovementAction.kMoveForward, transform.position, 1));
        //CommandQueue.Enqueue(new MovementCommand(MovementAction.kMoveForward, transform.position, 4));
        //CommandQueue.Enqueue(new MovementCommand(MovementAction.kMoveBackwards, transform.position, 4));
        CommandQueue.Enqueue(new MovementCommand(MovementAction.kRotateRight, transform.position, 1));
    }

    void Update() {
        if (ExectuingCommand != null) {
            if (ExectuingCommand.isBeingExecuted) {
                ExecuteCommand();
            } else {
                ExectuingCommand = null;
            }
        } else {
            if (CommandQueue.Count > 0) {
                ExectuingCommand = (MovementCommand)CommandQueue.Dequeue();
            }
        }
    }

    private void ExecuteCommand() {
        switch (ExectuingCommand.currentMovementAction) {
            case (MovementAction.kMoveForward):
                ExectuingCommand.TileMoveForward(CurrentTilemapController, transform, CurrentMovementController, AllSensorsLogic);
                break;
            case (MovementAction.kMoveBackwards):
                ExectuingCommand.TileMoveBackwards(CurrentTilemapController, transform, CurrentMovementController, AllSensorsLogic);
                break;
            case (MovementAction.kRotateRight):
                ExectuingCommand.TileMoveBackwards(CurrentTilemapController, transform, CurrentMovementController, AllSensorsLogic);
                break;
        }
    }

}
