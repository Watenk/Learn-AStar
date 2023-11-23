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

    private Vector2Int gridSize;

    public ADStar()
    {
        gridSize = GridManager.Instance.GridSize;
        nodes = new Node[gridSize.x, gridSize.y];
    }

    public void SetTarget(Vector2Int _agentNode, Vector2Int _targetNode)
    {
        ClearNodes();
        openNodes.Clear();
        closedNodes.Clear();

        agentNode = GetNode(_agentNode);
        targetNode = GetNode(_targetNode);
        openNodes.Add(targetNode);
    }

    //Returns null until path is found
    public List<Node> CalcPath()
    {
        Node currentNode = GetLowestOpenNode();

        if (currentNode != agentNode)
        {
            CalcNeightbours(currentNode);
        }
        else
        {
            return RetracePath(agentNode, targetNode);
        }

        DebugManager.Instance.DrawNodeGrid(nodes, openNodes, closedNodes, currentNode, false);

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
        List<Node> neighbours = new List<Node>
        {
            GetNode(new Vector2Int(_currentNode.Pos.x, _currentNode.Pos.y - 1)),
            GetNode(new Vector2Int(_currentNode.Pos.x, _currentNode.Pos.y + 1)),
            GetNode(new Vector2Int(_currentNode.Pos.x + 1, _currentNode.Pos.y)),
            GetNode(new Vector2Int(_currentNode.Pos.x - 1, _currentNode.Pos.y))
        };

        foreach (Node loopNode in neighbours)
        {
            CalcNodeCosts(loopNode, _currentNode.Pos);
        }
    }

    private void CalcNodeCosts(Node _currentNode, Vector2Int _parent)
    {
        //Conditions for execution
        if (_currentNode == null) { return; }
        if (closedNodes.Contains(_currentNode)) { return; }
        int CellMoveCost = GridManager.Instance.GetCell(_currentNode.Pos).CellMoveCost;
        if (CellMoveCost < 0) { return; }

        int GCost = CalcGCost(_parent);
        int HCost = CalcHCost(_currentNode);
        int FCost = GCost + HCost + CellMoveCost;

        _currentNode.SetNode(_parent, FCost, GCost, HCost);

        if (!openNodes.Contains(_currentNode))
        {
            openNodes.Add(_currentNode);
        }
    }

    //Distance from starting node
    private int CalcGCost(Vector2Int _parent)
    {
        if (_parent == new Vector2Int(-1, -1)) { return 0; }
        int newGCost = GetNode(_parent).GCost + 1;
        return newGCost;
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
        Node lowestNode = openNodes.OrderBy(node => node.FCost).ThenBy(node => node.HCost).FirstOrDefault();
        openNodes.Remove(lowestNode);
        closedNodes.Add(lowestNode);
        return lowestNode;
        //List<Node> lowestOpenNodes = new List<Node>
        //{
        //    lowestNode
        //};

        ////Check for multiple nodes that have the same value
        //foreach (Node loopNode in lowestOpenNodes)
        //{
        //    if (loopNode != lowestNode)
        //    {
        //        if (loopNode.GCost == lowestNode.GCost)
        //        {
        //            lowestOpenNodes.Add(loopNode);
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //}

        //if (lowestOpenNodes.Count > 1)
        //{
        //    int randomIndex = Random.Range(0, lowestOpenNodes.Count);
        //    return lowestOpenNodes[randomIndex];
        //}

        //return lowestOpenNodes[0];
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
        if (_pos.x >= 0 && _pos.x < gridSize.x)
        {
            if (_pos.y >= 0 && _pos.y < gridSize.y)
            {
                return true;
            }
        }

        return false;
    }
}
