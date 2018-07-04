using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MovementMove : MovementCommand {
    public enum EnumDirection { kForward, kBack };

    private int NumberOfTiles;
    private EnumDirection Direction;

    public MovementMove(Vector3 startPosition, TilemapController tilemapController, int numberOfTiles, EnumDirection direction) : base(startPosition, tilemapController) {
        IsBeingExecuted = true;
        NumberOfTiles = numberOfTiles;
        Direction = direction;
    }

    public void TileMoveForward(Transform currentTransform, MovementController movementController, SensorLogic[] sensorLogic) {
        // StartRow and StartColumn get initialised when it first enters the function, they represent our position in the tilemap
        TilemapController.TileSet currentPositionTileSet = TilemapController.GetTileSetForPosition(currentTransform.position);

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

    public void TileMoveBackwards(Transform currentTransform, MovementController movementController, SensorLogic[] sensorLogic) {

        TilemapController.TileSet currentPositionTileSet = TilemapController.GetTileSetForPosition(currentTransform.position);
        if (currentPositionTileSet == null) { IsBeingExecuted = false; return; }

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

    public override void Execute(Transform transform, MovementController movementController, SensorLogic[] sensorLogic) {
        switch (Direction) {
            case (EnumDirection.kForward):
                TileMoveForward(transform,movementController,sensorLogic);
                break;
            case (EnumDirection.kBack):
                TileMoveBackwards(transform, movementController, sensorLogic);
                break;
            default:
                break;
        }
    }
}