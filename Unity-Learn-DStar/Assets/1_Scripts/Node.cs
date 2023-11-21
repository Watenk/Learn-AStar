using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int Pos { get; private set; }
    public Vector2Int Parent;
    public int FCost;

    public Node(Vector2Int _pos)
    {
        Pos = _pos;
        Parent = new Vector2Int(-1, -1);
        FCost = -1;
    }
}
