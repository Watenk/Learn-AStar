using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance { get; private set; }
    public GameObject OpenNodePrefab;
    public GameObject ClosedNodePrefab;
    public GameObject PathPrefab;
    public GameObject CurrentNodePrefab;
    public GameObject ScoreTextPrefab;

    private List<GameObject> debugGameObjects = new List<GameObject>();

    public void Awake()
    {
        Instance = this;
    }

    public void DrawNodeGrid(Node[,] _nodes, List<Node> _openNodes, List<Node> _closedNodes, Node _currentNode, bool _drawText)
    {
        RemoveDebugObjects();

        AddDebugObject(CurrentNodePrefab, _currentNode.Pos, -3);

        for (int y = 0; y < _nodes.GetLength(1); y++)
        {
            for (int x = 0; x < _nodes.GetLength(0); x++)
            {
                Node currentNode = _nodes[x, y];
                if (currentNode.FCost != -1)
                {
                    if (_openNodes.Contains(currentNode))
                    {
                        AddDebugObject(OpenNodePrefab, new Vector2Int(x, y), -2);
                    }

                    if (_closedNodes.Contains(currentNode))
                    {
                        AddDebugObject(ClosedNodePrefab, new Vector2Int(x, y), -2);
                    }

                    if (_drawText)
                    {
                        GameObject scoreText = AddDebugObject(ScoreTextPrefab, new Vector2Int(x, y), -3);
                        Text FCost = scoreText.transform.Find("FCost").GetComponent<Text>();
                        Text HCost = scoreText.transform.Find("HCost").GetComponent<Text>();
                        Text GCost = scoreText.transform.Find("GCost").GetComponent<Text>();

                        FCost.text = currentNode.FCost.ToString();
                        HCost.text = currentNode.HCost.ToString();
                        GCost.text = currentNode.GCost.ToString();
                    }
                }
            }
        }
    }

    ////////////////////////////////////////////////////////

    private GameObject AddDebugObject(GameObject _prefab, Vector2Int _pos, int _depth)
    {
        GameObject newObject = Instantiate(_prefab, new Vector3(_pos.x, -_pos.y, _depth), Quaternion.identity);
        newObject.transform.SetParent(this.transform);
        debugGameObjects.Add(newObject);
        return newObject;
    }

    private void RemoveDebugObjects()
    {
        foreach (GameObject currentObject in debugGameObjects)
        {
            Destroy(currentObject);
        }
    }
}