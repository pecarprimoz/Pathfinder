using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MovementRotation : MovementCommand
{
    private float RotationSpeed = 1.0f;

    protected Quaternion StartRotation;
    protected Quaternion EndRotation;
    // how do quaterions work
    public MovementRotation(Quaternion startRotation, Quaternion endRotation, TilemapController tilemapController) : base(startRotation, endRotation, tilemapController) {
        StartRotation = startRotation;
        EndRotation = endRotation;
        IsBeingExecuted = true;
    }

    public void Rotate(Transform transform, MovementController movementController, SensorLogic[] sensorLogic) {
        if (transform.rotation != EndRotation) {
            MovementTime += Time.deltaTime * RotationSpeed;
            transform.rotation = Quaternion.Slerp(StartRotation, EndRotation, MovementTime);

        } else {
            IsBeingExecuted = false;
        }
    }

    public override void Execute(Transform transform, MovementController movementController, SensorLogic[] sensorLogic)
    {
        Rotate(transform, movementController, sensorLogic);
    }
}
