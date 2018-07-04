using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MovementRotation : MovementCommand {
    public enum EnumRotation { kLeft, kRight };
    private float RotationEuler;
    private EnumRotation Direction;

    protected MovementRotation(Vector3 startPosition, TilemapController tilemapController, float rotationEuler, EnumRotation direction) : base(startPosition, tilemapController) {
        IsBeingExecuted = true;
        RotationEuler = rotationEuler;
        Direction = direction;
    }

    public override void Execute(Transform transform, MovementController movementController, SensorLogic[] sensorLogic) {
        switch (Direction) {
            case (EnumRotation.kLeft):
                break;
            case (EnumRotation.kRight):
                break;
            default:
                break;
        }
    }
}
