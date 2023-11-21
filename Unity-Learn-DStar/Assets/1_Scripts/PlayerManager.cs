using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IMoveable
{
    public Vector2Int Pos { get; private set; }

    //private DStar dStar = new DStar();

    public void Move(Vector2Int _direction)
    {
        Pos += _direction;
        transform.position = new Vector3(Pos.x, -Pos.y, 0);
    }
}
