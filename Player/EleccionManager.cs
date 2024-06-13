using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleccionManager : MonoBehaviour
{
    public static EleccionManager Instance;
    public List<EleccionPlayer> personajes;

    private void Awake()
    {
        if(EleccionManager.Instance == null)
        {
            EleccionManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
