using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeAtaque : MonoBehaviour, IEnemy
{
    [SerializeField] private int damageAmount = 3; 

    private PlayerController player;
    private PlayerVida playerVida;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        playerVida = GetComponent<PlayerVida>();
    }

    public void Attack()
    {
        if (player != null && playerVida != null)
        {
            playerVida.TakeDamage(damageAmount, transform);
        }
    }
}
