using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineFollow : MonoBehaviour
{
    private GameObject player;
    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (player != null && virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
        }
        else
        {
            Debug.LogError("No se encontró el objeto con el tag 'Player' o la virtual camera.");
        }
    }
}
