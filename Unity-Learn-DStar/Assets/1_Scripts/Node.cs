using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int Pos { get; private set; }
    public Vector2Int Parent { get; private set; }
    public int FCost { get; private set; }
    public int GCost { get; private set; }
    public int HCost { get; private set; }

    public Node(Vector2Int _pos)
    {
        Pos = _pos;
        Parent = new Vector2Int(-1, -1);
        FCost = -1;
        GCost = -1;
        HCost = -1;
    }

    public void SetNode(Vector2Int _parent, int _fCost, int _gCost, int _hCost)
    {
        Parent = _parent;
        FCost = _fCost;
        GCost = _gCost;
        HCost = _hCost;
    }
}
