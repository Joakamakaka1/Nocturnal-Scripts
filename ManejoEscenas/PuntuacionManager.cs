using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntuacionManager : Singleton<PuntuacionManager>
{
    public float puntuacion;

    public float Puntuacion { get { return puntuacion; } }

    public void SumarPuntos(float puntosEntrada)
    {
        puntuacion += puntosEntrada;
    }
}
