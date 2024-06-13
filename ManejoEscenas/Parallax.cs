using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 velocidadMovimiento;

    private Vector2 offset;
    private Material material;

    private void Awake()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            material = new Material(image.material);
            image.material = material;
        }
    }

    void Update()
    {
        offset = velocidadMovimiento * Time.deltaTime;
        if (material != null)
        {
            material.mainTextureOffset += offset;
        }
    }
}