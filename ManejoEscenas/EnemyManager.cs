using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public TextMeshProUGUI enemigosText; // Texto para mostrar la cantidad de enemigos eliminados / total
    [SerializeField] private int cantidadEnemigos;
    [SerializeField] private int enemigosEliminados;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cantidadEnemigos = GameObject.FindGameObjectsWithTag("Enemigo").Length;
        ActualizarTextoEnemigos();
    }

    public void EnemigoEliminado()
    {
        enemigosEliminados++;
        ActualizarTextoEnemigos();
    }

    private void ActualizarTextoEnemigos()
    {
        enemigosText.text = $"{enemigosEliminados}/{cantidadEnemigos}";
    }
}
