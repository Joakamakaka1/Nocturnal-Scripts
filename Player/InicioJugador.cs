using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioJugador : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private void Start()
    {
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Instantiate(EleccionManager.Instance.personajes[indexJugador].personajeJugable, transform.position, Quaternion.identity);
    }
}
