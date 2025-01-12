using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    
    public int width = 9;
    public int height = 5;
    public float cellSize = 1f;
    
    private Unit[,] grid;
    
    private void Awake()
    {
        Instance = this;
        grid = new Unit[width, height];
    }

    public bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    public bool IsOccupied(Vector2Int pos)
    {
        return grid[pos.x, pos.y] != null;
    }

    public List<Vector2Int> GetValidMoves(Unit unit)
    {
        List<Vector2Int> validMoves = new List<Vector2Int>();
        int moveRange = Mathf.FloorToInt(unit.currentMovement);

        for (int x = -moveRange; x <= moveRange; x++)
        {
            for (int y = -moveRange; y <= moveRange; y++)
            {
                Vector2Int newPos = unit.gridPosition + new Vector2Int(x, y);
                
                if (!IsValidPosition(newPos) || IsOccupied(newPos))
                    continue;

                float distance = Mathf.Sqrt(x * x + y * y);
                if (distance <= unit.currentMovement)
                {
                    validMoves.Add(newPos);
                }
            }
        }

        return validMoves;
    }

    public void UpdateUnitPosition(Unit unit, Vector2Int newPos)
    {
        if (unit.gridPosition != Vector2Int.zero)
            grid[unit.gridPosition.x, unit.gridPosition.y] = null;
            
        grid[newPos.x, newPos.y] = unit;
        unit.gridPosition = newPos;
        unit.transform.position = GridToWorld(newPos);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * cellSize, 0, gridPos.y * cellSize);
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPos.x / cellSize),
            Mathf.RoundToInt(worldPos.z / cellSize)
        );
    }
} 