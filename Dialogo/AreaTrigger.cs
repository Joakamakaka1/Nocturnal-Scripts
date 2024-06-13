using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class AreaTrigger : Singleton<AreaTrigger>
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public string[] dialogue;
    private int index;

    public GameObject conButton;
    public float wordSpeed;
    public bool playerIsClose;
    public bool hasInteractedWithNPC = false;

    public AudioClip attackSound;

    // Cinemachine settings
    public CinemachineVirtualCamera cinemachineCamera;
    private bool cinematicCameraActive = false;
    private CinemachineFramingTransposer framingTransposer;

    void Start()
    {
        framingTransposer = cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerIsClose)
        {
            if (dialogPanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                dialogPanel.SetActive(true);
                StartCoroutine(TypeText());
            }
        }

        if (dialogText.text == dialogue[index])
        {
            conButton.SetActive(true);
        }
    }

    public void ZeroText()
    {
        dialogText.text = "";
        index = 0;
        dialogPanel.SetActive(false);

        if (cinematicCameraActive && !hasInteractedWithNPC)
        {
            framingTransposer.m_ScreenY = 0.5f;
            cinematicCameraActive = false;
        }
    }

    IEnumerator TypeText()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        conButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogText.text = "";
            StartCoroutine(TypeText());
        }
        else
        {
            ZeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasInteractedWithNPC)
            {
                return;
            }

            PlayAttackSound();
            playerIsClose = true;
            dialogPanel.SetActive(true);
            StartCoroutine(TypeText());

            if (!cinematicCameraActive)
            {
                framingTransposer.m_ScreenY = 0.3f; 
                cinematicCameraActive = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            ZeroText();
        }
    }

    public void InteractWithNPC()
    {
        hasInteractedWithNPC = true;

        // Revert Cinemachine settings
        framingTransposer.m_ScreenY = 0.5f;
        cinematicCameraActive = false;
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
    }
}