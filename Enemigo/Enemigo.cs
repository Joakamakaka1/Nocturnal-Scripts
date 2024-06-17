using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] public int startingHealth = 10;
    [SerializeField] private float knockBackThrust = 2f;

    [SerializeField] private float cantidadPuntos = 100f;
    [SerializeField] private Puntuaje puntuaje;

    public int currentHealth;
    private KnockBack knockback;
    private Flash flash;

    public AudioClip attackSound;
    public GameObject popUpDamage;
    public TMP_Text popUpText;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<KnockBack>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;     
        ShowFloatingText(damage);
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void ShowFloatingText(int damage)
    {
        Instantiate(popUpDamage, transform.position, Quaternion.identity, transform);
        popUpText.text = damage.ToString();
        
    }
    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {           
            PlayAttackSound();
            GameObject.FindGameObjectWithTag("Bandera").GetComponent<PasarNivel>().EnemigoEliminado();
            GetComponent<PickUpSpawner>().DropItems();
            Destroy(gameObject);
            PuntuacionManager.Instance.SumarPuntos(cantidadPuntos);
            EnemyManager.instance.EnemigoEliminado();
            puntuaje.UpdateScoreText();
        }
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
    }
}