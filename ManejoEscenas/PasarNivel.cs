using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNivel : MonoBehaviour
{

    [Header ("Dialogo")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public string[] dialogue;
    private int index;

    public GameObject conButton;
    public float wordSpeed;
    public bool playerIsClose;

    public AudioClip attackSound;

    [Header("Escena")]
    //[SerializeField] private string sceneToLoad;
    [SerializeField] private int cantidadEnemigos;
    [SerializeField] private int enemigosEliminados;

    public static int totalEnemigosEliminados = 0;

    private float waitToLoadTime = 2f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cantidadEnemigos = GameObject.FindGameObjectsWithTag("Enemigo").Length;

        enemigosEliminados = 0;
        totalEnemigosEliminados = 0;
    }
    public static int ObtenerTotalEnemigosEliminados()
    {
        return totalEnemigosEliminados;
    }

    private void ActivarBandera()
    {
        animator.SetTrigger("Activar");
    }

    public void EnemigoEliminado()
    {
        enemigosEliminados += 1;
        totalEnemigosEliminados += 1;

        if (enemigosEliminados == cantidadEnemigos)
        {
            ActivarBandera();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (enemigosEliminados == cantidadEnemigos)
            {
                PlayAttackSound();
                StartCoroutine(LoadSceneRoutine());
                dialogPanel.SetActive(false);
            }
            else
            {
                playerIsClose = true;
                dialogPanel.SetActive(true);
                StartCoroutine(TypeText());
            }
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        UIFade.Instance.FadeToBlack(); 

        yield return new WaitForSeconds(waitToLoadTime); 

        //SceneManager.LoadScene(sceneToLoad);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        UIFade.Instance.FadeToClear();
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
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
            PlayAttackSound();
            dialogText.text = "";
            StartCoroutine(TypeText());
        }
        else
        {
            ZeroText();
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
}