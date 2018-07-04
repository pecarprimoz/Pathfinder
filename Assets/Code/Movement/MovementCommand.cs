﻿using UnityEngine;

public enum MovementType { kMove, kRotate };
public abstract class MovementCommand {
    protected TilemapController TilemapController;

    protected bool IsBeingExecuted = false;

    protected Vector3 StartPosition = Vector3.zero;

    protected Vector3 EndPosition = Vector3.zero;

    // StatRow and StartColumn represent our position on the tilemap
    protected readonly int StartRow = -1;
    protected readonly int StartColumn = -1;

    protected MovementCommand(Vector3 startPosition, TilemapController tilemapController) {
        StartPosition = startPosition;
        TilemapController = tilemapController;
        TileSet currentPositionTileSet = tilemapController.GetTileSetForPosition(startPosition);
        if (currentPositionTileSet == null) {
            // DEBUG ERROR
            return;
        }
        StartColumn = currentPositionTileSet.column;
        StartRow = currentPositionTileSet.row;
    }

    public Vector3 startPosition {
        get { return StartPosition; }
        set { StartPosition = value; }
    }

    public bool isBeingExecuted { get { return IsBeingExecuted; } }

    protected bool WillHitWall(SensorLogic[] sensorLogic) {
        for (int i = 0; i < sensorLogic.Length; ++i) {
            if (sensorLogic[i].IRRaycastHit.distance < 1f && sensorLogic[i].IRRaycastHit.distance != 0) {
                Debug.LogWarningFormat("Sensor {0} returned true !", i);
                return true;
            }
        }
        return false;
    }
    public abstract void Execute(Transform transform, MovementController movementController, SensorLogic[] sensorLogic);
}