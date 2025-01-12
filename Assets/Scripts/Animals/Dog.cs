using UnityEngine;
using System.Linq;

public class Dog : Unit
{
    protected override void Start()
    {
        maxHP = 25;
        minAttack = 8;
        maxAttack = 11;
        maxMovement = 3;
        
        base.Start();
    }

    public override int CalculateAttackDamage()
    {
        int baseDamage = base.CalculateAttackDamage();
        
        // Check if there's at least one other living dog on the same team
        bool hasAllyDog = Object.FindObjectsByType<Dog>(FindObjectsSortMode.None)
            .Where(dog => dog != this && dog.belongsToTeam1 == this.belongsToTeam1 && dog.currentHP > 0)
            .Any();

        if (hasAllyDog)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f); // 25% bonus
        }

        return baseDamage;
    }

    public override void PlayAttackAnimation()
    {
        animator.SetTrigger("Dog_GoldenRetriever_Bark"); // Using bark as attack
    }
} 