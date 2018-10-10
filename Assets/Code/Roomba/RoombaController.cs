using UnityEngine;

public class RoombaController : MonoBehaviour {

    public bool IsPlayerControlled;
    public TilemapController CurrentTilemapController;
    public SensorLogic[] AllSensorsLogic;
    public State CurrentState;


    void Start() {
        // Set the controls
        if (IsPlayerControlled) {
            GetComponent<MovementController>().enabled = true;
            GetComponent<ShittyAI>().enabled = false;
        } else {
            GetComponent<ShittyAI>().enabled = true;
        }
        // Set the starting position
        gameObject.transform.position = CurrentTilemapController.StartTile.positionInWorldWithOffset;
        // Set the state
        CurrentState = State.kSeaching;
    }
    public TileSet GetCurrentRoombaTileSet() {
        return CurrentTilemapController.GetTileSetFromPosition(transform.position);
    }

    // maybe rewrite the sensors entirely with better logic ? 
    // now we heavily depend on the garbage sensor distance, could just check intersections from the sensor
    public bool WillHitWall() {
        for (int i = 0; i < AllSensorsLogic.Length; ++i) {
            var currentSensor = AllSensorsLogic[i];
            if (currentSensor.IRRaycastHit.distance < 1.5f && currentSensor.IRRaycastHit.distance != 0) {
                Debug.LogWarningFormat("Sensor {0} returned true !", i);
                currentSensor.IsColliding = true;
                return true;
            }
        }
        return false;
    }

    void Update() {
        WillHitWall();
    }
}
