using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Vector2Int Pos { get; set; }

    private ADStar adStar;

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

        adStar.SetTarget(Pos, mouseWorldPos);
    }

    private void OnSpacebar()
    {
        List<Node> path = adStar.CalcPath();

        if (path != null)
        {
            foreach (Node currentNode in path)
            {
                Move(currentNode.Pos);
            }
        }
    }
}
