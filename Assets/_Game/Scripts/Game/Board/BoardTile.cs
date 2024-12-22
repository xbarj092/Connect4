using System;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    internal void SetColor(Color color)
    {
        _renderer.color = color;
    }
}
