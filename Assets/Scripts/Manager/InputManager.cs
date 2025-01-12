using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    private Unit selectedUnit;
    private List<Vector2Int> validMoves;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ClearSelection();
        }
    }

    private void HandleSelection()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector2Int gridPosition = GridManager.Instance.WorldToGrid(hit.point);
            
            Unit clickedUnit = hit.collider.GetComponent<Unit>();
            
            if (clickedUnit != null)
            {
                if (clickedUnit.belongsToTeam1 == GameManager.Instance.isTeam1Turn)
                {
                    SelectUnit(clickedUnit);
                }
                else if (selectedUnit != null) // Enemy unit clicked with our unit selected
                {
                    AttemptAttack(clickedUnit);
                }
            }
            else if (selectedUnit != null) // Empty square clicked with unit selected
            {
                AttemptMove(gridPosition);
            }
        }
    }

    private void SelectUnit(Unit unit)
    {
        selectedUnit = unit;
        validMoves = GridManager.Instance.GetValidMoves(unit);
        // Highlight valid moves
    }

    private void AttemptMove(Vector2Int targetPos)
    {
        if (validMoves.Contains(targetPos))
        {
            float distance = Vector2Int.Distance(selectedUnit.gridPosition, targetPos);
            selectedUnit.SpendMovement(distance);
            GridManager.Instance.UpdateUnitPosition(selectedUnit, targetPos);
        }
    }

    private void AttemptAttack(Unit target)
    {
        if (Vector2Int.Distance(selectedUnit.gridPosition, target.gridPosition) <= 1.5f)
        {
            int damage = selectedUnit.CalculateAttackDamage();
            bool killed = target.TakeDamage(damage);
            
            if (killed)
            {
                // Handle unit death
            }
        }
    }

    private void ClearSelection()
    {
        selectedUnit = null;
        validMoves = null;
        // Clear highlights
    }

} 