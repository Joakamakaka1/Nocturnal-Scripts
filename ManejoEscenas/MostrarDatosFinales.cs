using UnityEngine;
using TMPro;
using System;

public class MostrarDatosFinales : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI puntuacionTMP;
    [SerializeField] private TextMeshProUGUI duracionTMP;
    [SerializeField] private TextMeshProUGUI horaInicioTMP;
    [SerializeField] private TextMeshProUGUI enemigosEliminadosTMP;

    private void Start()
    {
        float puntuacionFinal = PuntuacionManager.Instance.Puntuacion;
        puntuacionTMP.text = puntuacionFinal.ToString("0");

        float duracionPartida = Timer.Instance.ElapsedTime;
        TimeSpan duracionFormato = TimeSpan.FromSeconds(duracionPartida);
        duracionTMP.text = string.Format("{0:D2}:{1:D2}:{2:D2}", duracionFormato.Hours, duracionFormato.Minutes, duracionFormato.Seconds);

        string horaInicio = InicioJuego.FechaHoraInicio.ToString();
        horaInicioTMP.text = horaInicio;

        enemigosEliminadosTMP.text = PasarNivel.ObtenerTotalEnemigosEliminados().ToString();
    }
}