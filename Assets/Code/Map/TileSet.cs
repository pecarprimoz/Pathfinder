using UnityEngine;

public class TileSet {
    private int Column;
    private int Row;
    // We need the world position aswell
    private Vector3 PositionInWorld;
    private float Offset;
    public float Value;
    public bool Visited = false;

    // Note to self
    // PositionInWorld presents the Unity position
    // PositionInWorldWithOffset presents how the roomba moves in the world
    // We want tileset movement, so we need to keep in mind the size of the object moving in the tilemap

    public TileSet(int column, int row, float offset, Vector3 positionInWorld) {
        Column = column;
        Row = row;
        PositionInWorld = positionInWorld;
        Offset = offset;
        Value = -1;
    }
    public int column {
        get { return (int)Column; }
    }
    public int row {
        get { return (int)Row; }
    }

    public Vector3 positionInWorld {
        get { return PositionInWorld; }
    }

    public Vector3 positionInWorldWithOffset
    {
        get {
            return new Vector3(PositionInWorld.x + Offset, PositionInWorld.y, PositionInWorld.z + Offset);
        }
    }

    public override string ToString() {
        return string.Format("C: {0} R: {1} POW{2}", Column, Row, PositionInWorld);
    }

}