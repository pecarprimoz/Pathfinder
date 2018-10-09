using System.Collections.Generic;
using UnityEngine;

public partial class ShittyAI : MonoBehaviour {
    private enum State { kSeaching, kMoving, kRotating }
    public TilemapController CurrentTilemapController;
    public SensorLogic[] AllSensorsLogic;
    public RoombaController CurrentRoombaController;
    private State CurrentState;
    // CommandQueue is used to tell the AI how to move
    private Queue<MovementCommand> CommandQueue;
    // Current command that is being executed, need for update
    private MovementCommand ExectuingCommand = null;
    private MovementController CurrentMovementController;

    private Queue<TileSet> ClosedTileSetList;
    private Queue<TileSet> OpenTileSetList;

    void Start() {
        CurrentMovementController = GetComponent<MovementController>();
        CommandQueue = new Queue<MovementCommand>();

        ClosedTileSetList = new Queue<TileSet>();
        OpenTileSetList = new Queue<TileSet>();

        // generate best possible path by greedy method
        GenerateBestPath();

        CurrentState = State.kSeaching;
    }

    void Searching() {
        // DEQUEUE HERE AND ADD AS A COMMAND STEP BY STEP
        // This is gonna be replaced with the A* algorithm once I implement it
        if (ClosedTileSetList.Count > 0) {
            TileSet currentMove = ClosedTileSetList.Dequeue();
            var currentRoombaTileSet = CurrentRoombaController.GetCurrentRoombaTileSet();
            CommandQueue.Enqueue(new MovementMove(currentRoombaTileSet.column, currentRoombaTileSet.row, currentMove.column, currentMove.row, CurrentTilemapController));
            CurrentState = State.kMoving;
        } else {
            CurrentTilemapController.ClearTileMap();
            CurrentTilemapController.SetFinishTile();
            GenerateBestPath();
        }
    }
    void Moving() {
        if (ExectuingCommand != null) {
            if (ExectuingCommand.isBeingExecuted) {
                ExecuteCommand();
            } else {
                ExectuingCommand = null;
            }
        } else {
            if (CommandQueue.Count > 0) {
                ExectuingCommand = CommandQueue.Dequeue();
            } else {
                Searching();
            }
        }
    }

    void Update() {
        switch (CurrentState) {
            case State.kSeaching:
                Searching();
                break;
            case State.kMoving:
                Moving();
                break;
            case State.kRotating:
                break;
            default:
                Debug.LogWarning("Unhadeled case.");
                break;
        }
    }

    private void ExecuteCommand() {
        ExectuingCommand.Execute(transform, CurrentMovementController, AllSensorsLogic);
    }

    public void GenerateBestPath() {
        // move start tile and end tile to shitty Ai
        // enqueue starting position
        OpenTileSetList.Enqueue(CurrentTilemapController.StartTile);
        while (OpenTileSetList.Count > 0) {
            TileSet CurrentTileSet = OpenTileSetList.Dequeue();
            ClosedTileSetList.Enqueue(CurrentTileSet);

            // if current tile is equal to the finish tile we are at the end
            if (CurrentTileSet == CurrentTilemapController.FinishTile) {
                break;
            }
            float bestLowCost = float.MaxValue;
            TileSet potentialBestTileSet = null;
            foreach (var lowestCostNeigbour in CurrentTilemapController.GetNeighboursForTileSet(CurrentTileSet)) {
                var currentDistance = CurrentTilemapController.CalculateDistance(lowestCostNeigbour, CurrentTilemapController.FinishTile);
                if (bestLowCost > currentDistance && !lowestCostNeigbour.Visited) {
                    bestLowCost = currentDistance;
                    potentialBestTileSet = lowestCostNeigbour;
                    CurrentTilemapController.SetVisited(lowestCostNeigbour.column, lowestCostNeigbour.row);
                }
            }
            if (potentialBestTileSet != null) {
                OpenTileSetList.Enqueue(potentialBestTileSet);
            }
        }
    }

}
