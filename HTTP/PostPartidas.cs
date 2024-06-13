using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

[System.Serializable]
public class PartidasDTO
{
    public DateTime fechaHoraInicio;
    public string duracion;
    public string resultado;
    public string nombrePj;
    public string enemigosEliminados;
    public int clienteID;
}

public class PostPartidas : MonoBehaviour
{
    public TextMeshProUGUI puntuacionTMP;
    public TextMeshProUGUI duracionTMP;
    public TextMeshProUGUI horaInicioTMP;
    public TextMeshProUGUI enemigosEliminadosTMP;

    private string URL = "http://localhost:7500/api/v1/partidas";

    public void CrearPartida()
    {
        PartidasDTO partidaDTO = new PartidasDTO();

        partidaDTO.fechaHoraInicio = DateTime.Parse(horaInicioTMP.text);
        partidaDTO.duracion = duracionTMP.text;
        partidaDTO.resultado = puntuacionTMP.text;
        partidaDTO.nombrePj = PlayerPrefs.GetString("NombreJugador");
        partidaDTO.enemigosEliminados = enemigosEliminadosTMP.text;
        partidaDTO.clienteID = GetCliente.ClienteID;

        // Convertir el objeto PartidaDTO a JSON usando Newtonsoft.Json
        string jsonBody = JsonConvert.SerializeObject(partidaDTO);

        // Iniciar la corutina para enviar la solicitud POST
        StartCoroutine(EnviarSolicitudPOST(jsonBody));
    }

    IEnumerator EnviarSolicitudPOST(string jsonBody)
    {
        using (UnityWebRequest request = new UnityWebRequest(URL, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Partida creada con exito.");
                Debug.Log("Respuesta del servidor: " + request.downloadHandler.text);

                // Destruir los objetos DontDestroyOnLoad
                DontDestroyOnLoadManager.ClearDontDestroyOnLoadObjects();

                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}