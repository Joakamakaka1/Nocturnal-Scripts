using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCombat : MonoBehaviour
{

    public AudioClip attackSound;

    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaque;
    [SerializeField] private float tiempoSiguienteAtaque;
    [SerializeField] private float probabilidadCritico = 0.1f; 
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }


        if (Input.GetMouseButtonDown(1) && tiempoSiguienteAtaque <= 0)
        {
            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaque;
        }
    }
    private void Golpe()
    {
        animator.SetTrigger("Attack");
        PlayAttackSound();

        Collider2D[] objectos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objectos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                int daño = Random.value < probabilidadCritico ? (int)(dañoGolpe * 2 + PlayerController.Instance.damageMultiplier) : (int)(dañoGolpe + PlayerController.Instance.damageMultiplier);
                colisionador.transform.GetComponent<Enemigo>().TakeDamage(daño);
            }
        }
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

}