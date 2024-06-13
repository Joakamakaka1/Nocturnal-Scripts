using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour, IEnemy
{
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(AttackHash);
    }
}
