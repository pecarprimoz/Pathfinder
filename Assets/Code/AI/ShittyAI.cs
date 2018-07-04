using System.Collections.Generic;
using UnityEngine;

public partial class ShittyAI : MonoBehaviour {
    public TilemapController CurrentTilemapController;
    public SensorLogic[] AllSensorsLogic;

    // CommandQueue is used to tell the AI how to move
    private Queue<MovementCommand> CommandQueue;
    // Current command that is being executed, need for update
    private MovementCommand ExectuingCommand = null;
    private MovementController CurrentMovementController;

    void Start() {
        CurrentMovementController = GetComponent<MovementController>();
        CommandQueue = new Queue<MovementCommand>();
        CommandQueue.Enqueue(new MovementMove(transform.position,CurrentTilemapController,1, MovementMove.EnumDirection.kForward));
        CommandQueue.Enqueue(new MovementMove(transform.position, CurrentTilemapController, 1, MovementMove.EnumDirection.kForward));
        CommandQueue.Enqueue(new MovementRotation(transform.position, CurrentTilemapController, 90, MovementRotation.EnumRotation.kRight));
        CommandQueue.Enqueue(new MovementMove(transform.position, CurrentTilemapController, 1, MovementMove.EnumDirection.kForward));
        CommandQueue.Enqueue(new MovementMove(transform.position, CurrentTilemapController, 1, MovementMove.EnumDirection.kForward));
        CommandQueue.Enqueue(new MovementRotation(transform.position, CurrentTilemapController, 90, MovementRotation.EnumRotation.kLeft));
        CommandQueue.Enqueue(new MovementMove(transform.position, CurrentTilemapController, 1, MovementMove.EnumDirection.kForward));
    }

    void Update() {
        if (ExectuingCommand != null) {
            if (ExectuingCommand.isBeingExecuted) {
                ExecuteCommand();
            } else {
                ExectuingCommand = null;
            }
        } else {
            if (CommandQueue.Count > 0) {
                ExectuingCommand = CommandQueue.Dequeue();
                ExectuingCommand.startPosition = transform.position;
            } else {

            }
        }
    }

    private void ExecuteCommand() {
        ExectuingCommand.Execute(transform, CurrentMovementController, AllSensorsLogic);
    }
}
