using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject PathPrefab;
    public Vector2Int Pos { get; set; }

    private ADStar adStar;
    private List<Node> path;
    private int currentPathIndex;
    private List<GameObject> pathDebugObjects = new List<GameObject>();

    public void Start()
    {
        adStar = new ADStar();
        InputManager.Instance.OnSpacebar += OnSpacebar;
        InputManager.Instance.OnLeftMouseButtonDown += OnLeftMouseButtonDown;
    }

    public void Move(Vector2Int _pos)
    {
        Pos = _pos;
        transform.position = new Vector3(_pos.x, -_pos.y, -2);
    }

    private void OnLeftMouseButtonDown()
    {
        Vector3 mouseWorldPosVector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int mouseWorldPos = new Vector2Int((int)mouseWorldPosVector3.x, (int)-mouseWorldPosVector3.y);

        path = null;
        currentPathIndex = 0;
        foreach (GameObject loopObject in pathDebugObjects)
        {
            Destroy(loopObject);
        }
        adStar.SetTarget(Pos, mouseWorldPos);
    }

    private void OnSpacebar()
    {

        if (path == null)
        {
            path = adStar.CalcPath();
        }
        else if (currentPathIndex != path.Count)
        {
            Node currentNode = path[currentPathIndex];
            Move(currentNode.Pos);
            pathDebugObjects.Add(Instantiate(PathPrefab, new Vector3(currentNode.Pos.x, -currentNode.Pos.y, -5), Quaternion.identity));
            currentPathIndex++;
        }
    }
}
