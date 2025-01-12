using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Unit Info")]
    public GameObject unitInfoPanel;
    public TextMeshProUGUI unitNameText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI movementText;
    public TextMeshProUGUI specialAbilityText;

    [Header("Turn Info")]
    public TextMeshProUGUI turnText;
    public GameObject victoryPanel;
    public TextMeshProUGUI victoryText;

    public void UpdateUnitInfo(Unit unit)
    {
        if (unit == null)
        {
            unitInfoPanel.SetActive(false);
            return;
        }

        unitInfoPanel.SetActive(true);
        unitNameText.text = unit.GetType().Name;
        healthText.text = $"HP: {unit.currentHP}/{unit.maxHP}";
        attackText.text = $"Attack: {unit.minAttack}-{unit.maxAttack}";
        movementText.text = $"Movement: {unit.currentMovement}/{unit.maxMovement}";

        string specialAbility = unit is Cat ? 
            "Evasion: " + (((Cat)unit).evasionAvailable ? "Ready" : "Used") :
            "Pack Bonus: " + (unit as Dog)?.CalculateAttackDamage();
        
        specialAbilityText.text = specialAbility;
    }

    public void UpdateTurnInfo()
    {
        turnText.text = $"Current Turn: Team {(GameManager.Instance.isTeam1Turn ? "1" : "2")}";
    }

    public void ShowVictoryScreen(string message)
    {
        victoryPanel.SetActive(true);
        victoryText.text = message;
    }
} 