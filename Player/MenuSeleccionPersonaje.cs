using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    private int index;
    [SerializeField] private Image imagen;
    [SerializeField] private TextMeshProUGUI nombre;

    private EleccionManager eleccionManager;

    private void Start()
    {
        eleccionManager = EleccionManager.Instance;

        index = PlayerPrefs.GetInt("JugadorIndex");

        if(index > eleccionManager.personajes.Count -1)
        {
            index = 0;
        }
        CambiarPantalla();
    }

    private void CambiarPantalla()
    {
        PlayerPrefs.SetInt("JugadorIndex", index);
        PlayerPrefs.SetString("NombreJugador", eleccionManager.personajes[index].nombre);
        imagen.sprite = eleccionManager.personajes[index].imagen;
        nombre.text = eleccionManager.personajes[index].nombre;
    }

    public void SiguientePersonaje()
    {
        if(index == eleccionManager.personajes.Count-1)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }

        CambiarPantalla();
    }

    public void AnteriorPersonaje()
    {
        if (index == 0)
        {
            index = eleccionManager.personajes.Count -1;
        }
        else
        {
            index -= 1;
        }

        CambiarPantalla();
    }

    public void IniciarJuego()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
