using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapController : MonoBehaviour {

    private Vector3 RoombaDimensions = new Vector3(1, 0.2f, 1);
    private int Columns;
    private int Rows;
    private Vector3 TrackMeshRendererBoundsSize;
    public TileSet[,] TileMapList;

    void Awake() {
        TrackMeshRendererBoundsSize = GetComponent<MeshRenderer>().bounds.size;
        Columns = (int)(TrackMeshRendererBoundsSize.x / RoombaDimensions.x);
        Rows = (int)(TrackMeshRendererBoundsSize.z / RoombaDimensions.z);
        TileMapList = new TileSet[Columns, Rows];

        for (int i = 0; i < Columns; ++i) {
            for (int j = 0; j < Rows; ++j) {
                // note for myself since i wont remember wtf is going on here
                // since the world is displayed in 1 by 1 sizes we can get away with just adding columns and rows, we need to remove bounds size/2 because the center of gameobjects in unity is in the center, but we want it in the bottom left
                TileMapList[i, j] = new TileSet(i, j, RoombaDimensions.x / 2, new Vector3(transform.position.x + i - TrackMeshRendererBoundsSize.x / 2, 1f, transform.position.z + j - TrackMeshRendererBoundsSize.z / 2));
            }
        }
        // Debug.Log(TileMapList);
    }
    void Update() {
        //DEBUG
        for (int i = 0; i < Columns; i++) {
            Debug.DrawLine(TileMapList[i, 0].positionInWorld, new Vector3(TileMapList[i, 0].positionInWorld.x, 1, TileMapList[i, 0].positionInWorld.z + Rows), Color.red);
        }
        for (int i = 0; i < Rows; i++) {
            Debug.DrawLine(TileMapList[0, i].positionInWorld, new Vector3(TileMapList[0, i].positionInWorld.x + Columns, 1, TileMapList[0, i].positionInWorld.z), Color.cyan);
        }
    }
    public TileSet GetTileSetFromPosition(Vector3 position) {
        // lmao i can just use this thanks math
        int currentZ = (int)Mathf.Floor(position.z);
        int currentX = (int)Mathf.Floor(position.x);
        if (CheckRows(currentZ)) {
            if (CheckColumns(currentX)) {
                return TileMapList[(int)position.x, (int)position.z];
            }
        }
        return null;
    }
    public TileSet GetTileSetFromColRow(int columns, int rows) {
        if (!CheckColumns(columns)) {
            return null;
        }
        if (!CheckRows(rows)) {
            return null;
        }
        return TileMapList[columns, rows];
    }

    private bool CheckColumns(int col) {
        if (col > Columns || col < 0) {
            Debug.LogErrorFormat("ERROR, COLUMN MISSMATCH GOT {0}, MAX IS {1}, MIN IS 0", col, Columns);
            return false;
        }
        return true;
    }
    private bool CheckRows(int row) {
        if (row > Rows || row < 0) {
            Debug.LogErrorFormat("ERROR, ROW MISSMATCH GOT {0}, MAX IS {1}", row, Rows);
            return false;
        }
        return true;
    }
}
