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

        public override string ToString() {
            return string.Format("C: {0} R: {1} POW{2}", Column, Row, PositionInWorld);
        }
    }
    private Vector3 RoombaDimensions = new Vector3(1, 0.2f, 1);
    private int Columns;
    private int Rows;
    private Vector3 TrackMeshRendererBoundsSize;
    private Vector3 ParentOffset;
    public TileSet[,] TileMapList;

    void Start() {
        ParentOffset = GetComponentInParent<Transform>().transform.position;
        TrackMeshRendererBoundsSize = GetComponent<MeshRenderer>().bounds.size;
        Columns = (int)(TrackMeshRendererBoundsSize.x / RoombaDimensions.x);
        Rows = (int)(TrackMeshRendererBoundsSize.z / RoombaDimensions.z);
        TileMapList = new TileSet[Columns, Rows];
        
        for (int i = 0; i < Columns; ++i) {
            for (int j = 0; j < Rows; ++j) {
                // note for myself since i wont remember wtf is going on here
                // since the world is displayed in 1 by 1 sizes we can get away with just adding columns and rows, we need to remove bounds size/2 because the center of gameobjects in unity is in the center, but we want it in the bottom left
                TileMapList[i, j] = new TileSet(i, j, new Vector3(transform.position.x+i - TrackMeshRendererBoundsSize.x / 2, 1f, transform.position.z + j - TrackMeshRendererBoundsSize.z / 2));
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
    public TileSet GetTileSetForPosition(Vector3 position) {
        /*
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
        */
        // lmao i can just use this thanks math
        int currentZ = (int)Mathf.Floor(position.z);
        int currentX = (int)Mathf.Floor(position.x);
        if (currentZ < Rows) {
            if(currentX < Columns){ 
                return TileMapList[(int)position.x, (int)position.z];
            }
        }
        Debug.LogErrorFormat("ERROR, INDICIES MISSTMATCH, FOR X GOT {0}, MAX IS {1}; FOR Z GOT {2}, MAX IS {3}", currentX, Columns, currentZ, Rows);
        return null;
    }
    public Vector3 GetPositionFromTileset(int columnsWidth, int columnsHeight) {
        if (columnsWidth > Columns) {
            Debug.LogErrorFormat("ERROR, COLUMN WIDTH MISSMATCH GOT {0}, MAX IS {1}",columnsWidth,Columns);
            return new Vector3(0,0,0);
        }
        if (columnsHeight > Rows) {
            Debug.LogErrorFormat("ERROR, COLUMN HEIGHT MISSMATCH GOT {0}, MAX IS {1}", columnsHeight, Rows);
            return new Vector3(0, 0, 0);
        }
        return TileMapList[columnsWidth, columnsHeight].positionInWorld;
    }
}
