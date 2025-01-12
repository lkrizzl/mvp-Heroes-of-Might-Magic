using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Base Stats")]
    public int maxHP;
    public int currentHP;
    public int minAttack;
    public int maxAttack;
    public int maxMovement;
    public float currentMovement;

    [Header("Components")]
    protected Animator animator;

    [Header("Position")]
    public Vector2Int gridPosition;
    public bool belongsToTeam1;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        currentHP = maxHP;
        currentMovement = maxMovement;
    }

    public virtual void PlayIdleAnimation()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

    public virtual void PlayMoveAnimation(bool isRunning)
    {
        animator.SetBool("isWalking", !isRunning);
        animator.SetBool("isRunning", isRunning);
    }

    public virtual void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public virtual void PlayDeathAnimation()
    {
        animator.SetTrigger("Death");
    }

    public virtual int CalculateAttackDamage()
    {
        return Random.Range(minAttack, maxAttack + 1);
    }

    public virtual bool TakeDamage(int damage)
    {
        currentHP -= damage;
        return currentHP <= 0;
    }

    public virtual void ResetMovement()
    {
        currentMovement = maxMovement;
    }

    public virtual bool CanMove(float cost)
    {
        return currentMovement >= cost;
    }

    public virtual void SpendMovement(float cost)
    {
        currentMovement = Mathf.Max(0, currentMovement - cost);
    }
}