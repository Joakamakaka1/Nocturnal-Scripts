using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarEscena : MonoBehaviour
{

    public void Jugar()
    {
        SceneManager.LoadScene("EleccionPj");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Credito()
    {
        Application.OpenURL("https://edermunizz.itch.io/free-pixel-art-forest/download/eyJpZCI6MTIxNjU4LCJleHBpcmVzIjoxNzE2MDQ1Mjg4fQ%3d%3d.jVD9zsUE44LHNWvtMYfoehYcy18%3d");
    }
}
