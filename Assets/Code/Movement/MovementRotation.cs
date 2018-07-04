using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MovementRotation : MovementCommand {
    public enum EnumRotation { kLeft, kRight };
    private float RotationEuler;
    private EnumRotation Direction;

    public MovementRotation(Vector3 startPosition, TilemapController tilemapController, float rotationEuler, EnumRotation direction) : base(startPosition, tilemapController) {
        IsBeingExecuted = true;
        RotationEuler = rotationEuler;
        Direction = direction;
    }

    public void RotateLeft(Transform currentTransform, MovementController movementController, SensorLogic[] sensorLogic) {
        if (currentTransform.rotation.eulerAngles.y != RotationEuler) {
            movementController.RotateGameObjectLeft();
        } else {
            IsBeingExecuted = false;
        }
    }

    public void RotateRight(Transform currentTransform, MovementController movementController, SensorLogic[] sensorLogic) {
        if (currentTransform.rotation.eulerAngles.y != RotationEuler) {
            movementController.RotateGameObjectRight();
        } else {
            IsBeingExecuted = false;
        }
    }


    public override void Execute(Transform transform, MovementController movementController, SensorLogic[] sensorLogic) {
        switch (Direction) {
            case (EnumRotation.kLeft):
                RotateLeft(transform, movementController, sensorLogic);
                break;
            case (EnumRotation.kRight):
                RotateRight(transform, movementController, sensorLogic);
                break;
            default:
                break;
        }
    }
}
