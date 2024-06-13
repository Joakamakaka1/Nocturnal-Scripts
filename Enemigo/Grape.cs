using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;
    [SerializeField] private GameObject bigGrapeProjectilePrefab;
    [SerializeField] private int normalProjectileCountMin = 3;
    [SerializeField] private int normalProjectileCountMax = 6;
    [SerializeField] private int bigProjectileFrequency = 4;
    [SerializeField] private float bigProjectileSizeIncrease = 1.5f;

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    private int attackCount = 0;
    private bool isHalfHealth = false;

    private static readonly int AttackHash = Animator.StringToHash("Attack");

    public AudioClip attackSound;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(AttackHash);
        PlayAttackSound();
        spriteRenderer.flipX = (transform.position.x - PlayerController.Instance.transform.position.x) < 0;

        attackCount++;

        if (!isHalfHealth && GetComponent<Enemigo>().currentHealth <= GetComponent<Enemigo>().startingHealth / 2)
        {
            isHalfHealth = true;
        }

        if (attackCount % bigProjectileFrequency == 0 && attackCount > 0 && isHalfHealth)
        {
            SpawnBigProjectile();
        }
        else
        {
            SpawnNormalProjectiles();
        }
    }

    private void SpawnNormalProjectiles()
    {
        int projectileCount = Random.Range(normalProjectileCountMin, normalProjectileCountMax + 1);
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject projectile = Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
            GrapeProyectile grapeProjectile = projectile.GetComponent<GrapeProyectile>();
            grapeProjectile.SetDamage(1); 
        }
    }

    private void SpawnBigProjectile()
    {
        GameObject bigProjectile = Instantiate(bigGrapeProjectilePrefab, transform.position, Quaternion.identity);
        bigProjectile.transform.localScale *= bigProjectileSizeIncrease;
        GrapeProyectile grapeProjectile = bigProjectile.GetComponent<GrapeProyectile>();
        grapeProjectile.SetDamage(2); 
    }

    public void SpawnProjectile()
    {
        Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
    }
}