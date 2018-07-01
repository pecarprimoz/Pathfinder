﻿using UnityEngine;

public class RoombaController : MonoBehaviour {

    public GameObject FinishLineGameObject;
    public bool IsPlayerControlled;
    public TilemapController CurrentTilemapController;

    private Vector3 RoombaStartPosition;

    void Start() {
        RoombaStartPosition = transform.position;
        if (IsPlayerControlled) {
            GetComponent<MovementController>().enabled = true;
        } else {
            GetComponent<ShittyAI>().enabled = true;
        }
    }
	
	void Update () {
        // Debug.LogFormat("X{0} Z{1}", CurrentTilemapController.GetTileSetForPosition(transform.position).column, CurrentTilemapController.GetTileSetForPosition(transform.position).row);
    }
}
