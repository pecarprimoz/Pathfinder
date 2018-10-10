using UnityEngine;

public enum MovementType { kMove, kRotate };
public abstract class MovementCommand
{
    protected TilemapController TilemapController;

    protected bool IsBeingExecuted = false;

    protected TileSet StartPosition;
    protected TileSet EndPosition;


    protected MovementCommand(Vector3 startPosition, Vector3 endPosition, TilemapController tilemapController)
    {
        TilemapController = tilemapController;
        StartPosition = tilemapController.GetTileSetFromPosition(startPosition);
        EndPosition = tilemapController.GetTileSetFromPosition(startPosition);
        
        if (StartPosition == null || EndPosition == null)
        {
            // DEBUG ERROR
            return;
        }
    }

    protected MovementCommand(int startColumn, int startRow, int endColumn, int endRow, TilemapController tilemapController)
    {
        TilemapController = tilemapController;
        StartPosition = tilemapController.GetTileSetFromColRow(startColumn,startRow);
        EndPosition = tilemapController.GetTileSetFromColRow(endColumn, endRow);

        if (StartPosition == null || EndPosition == null)
        {
            // DEBUG ERROR
            return;
        }
    }

    public bool isBeingExecuted { get { return IsBeingExecuted; } }
    
    public abstract void Execute(Transform transform, MovementController movementController);
}
