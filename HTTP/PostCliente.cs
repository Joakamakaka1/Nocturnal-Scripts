using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ClienteDTO
{
    public string usuario;
    public string password;
    public string gmail;
}

public class PostCliente : MonoBehaviour
{
    public TMP_InputField UsuInput;
    public TMP_InputField PassInput;
    public TMP_InputField GmailInput;

    private string URL = "http://localhost:7500/api/v1/cliente";

    public void CrearCliente()
    {
        if (!ValidarCampos())
        {
            return;
        }

        ClienteDTO clienteDTO = new ClienteDTO();
        clienteDTO.usuario = UsuInput.text;
        clienteDTO.password = PassInput.text;
        clienteDTO.gmail = GmailInput.text;

        string jsonBody = JsonUtility.ToJson(clienteDTO);

        StartCoroutine(EnviarSolicitudPOST(jsonBody));
    }

    private bool ValidarCampos()
    {
        bool camposValidos = true;

        // Validar usuario
        if (string.IsNullOrEmpty(UsuInput.text) || UsuInput.text.Length < 3 || UsuInput.text.Length > 30)
        {
            UsuInput.text = "";
            UsuInput.placeholder.GetComponent<TextMeshProUGUI>().color = Color.red;
            camposValidos = false;
        }
        else
        {
            UsuInput.placeholder.GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        // Validar contraseña
        if (string.IsNullOrEmpty(PassInput.text) || PassInput.text.Length < 6 || PassInput.text.Length > 10)
        {
            PassInput.text = "";
            PassInput.placeholder.GetComponent<TextMeshProUGUI>().color = Color.red;
            camposValidos = false;
        }
        else
        {
            PassInput.placeholder.GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        // Validar correo
        if (string.IsNullOrEmpty(GmailInput.text) || GmailInput.text.Length < 8 || GmailInput.text.Length > 40)
        {
            GmailInput.text = "";
            GmailInput.placeholder.GetComponent<TextMeshProUGUI>().color = Color.red;
            camposValidos = false;
        }
        else
        {
            GmailInput.placeholder.GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        return camposValidos;
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
                Debug.Log("Cliente creado con éxito.");
                Debug.Log("Respuesta del servidor: " + request.downloadHandler.text);
                SceneManager.LoadScene("Login");
            }
        }
    }

    public void irLogin()
    {
        SceneManager.LoadScene("Login");
    }
}


