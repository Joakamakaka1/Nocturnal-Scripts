using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        if (currentPosition.x < lastPosition.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (currentPosition.x > lastPosition.x)
        {
            
            spriteRenderer.flipX = true;
        }

        lastPosition = currentPosition;
    }
}