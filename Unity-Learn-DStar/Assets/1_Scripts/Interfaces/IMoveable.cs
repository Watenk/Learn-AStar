using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    Vector2Int Pos { get; }

    void Move(Vector2Int _direction);
}
