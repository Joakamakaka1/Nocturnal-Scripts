using UnityEngine;
using Cinemachine;

public class SeguirPersonaje : MonoBehaviour
{
    public Transform jugador;

    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (jugador == null)
        {
            Debug.LogError("¡No se ha asignado el jugador en el script de SeguirPersonaje!");
            return;
        }

        virtualCamera.Follow = jugador;
    }
}