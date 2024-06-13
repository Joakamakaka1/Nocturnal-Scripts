using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public AudioClip stopSound;

    // Referencia al panel de pausa en la interfaz de usuario.
    public GameObject pauseScreen;

    // Variable para indicar si el juego est� pausado.
    public bool isPaused;

    // Nombres de las escenas para selecci�n de nivel y men� principal.
    public string eleccionPj, mainMenu;

    // Instancia est�tica para acceder f�cilmente desde otras clases.
    public static PauseMenu instance;

    // M�todo que crea la instancia de la clase
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Si se presiona la tecla M.
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayStopSound();
            PauseUnPause(); // Llama al m�todo para pausar/despausar el juego.
        }
    }

    // M�todo para pausar o despausar el juego.
    public void PauseUnPause()
    {
        // Si el juego est� pausado.
        if (isPaused)
        {
            isPaused = false; // Marca que el juego no est� pausado.
            pauseScreen.SetActive(false); // Desactiva el panel de pausa en la interfaz de usuario.
            Time.timeScale = 1f; // Reanuda el tiempo en el juego.
        }
        // Si el juego no est� pausado.
        else
        {
            isPaused = true; // Marca que el juego est� pausado.
            pauseScreen.SetActive(true); // Activa el panel de pausa en la interfaz de usuario.
            Time.timeScale = 0f; // Pausa el tiempo en el juego.
        }
    }

    // M�todo para cargar la escena de selecci�n de nivel.
    public void LevelSelect()
    {
         SceneManager.LoadScene(eleccionPj); 
         Time.timeScale = 1f;
         DontDestroyOnLoadManager.ClearDontDestroyOnLoadObjects();
    }

    // M�todo para cargar la escena del men� principal.
    public void MainMenu()
    {
          SceneManager.LoadScene(mainMenu); // Carga la escena del men� principal.
          Time.timeScale = 1f; // Reanuda el tiempo en el juego.
          DontDestroyOnLoadManager.ClearDontDestroyOnLoadObjects();
    }

    private void PlayStopSound()
    {
        if (stopSound != null)
        {
            AudioSource.PlayClipAtPoint(stopSound, transform.position);
        }
    }
}
