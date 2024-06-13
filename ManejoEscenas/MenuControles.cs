using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuControles : MonoBehaviour
{
    public AudioClip stopSound;

    public GameObject pauseScreen;

    public bool isPaused;

    public static MenuControles instance;

    public Image wKeyImage;
    public Image aKeyImage;
    public Image sKeyImage;
    public Image dKeyImage;

    public Sprite wKeyNormal;
    public Sprite wKeyPressed;
    public Sprite aKeyNormal;
    public Sprite aKeyPressed;
    public Sprite sKeyNormal;
    public Sprite sKeyPressed;
    public Sprite dKeyNormal;
    public Sprite dKeyPressed;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            wKeyImage.sprite = wKeyPressed;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            wKeyImage.sprite = wKeyNormal;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            aKeyImage.sprite = aKeyPressed;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            aKeyImage.sprite = aKeyNormal;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            sKeyImage.sprite = sKeyPressed;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            sKeyImage.sprite = sKeyNormal;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            dKeyImage.sprite = dKeyPressed;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            dKeyImage.sprite = dKeyNormal;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                PlayStopSound();
                PauseUnPause();
            }
        }
    }

    public void OpenMenu()
    {
        PlayStopSound();
        PauseUnPause();
    }

    public void PauseUnPause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
        }
    }

    private void PlayStopSound()
    {
        if (stopSound != null)
        {
            AudioSource.PlayClipAtPoint(stopSound, transform.position);
        }
    }
}