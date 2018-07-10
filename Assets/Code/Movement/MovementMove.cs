using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MovementMove : MovementCommand
{
    private float MovementSpeed = 2.0f;
    private float MovementLength;

    public MovementMove(Vector3 startPosition, Vector3 endPosition, TilemapController tilemapController) : base(startPosition, endPosition, tilemapController)
    {
        IsBeingExecuted = true;
        MovementLength = Vector3.Distance(StartPosition.positionInWorldWithOffset, EndPosition.positionInWorldWithOffset);
    }

    public MovementMove(int startColumn, int startRow, int endColumn, int endRow, TilemapController tilemapController) : base(startColumn, startRow, endColumn, endRow, tilemapController)
    {
        IsBeingExecuted = true;
        MovementLength = Vector3.Distance(StartPosition.positionInWorldWithOffset, EndPosition.positionInWorldWithOffset);
    }

    public void Move(Transform transform, MovementController movementController, SensorLogic[] sensorLogic)
    {
        if (transform.position != EndPosition.positionInWorldWithOffset)
        {
            MovementTime += Time.deltaTime * MovementSpeed;
            transform.position = Vector3.Lerp(StartPosition.positionInWorldWithOffset, EndPosition.positionInWorldWithOffset, MovementTime / MovementLength);
        }
        else
        {
            IsBeingExecuted = false;
        }
    }


    public override void Execute(Transform transform, MovementController movementController, SensorLogic[] sensorLogic)
    {
        Move(transform, movementController, sensorLogic);
    }
}