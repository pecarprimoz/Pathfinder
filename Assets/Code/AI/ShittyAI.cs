using System.Collections.Generic;
using UnityEngine;

public partial class ShittyAI : MonoBehaviour
{
    public TilemapController CurrentTilemapController;
    public SensorLogic[] AllSensorsLogic;

    // CommandQueue is used to tell the AI how to move
    private Queue<MovementCommand> CommandQueue;
    // Current command that is being executed, need for update
    private MovementCommand ExectuingCommand = null;
    private MovementController CurrentMovementController;

    void Start()
    {
        CurrentMovementController = GetComponent<MovementController>();
        CommandQueue = new Queue<MovementCommand>();
        CommandQueue.Enqueue(new MovementMove(0, 0, 3, 0, CurrentTilemapController));
        var quat = new Quaternion();
        quat.eulerAngles = new Vector3(0, 90, 0);
        CommandQueue.Enqueue(new MovementRotation(transform.rotation, quat, CurrentTilemapController));
        var current = CurrentTilemapController.GetTileSetFromPosition(transform.position);

    }

    void Update()
    {
        if (ExectuingCommand != null)
        {
            if (ExectuingCommand.isBeingExecuted)
            {
                ExecuteCommand();
            }
            else
            {
                ExectuingCommand = null;
            }
        }
        else
        {
            if (CommandQueue.Count > 0)
            {
                ExectuingCommand = CommandQueue.Dequeue();
            }
        }
    }

    private void ExecuteCommand()
    {
        ExectuingCommand.Execute(transform, CurrentMovementController, AllSensorsLogic);
    }
}
