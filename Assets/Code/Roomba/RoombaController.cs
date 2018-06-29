using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaController : MonoBehaviour {

    public GameObject FinishLineGameObject;
    public bool IsPlayerControlled;
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

	}
}
