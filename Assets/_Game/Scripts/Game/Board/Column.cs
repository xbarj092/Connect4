using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Column : MonoBehaviour
{
    [SerializeField] private Image _highlight;
    [SerializeField] private BoxCollider _collider;
    
    private List<GridNode> _nodes = new();
    private bool _highlighted;

    public void Init(float height, List<GridNode> nodes)
    {
        _nodes = nodes;
        _collider.size = new Vector3(_collider.size.x, height, _collider.size.z);
        _highlight.transform.position = new Vector3(_highlight.transform.position.x, height, _highlight.transform.position.z);
    }

    public void SetHighlight(bool highlighted)
    {
        if (_highlighted == highlighted)
        {
            return;
        }

        _highlighted = highlighted;
        _highlight.enabled = highlighted;
    }

    public void PlaceToken(GameBoard gameBoard)
    {
        foreach (GridNode node in _nodes)
        {
            if (!node.Red && !node.Blue)
            {
                bool isRedTurn = GameManager.Instance.IsRedTurn;

                if (isRedTurn)
                {
                    node.Red = true;
                    node.BoardTile.SetColor(Color.red);
                }
                else
                {
                    node.Blue = true;
                    node.BoardTile.SetColor(Color.blue);
                }

                // node.BoardTile.TriggerPlacementEffect();
                gameBoard.CheckWinCondition(node);
                GameManager.Instance.SwitchTurn();
                return;
            }
        }
    }

    public bool IsOccupied() => !_nodes.Any(node => !node.Blue && !node.Red);
}
