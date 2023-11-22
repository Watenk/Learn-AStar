using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DStar
{
    //private List<Node> openNodes = new List<Node>();
    //private List<Node> closedNodes = new List<Node>();
    //private Node[,] nodes;

    //private Node agentNode;
    //private Node targetNode;
    //private Node currentNode;

    //private Vector2Int gridSize;

    ////References
    //private PlayerManager playerManager;

    //public DStar(PlayerManager playerManager)
    //{
    //    gridSize = GridManager.Instance.GridSize;
    //    nodes = new Node[gridSize.x, gridSize.y];
    //    this.playerManager = playerManager;
    //}

    //public Stack<Node> CalcNewPath(Vector2Int _agentCell, Vector2Int _targetCell)
    //{
    //    ClearNodes();

    //    agentNode = GetNode(_agentCell);
    //    targetNode = GetNode(_targetCell);
    //    currentNode = targetNode;

    //    //Calc Tiles until agentNode is reached
    //    while (currentNode != agentNode)
    //    {
    //        CalcNeighbours(currentNode);
    //        if (openNodes == null) { Debug.LogError("No Path Available"); break; }
    //        currentNode = GetLowestPending();
    //    }

    //    //Retrace path
    //    Stack<Node> path = new Stack<Node>();
    //    while (currentNode != targetNode)
    //    {
    //        path.Push(currentNode);
    //        currentNode = GetNode(currentNode.Parent);
    //    }

    //    return path;
    //}

    /////////////////////////////////////////////////////////////////

    //private void CalcNeighbours(Node _currentNode)
    //{
    //    List<Node> neighbours = new List<Node>
    //    {
    //        GetNode(new Vector2Int(_currentNode.Pos.x, _currentNode.Pos.y - 1)),
    //        GetNode(new Vector2Int(_currentNode.Pos.x, _currentNode.Pos.y + 1)),
    //        GetNode(new Vector2Int(_currentNode.Pos.x + 1, _currentNode.Pos.y)),
    //        GetNode(new Vector2Int(_currentNode.Pos.x - 1, _currentNode.Pos.y))
    //    };

    //    foreach (Node currentNode in neighbours)
    //    {
    //        if (currentNode != null)
    //        {
    //            CalcNode(currentNode, _currentNode.Pos);
    //        }
    //    }
    //}

    //private void CalcNode(Node _currentCell, Vector2Int _parent)
    //{
    //    int CellMoveCost = GridManager.Instance.GetCell(_currentCell.Pos).CellMoveCost;
    //    if (CellMoveCost < 0) { return; }

    //    //To Do Inprove G-Cost accuracy
    //    int GCost = CalcDistance(_currentCell, agentNode);
    //    int HCost = CalcDistance(_currentCell, targetNode);

    //    _currentCell.SetNode(_parent, GCost + HCost + CellMoveCost, GCost, HCost);

    //    playerManager.InstantiateDebugValue(_currentCell);

    //    openNodes.Add(_currentCell);
    //}

    //private int CalcDistance(Node _cell1, Node _cell2)
    //{
    //    int xDifference = Mathf.Abs(_cell1.Pos.x - _cell2.Pos.x);
    //    int yDifference = Mathf.Abs(_cell1.Pos.y - _cell2.Pos.y);
    //    return xDifference + yDifference;
    //}

    //private Node GetLowestPending()
    //{
    //    Node lowestNode = openNodes[0];

    //    for (int i = 0; i < openNodes.Count; i++)
    //    {
    //        if (openNodes[i].FCost < lowestNode.FCost)
    //        {
    //            lowestNode = openNodes[i];
    //        }
    //    }

    //    openNodes.Remove(lowestNode);
    //    return lowestNode;
    //}

    ////Node Grid Functions
    //////////////////////////////////////////////////////////////////

    //private void ClearNodes()
    //{
    //    for (int y = 0; y < gridSize.y; y++)
    //    {
    //        for (int x = 0; x < gridSize.x; x++)
    //        {
    //            nodes[x, y] = new Node(new Vector2Int(x, y));
    //        }
    //    }
    //}

    //private Node GetNode(Vector2Int _pos)
    //{
    //    if (IsInBounds(_pos))
    //    {
    //        return nodes[_pos.x, _pos.y];
    //    }

    //    return null;
    //}

    //private bool IsInBounds(Vector2Int _pos)
    //{
    //    if (_pos.x > 0 && _pos.x < gridSize.x)
    //    {
    //        if (_pos.y > 0 && _pos.y < gridSize.y)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}
}
