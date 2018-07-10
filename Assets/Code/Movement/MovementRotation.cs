using UnityEngine;

public class MovementRotation : MovementCommand
{
    private float RotationTime = 0.0f;
    private float RotationSpeed = .5f;
    private Vector3 CurrentEulerAngles = new Vector3(-1.0f, -1.0f, -1.0f);

    public MovementRotation(Vector3 startPosition, Vector3 endPosition, TilemapController tilemapController) : base(startPosition, endPosition, tilemapController) {
        IsBeingExecuted = true;
    }

    public MovementRotation(int startColumn, int startRow, int endColumn, int endRow, TilemapController tilemapController) : base(startColumn, startRow, endColumn, endRow, tilemapController) {
        IsBeingExecuted = true;
    }

    public void Rotate(Transform transform, MovementController movementController, SensorLogic[] sensorLogic) {
        if (CurrentEulerAngles!= transform.eulerAngles) {
            CurrentEulerAngles = transform.eulerAngles;
            RotationTime += Time.deltaTime * RotationSpeed;
            var lookAtPoint = EndPosition.positionInWorldWithOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtPoint - transform.position), RotationTime);
        } else {
            IsBeingExecuted = false;
        }
    }

    public override void Execute(Transform transform, MovementController movementController, SensorLogic[] sensorLogic)
    {
        Rotate(transform, movementController, sensorLogic);
    }
}
