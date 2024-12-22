using System.Collections.Generic;
using UnityEngine;

public class TileSpawner
{
    private Grid<GridNode> _grid;

    public void SpawnTiles(Grid<GridNode> grid, BoardTile tilePrefab, Column columnPrefab, Transform spawnTransform)
    {
        _grid = grid;

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            List<GridNode> nodes = new();

            for (int y = 0; y < grid.GetHeight(); y++)
            {
                nodes.Add(PlaceBoardTile(x, y, tilePrefab, spawnTransform));
            }

            PlaceColumn(x, columnPrefab, spawnTransform, nodes);
        }
    }

    private void PlaceColumn(int x, Column columnPrefab, Transform spawnTransform, List<GridNode> nodes)
    {
        Column column = Object.Instantiate(columnPrefab, spawnTransform);
        column.transform.position = _grid.GetWorldPosition(x, _grid.GetHeight() / 2);
        column.Init(_grid.GetHeight(), nodes);
    }

    private GridNode PlaceBoardTile(int x, int y, BoardTile tilePrefab, Transform spawnTransform)
    {
        GridNode gridNode = _grid.GetGridObject(x, y);
        BoardTile boardTile = Object.Instantiate(tilePrefab, _grid.GetWorldPosition(x, y), Quaternion.identity, spawnTransform);
        gridNode.BoardTile = boardTile;
        _grid.SetGridObject(x, y, gridNode);
        return gridNode;
    }
}
