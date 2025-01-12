using UnityEngine;

public class Cat : Unit
{
    public bool evasionAvailable = true;

    protected override void Start()
    {
        maxHP = 15;
        minAttack = 5;
        maxAttack = 7;
        maxMovement = 4;

        base.Start();
    }

    public override void PlayAttackAnimation()
    {
        animator.SetTrigger("Cat_Attack");
    }

    public override bool TakeDamage(int damage)
    {
        if (evasionAvailable)
        {
            if (Random.value < 0.5f) // 50% chance to evade
            {
                evasionAvailable = false;
                return false; // Didn't die
            }
        }
        return base.TakeDamage(damage);
    }

    public void ResetEvasion()
    {
        evasionAvailable = true;
    }
}