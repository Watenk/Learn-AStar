using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int GridSize;
    [Header("GridGeneration")]
    public int PerlinMagnification;
    public List<Cells> GenerationCells = new List<Cells>();

    [Header("GridCells")]
    public List<CellPrefab> CellPrefabs = new List<CellPrefab>();

    private Cell[,] grid;
    private int perlinXOffset;
    private int perlinYOffset;


    public void Start()
    {
        grid = new Cell[GridSize.x, GridSize.y];
        perlinXOffset = UnityEngine.Random.Range(-100000, 100000);
        perlinYOffset = UnityEngine.Random.Range(-100000, 100000);

        GenerateGrid();
    }

    public void AddTile(Cells _cellEnum, Vector2Int _pos)
    {
        CellPrefab cellPrefab = CellEnumToCellPrefab(_cellEnum);
        GameObject cellGameObject = Instantiate(cellPrefab.prefab, new Vector3(_pos.x, -_pos.y, 0), Quaternion.identity);
        cellGameObject.transform.SetParent(transform);
        grid[_pos.x, _pos.y] = new Cell(_pos, cellGameObject, cellPrefab.MoveCost);
    }

    private void GenerateGrid()
    {
        for (int y = 0; y < GridSize.y; y++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                Vector2Int currentPos = new Vector2Int(x, y);
                AddTile(GetPerlinCell(currentPos), currentPos);
            }
        }
    }

    private Cells GetPerlinCell(Vector2Int _pos)
    {
        float offsetPerlin = Mathf.PerlinNoise(((float)_pos.x + perlinXOffset) / PerlinMagnification, ((float)_pos.y + perlinYOffset) / PerlinMagnification); 
        float cellIndex = Mathf.Clamp(offsetPerlin, 0.0f, 0.9999999f) * GenerationCells.Count;
        int cellIndexInt = Mathf.FloorToInt(cellIndex);
        return GenerationCells[cellIndexInt];
    }

    private CellPrefab CellEnumToCellPrefab(Cells _cellEnum)
    {
        foreach (CellPrefab currentCellPrefab in CellPrefabs)
        {
            if (currentCellPrefab.cellID == _cellEnum)
            {
                return currentCellPrefab;
            }
        }

        Debug.LogError("CellEnumToCellPrefab search Failed. Couldn't find " + _cellEnum + " In CellPrefabs");
        return new CellPrefab();
    }
}
