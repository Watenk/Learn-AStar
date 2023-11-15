using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cells
{
    DirtFloor = 0,
    StoneFloor = 1,
    PathFloor = 2,
    WaterFloor = 3,
    StoneWall = 4,
    WaterWall = 5,
}

[Serializable]
public struct CellPrefab
{
    public Cells cellID;
    public GameObject prefab;
    public int MoveCost;
}
