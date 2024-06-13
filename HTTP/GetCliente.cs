using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GetCliente : MonoBehaviour
{
    public TMP_InputField UsuInput;
    public TMP_InputField PassInput;
    private string URL = "http://localhost:7500/api/v1/clientes";
    public static int ClienteID;

    [System.Serializable]
    public class ClienteResponse
    {
        public string mensaje;
        public List<Cliente> objeto;
    }

    [System.Serializable]
    public class Cliente
    {
        public int id;
        public string gmail;
        public string usuario;
        public string password;
    }

    IEnumerator GetClientes()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                ClienteResponse clienteResponse = JsonUtility.FromJson<ClienteResponse>(request.downloadHandler.text);
                List<Cliente> clientes = clienteResponse.objeto;
                bool loginSuccess = false;

                foreach (Cliente cliente in clientes)
                {
                    if (cliente.usuario == UsuInput.text && cliente.password == PassInput.text)
                    {
                        Debug.Log("Inicio de sesión exitoso para: " + cliente.usuario);
                        ClienteID = cliente.id;
                        SceneManager.LoadScene("MainMenu");
                        loginSuccess = true;
                        break;
                    }
                }

                if (!loginSuccess)
                {
                    UsuInput.text = "";
                    UsuInput.placeholder.GetComponent<TextMeshProUGUI>().color = Color.red;
                    PassInput.text = "";
                    PassInput.placeholder.GetComponent<TextMeshProUGUI>().color = Color.red;
                    Debug.Log("No se ha encontrado ningun cliente");
                }
            }
        }
    }
    public void OnLoginButtonClick()
    {
        StartCoroutine(GetClientes());
    }

    public void irRegistro()
    {
        SceneManager.LoadScene("Registro");
    }
}