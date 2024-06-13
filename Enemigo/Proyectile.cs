using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;

    private float damageMultiplier = 0f;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;

    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();

        if (other.CompareTag("Player") && isEnemyProjectile)
        {
            PlayerVida player = other.GetComponent<PlayerVida>();
            if (player != null)
            {
                player.TakeDamage(1, transform);
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Enemigo") && !isEnemyProjectile)
        {
            Enemigo enemy = other.GetComponent<Enemigo>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)(10 + damageMultiplier));
                Destroy(gameObject);
            }
        }
        else if ((!other.isTrigger && indestructible))
        {
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

}

