using UnityEngine;

public class BossController : MonoBehaviour, IEnemy
{
    private Animator animator;
    private static readonly int AttackHash = Animator.StringToHash("Attack"); // Trigger
    private static readonly int DeadHash = Animator.StringToHash("Dead"); // Bool
    private static readonly int RunHash = Animator.StringToHash("Run"); // Bool
    private static readonly int IsAttackingHash = Animator.StringToHash("isAttacking"); // Bool

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger(AttackHash);
    }

    public void Die()
    {
        animator.SetBool(DeadHash, true);
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool(RunHash, isRunning);
    }

    public void SetIdle()
    {
        animator.SetBool(RunHash, false);
    }

    public void SetAttacking(bool isAttacking)
    {
        animator.SetBool(IsAttackingHash, isAttacking);
    }
}