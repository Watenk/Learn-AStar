using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DStar
{
    private Vector2Int gridSize;
    private List<Node> pendingNodes;
    private Node[,] nodes;
    private Node agentNode;
    private Node startNode;
    private Node currentNode;

    public DStar()
    {
        gridSize = GridManager.Instance.GridSize;
        nodes = new Node[gridSize.x, gridSize.y];
    }

    public Stack<Vector2Int> CalcPath(Vector2Int _currentCell, Vector2Int _targetCell)
    {
        ClearNodes();

        agentNode = new Node(_currentCell);
        startNode = new Node(_targetCell);
        currentNode = startNode;

        //Calc Tiles until startcell is reached
        while (currentNode != agentNode) 
        {
            CalcNeighbours(currentNode);
            if (pendingNodes == null) { Debug.LogError("No Path Available"); break; }

            currentNode = GetLowestPending();

            Debug.Log("While");
        }

        //Retrace path
        Stack<Vector2Int> path = new Stack<Vector2Int>();
        while (currentNode != startNode)
        {
            path.Push(currentNode.Pos);
            currentNode = GetNode(currentNode.Parent);
        }

        return path;
    }

    //////////////////////////////////////////////////////////////

    private void CalcNeighbours(Node _currentCell)
    {
        List<Node> neighbours = new List<Node>
        {
            GetNode(new Vector2Int(_currentCell.Pos.x, _currentCell.Pos.y - 1)),
            GetNode(new Vector2Int(_currentCell.Pos.x, _currentCell.Pos.y + 1)),
            GetNode(new Vector2Int(_currentCell.Pos.x + 1, _currentCell.Pos.y)),
            GetNode(new Vector2Int(_currentCell.Pos.x - 1, _currentCell.Pos.y))
        };

        foreach (Node currentNode in neighbours)
        {
            CalcFCost(currentNode);
            nodes[currentNode.Pos.x, currentNode.Pos.y].Parent = _currentCell.Pos;
        }
    }

    private void CalcFCost(Node _currentCell)
    {
        if (_currentCell == null) { return; }
        if (_currentCell.FCost != -1) { return; }
        
        int GCost = CalcDistance(_currentCell, agentNode);
        int HCost = CalcDistance(_currentCell, startNode);
        int CellMoveCost = GridManager.Instance.GetCell(_currentCell.Pos).CellMoveCost;

        nodes[_currentCell.Pos.x, _currentCell.Pos.y].FCost = GCost + HCost + CellMoveCost;
        pendingNodes.Add(_currentCell);
    }

    private int CalcDistance(Node _cell1, Node _cell2)
    {
        int xDifference = _cell1.Pos.x - _cell2.Pos.x;
        int yDifference = _cell1.Pos.y - _cell2.Pos.y;
        return xDifference + yDifference;
    }

    private Node GetLowestPending()
    {
        Node lowestNode = pendingNodes[0];

        for (int i = 0; i < pendingNodes.Count; i++)
        {
            if (pendingNodes[i].FCost < lowestNode.FCost)
            {
                lowestNode = pendingNodes[i];
            }
        }

        return lowestNode;
    }

    //Node Grid Functions
    ////////////////////////////////////////////////////////////////

    private void ClearNodes()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                nodes[x, y] = new Node(new Vector2Int(x, y));
            }
        }
    }

    private Node GetNode(Vector2Int _pos)
    {
        if (IsInBounds(_pos))
        {
            return nodes[_pos.x, _pos.y];
        }

        return null;
    }

    private bool IsInBounds(Vector2Int _pos)
    {
        if (_pos.x > 0 && _pos.x < gridSize.x)
        {
            if (_pos.y > 0 && _pos.y < gridSize.y)
            {
                return true;
            }
        }

        return false;
    }
}
