using System;

[Serializable]
public class GridNode
{
    private Grid<GridNode> _grid;

    public int X;
    public int Y;

    public bool Red;
    public bool Blue;

    public BoardTile BoardTile;
    //Konstruktor pro sestavení políèek v møížce
    public GridNode(Grid<GridNode> grid, int x, int y)
    {
        _grid = grid;
        X = x;
        Y = y;
    }
}
