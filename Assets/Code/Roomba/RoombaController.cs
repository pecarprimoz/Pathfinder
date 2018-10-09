using UnityEngine;

public class RoombaController : MonoBehaviour {

    public bool IsPlayerControlled;
    public TilemapController CurrentTilemapController;

    void Start() {
        // Set the controls
        if (IsPlayerControlled) {
            GetComponent<MovementController>().enabled = true;
        } else {
            GetComponent<ShittyAI>().enabled = true;
        }
        // Set the starting position
        gameObject.transform.position = CurrentTilemapController.StartTile.positionInWorldWithOffset;
    }
    public TileSet GetCurrentRoombaTileSet(){ 
        return CurrentTilemapController.GetTileSetFromPosition(transform.position);
    }
	
	void Update () {
        // Debug.LogFormat("X{0} Z{1}", CurrentTilemapController.GetTileSetForPosition(transform.position).column, CurrentTilemapController.GetTileSetForPosition(transform.position).row);
    }
}
