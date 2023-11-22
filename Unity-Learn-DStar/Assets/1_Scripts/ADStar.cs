using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Timeline.AnimationPlayableAsset;

//AStar with some DStar elements
public class ADStar
{
    private List<Node> openNodes = new List<Node>();
    private List<Node> closedNodes = new List<Node>();
    private Node[,] nodes;

    private Node agentNode;
    private Node targetNode;
    private Node currentNode;
    private Node previousNode;

    private Vector2Int gridSize;

    public ADStar()
    {
        gridSize = GridManager.Instance.GridSize;
        nodes = new Node[gridSize.x, gridSize.y];
    }

    public void SetTarget(Vector2Int _agentNode, Vector2Int _targetNode)
    {
        ClearNodes();

        agentNode = GetNode(_agentNode);
        targetNode = GetNode(_targetNode);
        currentNode = targetNode;
        previousNode = currentNode;
    }

    //Returns null until path is found
    public List<Node> CalcPath()
    {
        if (currentNode != agentNode)
        {
            CalcNeightbours(currentNode);
            previousNode = currentNode;
            currentNode = GetLowestOpenNode();
        }
        else
        {
            return RetracePath(agentNode, targetNode);
        }

        DebugManager.Instance.DrawNodeGrid(nodes, openNodes, closedNodes, currentNode);

        return null;
    }

    ////////////////////////////////////////////////////////////////

    private List<Node> RetracePath(Node _startNode, Node _endNode)
    {
        List<Node> path = new List<Node>();
        Node _currentNode = _startNode;

        while (_currentNode != _endNode)
        {
            path.Add(_currentNode);
            _currentNode = GetNode(_currentNode.Parent);
        }

        return path;
    }

    private void CalcNeightbours(Node _currentNode)
    {
        openNodes.Remove(_currentNode);
        closedNodes.Add(_currentNode);

        List<Node> neighbours = new List<Node>
        {
            GetNode(new Vector2Int(_currentNode.Pos.x, _currentNode.Pos.y - 1)),
            GetNode(new Vector2Int(_currentNode.Pos.x, _currentNode.Pos.y + 1)),
            GetNode(new Vector2Int(_currentNode.Pos.x + 1, _currentNode.Pos.y)),
            GetNode(new Vector2Int(_currentNode.Pos.x - 1, _currentNode.Pos.y))
        };

        foreach (Node loopNode in neighbours)
        {
            if (loopNode != null && !closedNodes.Contains(loopNode))
            {
                CalcNodeCosts(loopNode, _currentNode.Pos);
            }
        }
    }

    private void CalcNodeCosts(Node _currentNode, Vector2Int _parent)
    {
        int CellMoveCost = GridManager.Instance.GetCell(_currentNode.Pos).CellMoveCost;
        if (CellMoveCost < 0) { return; }

        int GCost = CalcGCost(previousNode);
        int HCost = CalcHCost(_currentNode);
        int FCost = GCost + HCost + CellMoveCost;

        _currentNode.SetNode(_parent, FCost, GCost, HCost);
        openNodes.Add(_currentNode);
    }

    //Distance from starting node
    private int CalcGCost(Node _currentNode)
    {
        return RetracePath(_currentNode, targetNode).Count;
    }

    //Distance from end node (heuristic)
    private int CalcHCost(Node _currentNode)
    {
        int xDifference = Mathf.Abs(_currentNode.Pos.x - agentNode.Pos.x);
        int yDifference = Mathf.Abs(_currentNode.Pos.y - agentNode.Pos.y);
        return xDifference + yDifference;
    }

    private Node GetLowestOpenNode()
    {
        return openNodes.OrderBy(node => node.FCost).ThenBy(node => node.HCost).FirstOrDefault();
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
