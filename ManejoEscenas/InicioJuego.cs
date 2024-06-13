using UnityEngine;
using System;

public class InicioJuego : MonoBehaviour
{   
    public static DateTime FechaHoraInicio { get; set; }

    public void Start()
    {
        FechaHoraInicio = DateTime.Now;
    }
}
