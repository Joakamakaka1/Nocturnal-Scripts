using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Puntuaje : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        textMesh.text = PuntuacionManager.Instance.Puntuacion.ToString("0");
    }
}
