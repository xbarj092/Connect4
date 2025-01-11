using UnityEngine;

public class BoardTile : MonoBehaviour
{
    // promìnná je vidìt v inspektoru pokud má parametr SerializeField
    [SerializeField] private SpriteRenderer _renderer;

    public void SetColor(Color color)
    {
        _renderer.color = color;
    }
}
