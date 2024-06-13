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

    // Variable para indicar si el juego está pausado.
    public bool isPaused;

    // Nombres de las escenas para selección de nivel y menú principal.
    public string eleccionPj, mainMenu;

    // Instancia estática para acceder fácilmente desde otras clases.
    public static PauseMenu instance;

    // Método que crea la instancia de la clase
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
            PauseUnPause(); // Llama al método para pausar/despausar el juego.
        }
    }

    // Método para pausar o despausar el juego.
    public void PauseUnPause()
    {
        // Si el juego está pausado.
        if (isPaused)
        {
            isPaused = false; // Marca que el juego no está pausado.
            pauseScreen.SetActive(false); // Desactiva el panel de pausa en la interfaz de usuario.
            Time.timeScale = 1f; // Reanuda el tiempo en el juego.
        }
        // Si el juego no está pausado.
        else
        {
            isPaused = true; // Marca que el juego está pausado.
            pauseScreen.SetActive(true); // Activa el panel de pausa en la interfaz de usuario.
            Time.timeScale = 0f; // Pausa el tiempo en el juego.
        }
    }

    // Método para cargar la escena de selección de nivel.
    public void LevelSelect()
    {
         SceneManager.LoadScene(eleccionPj); 
         Time.timeScale = 1f;
         DontDestroyOnLoadManager.ClearDontDestroyOnLoadObjects();
    }

    // Método para cargar la escena del menú principal.
    public void MainMenu()
    {
          SceneManager.LoadScene(mainMenu); // Carga la escena del menú principal.
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
