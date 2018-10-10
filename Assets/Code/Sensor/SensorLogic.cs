using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ir sensor should work up to 15cm ingame, need to figure out dimensions for this

public class SensorLogic : MonoBehaviour {

    public RaycastHit IRRaycastHit;
    public bool IsColliding = false;
    private float RayDistance = 1.5f;

    void Update() {
        Debug.DrawRay(transform.position, transform.forward * RayDistance, Color.red);
        IRSensorSimulation();
    }
    void IRSensorSimulation() {
        Physics.Raycast(transform.position, transform.forward, out IRRaycastHit, RayDistance);
        // Debug.Log(IRRaycastHit.distance);
    }
}
