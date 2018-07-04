using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MovementRotation : MovementCommand
{
    protected MovementRotation(Vector3 startPosition, Vector3 endPosition, TilemapController tilemapController) : base(startPosition, endPosition, tilemapController)
    {
    }

    protected MovementRotation(int startColumn, int startRow, int endColumn, int endRow, TilemapController tilemapController) : base(startColumn, startRow, endColumn, endRow, tilemapController)
    {
    }

    public override void Execute(Transform transform, MovementController movementController, SensorLogic[] sensorLogic)
    {
        throw new NotImplementedException();
    }
}
