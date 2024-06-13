using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string cargarEscena;
    [SerializeField] private string sceneTransitionName;

    private float tiempoEspera = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        while (tiempoEspera >= 0)
        {
            tiempoEspera -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(cargarEscena);
    }
}