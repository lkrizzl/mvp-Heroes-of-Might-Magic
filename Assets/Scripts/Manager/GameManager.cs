using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Teams")]
    public List<Unit> team1Units = new List<Unit>();
    public List<Unit> team2Units = new List<Unit>();
    public bool isTeam1Turn = true;

    [Header("References")]
    public UIManager uiManager;
    public GridManager gridManager;

    private Unit selectedUnit;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DetermineFirstTurn();
    }

    private void DetermineFirstTurn()
    {
        // Team with less total HP starts
        int team1HP = CalculateTeamHP(team1Units);
        int team2HP = CalculateTeamHP(team2Units);
        isTeam1Turn = team1HP <= team2HP;
        
        ResetTeamMovement(isTeam1Turn ? team1Units : team2Units);
    }

    private int CalculateTeamHP(List<Unit> team)
    {
        int totalHP = 0;
        foreach (Unit unit in team)
        {
            totalHP += unit.currentHP;
        }
        return totalHP;
    }

    public void EndTurn()
    {
        isTeam1Turn = !isTeam1Turn;
        List<Unit> activeTeam = isTeam1Turn ? team1Units : team2Units;
        
        ResetTeamMovement(activeTeam);
        ResetTeamAbilities(activeTeam);
        CheckWinCondition();
    }

    private void ResetTeamMovement(List<Unit> team)
    {
        foreach (Unit unit in team)
        {
            if (unit.currentHP > 0)
                unit.ResetMovement();
        }
    }

    private void ResetTeamAbilities(List<Unit> team)
    {
        foreach (Unit unit in team)
        {
            if (unit is Cat cat)
                cat.ResetEvasion();
        }
    }

    private void CheckWinCondition()
    {
        bool team1Alive = team1Units.Exists(u => u.currentHP > 0);
        bool team2Alive = team2Units.Exists(u => u.currentHP > 0);

        if (!team1Alive) uiManager.ShowVictoryScreen("Team 2 Wins!");
        if (!team2Alive) uiManager.ShowVictoryScreen("Team 1 Wins!");
    }
} 