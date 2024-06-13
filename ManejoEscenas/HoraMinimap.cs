using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoraMinimap : MonoBehaviour
{
    public TextMeshProUGUI clockText; 

    void Start()
    {
        StartCoroutine(UpdateClock());
    }

    IEnumerator UpdateClock()
    {
        while (true)
        {
            clockText.text = System.DateTime.Now.ToString("HH:mm");
            yield return new WaitForSeconds(60f);
        }
    }
}
