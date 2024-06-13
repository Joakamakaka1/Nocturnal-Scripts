using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public string[] dialogue;
    private int index;

    public GameObject conButton;
    public float wordSpeed;
    public bool playerIsClose;

    public AudioClip attackSound;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerIsClose)
        {
            if (dialogPanel.activeInHierarchy )
            {
                ZeroText();
            }
            else
            {
                dialogPanel.SetActive(true);
                StartCoroutine(TypeText());
            }
        }

        if(dialogText.text == dialogue[index])
        {
            conButton.SetActive(true);
        }
    }

    public void ZeroText()
    {
        dialogText.text = "";
        index = 0;
        dialogPanel.SetActive(false);
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

        if(index < dialogue.Length -1 )
        {
            index++;
            PlayAttackSound();
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
            playerIsClose = true;
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

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
    }
}