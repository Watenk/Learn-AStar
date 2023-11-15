using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2Int Pos { get; private set; }
    public GameObject GameObject { get; private set; }
    public int TileMoveCost { get; private set; }

    public Cell(Vector2Int _pos, GameObject _gameObject, int _tileMoveCost)
    {
        Pos = _pos;
        GameObject = _gameObject;
        TileMoveCost = _tileMoveCost;
    }
}
