using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public float waitToRespawn;

    public string nivelACargar;

    private void Awake()
    {
        Instance = this;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    IEnumerator RespawnCo()
    {
        PlayerController.Instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        UIFade.Instance.FadeToBlack();

        yield return new WaitForSeconds(1f);

        UIFade.Instance.FadeToClear();

        PlayerController.Instance.gameObject.SetActive(true);

    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCoroutine());
    }

    public IEnumerator EndLevelCoroutine()
    {
        UIFade.Instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(nivelACargar);
    }
}
