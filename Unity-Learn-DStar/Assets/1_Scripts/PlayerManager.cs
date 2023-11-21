using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject PathPrefab;
    public Vector2Int Pos { get; set; }

    private DStar dStar;

    public void Start()
    {
        dStar = new DStar();
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

        Stack<Vector2Int> path = dStar.CalcPath(Pos, mouseWorldPos);

        foreach (Vector2Int currentNode in path)
        {
            Instantiate(PathPrefab, new Vector3(currentNode.x, -currentNode.y, -2), Quaternion.identity);
            Move(currentNode);
        }
    }
}
