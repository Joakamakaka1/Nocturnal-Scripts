using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    public AudioClip attackSound;
    public float projectileRange = 10f;
    public float tiempoDeEspera = 0.5f;
    private Animator animator;
    private float tiempoTranscurridoDesdeUltimoDisparo;

    private void Start()
    {
        animator = GetComponent<Animator>();
        tiempoTranscurridoDesdeUltimoDisparo = tiempoDeEspera;
    }

    private void Update()
    {
        tiempoTranscurridoDesdeUltimoDisparo += Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && tiempoTranscurridoDesdeUltimoDisparo >= tiempoDeEspera)
        {
            Atacar();
            tiempoTranscurridoDesdeUltimoDisparo = 0f;
        }
    }

    private void Atacar()
    {
        animator.SetTrigger("Attack");
        PlayAttackSound();

        Vector2 targetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<Proyectile>().UpdateProjectileRange(projectileRange);

        float damageMultiplier = PlayerController.Instance.damageMultiplier;

        newBullet.GetComponent<Proyectile>().SetDamageMultiplier(damageMultiplier);

        newBullet.transform.right = targetDirection.normalized;
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
    }
}