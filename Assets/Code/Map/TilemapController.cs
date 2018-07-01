using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapController : MonoBehaviour {
    public class TileSet {
        private int TileSetWidth = 1;
        private int TileSetHeight = 1;

        private int Column;
        private int Row;
        // We need the world position aswell
        private Vector3 PositionInWorld;

        public TileSet(int column, int row, Vector3 positionInWorld) {
            Column = column;
            Row = row;
            PositionInWorld = positionInWorld;
        }
        public int column {
            get { return Column; }
        }
        public int row {
            get { return Row; }
        }
        public Vector3 positionInWorld {
            get { return PositionInWorld; }
        }
    }
    private Vector3 RoombaDimensions = new Vector3(1, 0.2f, 1);

    private int ColumnsWidth;
    private int ColumnsHeight;
    private Vector3 TrackMeshRendererBoundsSize;

    public TileSet[,] TileMapList;

    void Start() {
        TrackMeshRendererBoundsSize = GetComponent<MeshRenderer>().bounds.size;
        ColumnsWidth = (int)(TrackMeshRendererBoundsSize.x / RoombaDimensions.x);
        ColumnsHeight = (int)(TrackMeshRendererBoundsSize.z / RoombaDimensions.z);
        TileMapList = new TileSet[ColumnsWidth, ColumnsHeight];
        for (int i = 0; i < ColumnsWidth; ++i) {
            for (int j = 0; j < ColumnsHeight; ++j) {
                TileMapList[i, j] = new TileSet(i, j, new Vector3(i - TrackMeshRendererBoundsSize.x / 2, 1f, j - TrackMeshRendererBoundsSize.z / 2));
            }
        }
        // Debug.Log(TileMapList);
    }
    void Update() {
        //DEBUG
        for (int i = 0; i < ColumnsWidth; i++) {
            Debug.DrawLine(TileMapList[i, 0].positionInWorld, new Vector3(TileMapList[i, 0].positionInWorld.x, 1, TileMapList[i, 0].positionInWorld.z + ColumnsHeight), Color.red);
        }
        for (int i = 0; i < ColumnsHeight; i++) {
            Debug.DrawLine(TileMapList[0, i].positionInWorld, new Vector3(TileMapList[0, i].positionInWorld.x + ColumnsWidth, 1, TileMapList[0, i].positionInWorld.z), Color.cyan);
        }
    }
    public TileSet GetTileSetForPosition(Vector3 position) {
        float minimumDistance = float.MaxValue;
        int bi = -1;
        int bj = -1;
        for (int i = 0; i < ColumnsWidth; ++i) {
            for (int j = 0; j < ColumnsHeight; ++j) {
                float currentDistance = Vector3.Distance(TileMapList[i, j].positionInWorld, position);
                if (currentDistance < minimumDistance) {
                    minimumDistance = currentDistance;
                    bi = i;
                    bj = j;
                }
            }
        }
        return TileMapList[bi, bj];
    }
}
