using UnityEngine;

public class TileSet {

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