using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2Int Pos { get; private set; }
    public GameObject GameObject { get; private set; }
    public int CellMoveCost { get; private set; }

    public Cell(Vector2Int _pos, GameObject _gameObject, int _tileMoveCost)
    {
        Pos = _pos;
        GameObject = _gameObject;
        CellMoveCost = _tileMoveCost;
    }
}
