using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destructTime = 3f;
    public Vector3 offset = new Vector3(0,2,0);
    public Vector3 randomizeIntensity = new Vector3(0.5f,0,0);

    private void Start()
    {
        Destroy(gameObject, destructTime);

        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
                                               Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
                                               Random.Range(-randomizeIntensity.z, randomizeIntensity.z));
    }

    
}
