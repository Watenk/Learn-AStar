using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    public Vector2Int GridSize;
    [Header("GridGeneration")]
    public int PerlinMagnification;
    public List<Cells> GenerationCells = new List<Cells>();

    [Header("GridCells")]
    public List<CellPrefab> CellPrefabs = new List<CellPrefab>();

    public GameObject PlayerPrefab;

    private Cell[,] grid;
    private int perlinXOffset;
    private int perlinYOffset;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        grid = new Cell[GridSize.x, GridSize.y];
        perlinXOffset = UnityEngine.Random.Range(-100000, 100000);
        perlinYOffset = UnityEngine.Random.Range(-100000, 100000);

        GenerateGrid();
    }

    public void AddCell(Cells _cellEnum, Vector2Int _pos)
    {
        CellPrefab cellPrefab = CellEnumToCellPrefab(_cellEnum);
        GameObject cellGameObject = Instantiate(cellPrefab.prefab, new Vector3(_pos.x, -_pos.y, 0), Quaternion.identity);
        cellGameObject.transform.SetParent(transform);
        grid[_pos.x, _pos.y] = new Cell(_pos, cellGameObject, cellPrefab.MoveCost);
    }

    public Cell GetCell(Vector2Int _pos)
    {
        if (IsInGrid(_pos))
        {
            return grid[_pos.x, _pos.y];
        }
        else
        {
            Debug.LogError("GetCell Out of Bound: " + _pos);
            return null;
        }
    }

    private void GenerateGrid()
    {
        for (int y = 0; y < GridSize.y; y++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                Vector2Int currentPos = new Vector2Int(x, y);
                AddCell(GetPerlinCell(currentPos), currentPos);
            }
        }

        SpawnPlayer(new Vector2Int(10, 10));
    }

    private void SpawnPlayer(Vector2Int _pos)
    {
        Cell spawnCell = GetMovableCellAround(_pos);
        PlayerManager player = Instantiate(PlayerPrefab, new Vector3(spawnCell.Pos.x, -spawnCell.Pos.y, -2), Quaternion.identity).GetComponent<PlayerManager>();
        player.Pos = spawnCell.Pos;
    }
    
    private Cells GetPerlinCell(Vector2Int _pos)
    {
        float offsetPerlin = Mathf.PerlinNoise(((float)_pos.x + perlinXOffset) / PerlinMagnification, ((float)_pos.y + perlinYOffset) / PerlinMagnification); 
        float cellIndex = Mathf.Clamp(offsetPerlin, 0.0f, 0.9999999f) * GenerationCells.Count;
        int cellIndexInt = Mathf.FloorToInt(cellIndex);
        return GenerationCells[cellIndexInt];
    }

    private Cell GetMovableCellAround(Vector2Int _pos)
    {
        int searchRadius = 10;
        for (int y = _pos.y - searchRadius; y < _pos.y + searchRadius; y++)
        {
            for (int x = _pos.x - searchRadius; x < _pos.y + searchRadius; x++)
            {
                Vector2Int currentPos = new Vector2Int(x, y);
                if (IsInGrid(currentPos))
                {
                    Cell currentCell = GetCell(currentPos);
                    if (currentCell.CellMoveCost != 0)
                    {
                        return currentCell;
                    }
                }
            }
        }

        Debug.LogError("GetMovableCellAround " + _pos + " Failed");
        return null;
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

    private bool IsInGrid(Vector2Int _pos)
    {
        if (_pos.x > 0 && _pos.x < GridSize.x)
        {
            if (_pos.y > 0 && _pos.y < GridSize.y)
            {
                return true;
            }
        }
        return false;
    }
}
