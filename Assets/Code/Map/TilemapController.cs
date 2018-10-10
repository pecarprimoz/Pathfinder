using System.Collections.Generic;
using UnityEngine;

public class TilemapController : MonoBehaviour {

    private Vector3 RoombaDimensions = new Vector3(1, 0.2f, 1);
    private int Columns;
    private int Rows;
    private Vector3 TrackMeshRendererBoundsSize;

    public TileSet StartTile;
    public TileSet FinishTile;
    private TileSet[,] TileMapList;
    public Queue<TileSet> ClosedTileMapList;
    public Queue<TileSet> OpenTileMapList;


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
        SetStartTile();
        SetFinishTile();
        GenerateValuesForTileMap();
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
        // may be an issue if we dont use tilemap mopv
        foreach (TileSet tileSet in TileMapList) {
            if (tileSet.positionInWorldWithOffset == position) {
                return tileSet;
            }
        }
        Debug.LogErrorFormat("COULD NOT FIND VALID TILE");
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
        if (col >= Columns || col < 0) {
            Debug.LogErrorFormat("ERROR, COLUMN MISSMATCH GOT {0}, MAX IS {1}, MIN IS 0", col, Columns);
            return false;
        }
        return true;
    }

    private bool CheckRows(int row) {
        if (row >= Rows || row < 0) {
            Debug.LogErrorFormat("ERROR, ROW MISSMATCH GOT {0}, MAX IS {1}", row, Rows);
            return false;
        }
        return true;
    }

    public void SetStartTile() {
        StartTile = TileMapList[0, 0];
        StartTile.Visited = true;
    }

    public void ClearTileMap() {
        for (int i = 0; i < Columns; ++i) {
            for (int j = 0; j < Rows; ++j) {
                // note for myself since i wont remember wtf is going on here
                // since the world is displayed in 1 by 1 sizes we can get away with just adding columns and rows, we need to remove bounds size/2 because the center of gameobjects in unity is in the center, but we want it in the bottom left
                TileMapList[i, j].Visited = false;
            }
        }
    }

    public void SetFinishTile() {
        //(int)Random.Range(0, Columns), (int)Random.Range(0, Rows)
        //FinishTile = TileMapList[Columns - 1, Rows - 1];
        FinishTile = TileMapList[(int)Random.Range(0, Columns), (int)Random.Range(0, Rows)];
    }

    public void GenerateValuesForTileMap() {
        for (int i = 0; i < Columns; ++i) {
            for (int j = 0; j < Rows; ++j) {
                var currentTileSet = TileMapList[i, j];
                // manhatten distance abs(x1 - x2) + abs(y1 - y2)
                currentTileSet.Value = CalculateDistance(currentTileSet, FinishTile);
            }
        }
    }

    public List<TileSet> GetNeighboursForTileSet(TileSet currentTileSet) {
        // need to check 8 tiles for all possible neighbours
        List<TileSet> neighbours = new List<TileSet>();
        var cc = currentTileSet.column;
        var cr = currentTileSet.row;
        if (cc + 1 < Columns) {

            // top left
            if (cr - 1 > 0) {
                neighbours.Add(TileMapList[cc + 1, cr - 1]);
            }
            // top top
            neighbours.Add(TileMapList[cc + 1, cr]);

            // top right
            if (cr + 1 < Rows) {
                neighbours.Add(TileMapList[cc + 1, cr + 1]);
            }
        }
        if (cr - 1 > 0) {
            // left center
            neighbours.Add(TileMapList[cc, cr - 1]);
        }
        if (cr + 1 < Rows) {
            // right center
            neighbours.Add(TileMapList[cc, cr + 1]);
        }
        if (cc - 1 > 0) {
            if (cr - 1 > 0) {
                // bottom left
                neighbours.Add(TileMapList[cc - 1, cr - 1]);
            }
            // bottom bottom 
            neighbours.Add(TileMapList[cc - 1, cr]);
            if (cr + 1 < Rows) {
                // bottom right
                neighbours.Add(TileMapList[cc - 1, cr + 1]);
            }
        }
        return neighbours;
    }

    public float CalculateDistance(TileSet a, TileSet b) {
        return Mathf.Abs(a.positionInWorld.x - b.positionInWorld.x) + Mathf.Abs(a.positionInWorld.z - a.positionInWorld.z);
    }

    public void SetVisited(int col, int row) {
        TileMapList[col, row].Visited = true;
    }
}
