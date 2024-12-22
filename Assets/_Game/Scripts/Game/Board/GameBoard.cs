using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private BoardTile _boardTilePrefab;
    [SerializeField] private Column _columnPrefab;

    private TileSpawner _tileSpawner = new();
    private Grid<GridNode> _grid;
    public Grid<GridNode> Grid => _grid;

    private void Awake()
    {
        _grid = new(GameManager.Instance.ColumnCount, GameManager.Instance.RowCount, 1, (Grid<GridNode> g, int x, int y) => new GridNode(g, x, y));
        _tileSpawner.SpawnTiles(_grid, _boardTilePrefab, _columnPrefab, transform);
        transform.position = new(-(GameManager.Instance.ColumnCount / 2f) + 0.5f, -(GameManager.Instance.RowCount / 2f) - 0.5f, 0);
    }

    public void CheckWinCondition(GridNode lastPlacedNode)
    {
        bool gameEnded = false;
        Vector2Int[] directions =
        {
            new(1, 0),
            new(0, 1),
            new(1, 1),
            new(1, -1),
        };

        foreach (Vector2Int dir in directions)
        {
            if (CountConsecutiveTokens(lastPlacedNode, dir, GameManager.Instance.IsRedTurn) >= 4)
            {
                GameManager.Instance.PlayerOneWon = GameManager.Instance.IsRedTurn;
                gameEnded = true;
                break;
            }
        }

        if (IsTie())
        {
            GameManager.Instance.IsTie = true;
            gameEnded = true;
        }

        if (gameEnded)
        {
            ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.GameEnd);
        }
    }

    private bool IsTie()
    {
        bool tie = true;

        for (int i = 0; i < _grid.GetHeight(); i++)
        {
            for (int j = 0; j < _grid.GetWidth(); j++)
            {
                if (!_grid.GetGridObject(i, j).Red && !_grid.GetGridObject(i, j).Blue)
                {
                    tie = false;
                    break;
                }
            }
        }

        return tie;
    }

    private int CountConsecutiveTokens(GridNode startNode, Vector2Int direction, bool isRed)
    {
        int count = 1;
        count += CountInDirection(startNode, direction, isRed);
        count += CountInDirection(startNode, -direction, isRed);
        return count;
    }

    private int CountInDirection(GridNode startNode, Vector2Int direction, bool isRed)
    {
        int count = 0;
        int x = startNode.X;
        int y = startNode.Y;

        while (true)
        {
            x += direction.x;
            y += direction.y;

            if (x < 0 || x >= _grid.GetWidth() || y < 0 || y >= _grid.GetHeight())
            {
                break;
            }

            GridNode node = _grid.GetGridObject(x, y);
            if ((isRed && node.Red) || (!isRed && node.Blue))
            {
                count++;
            }
            else
            {
                break;
            }
        }

        return count;
    }
}
