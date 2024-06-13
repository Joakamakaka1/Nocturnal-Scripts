using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    private int currentGold = 0;

    const string cantidadCoin = "Coin Cantidad";
    public int CurrentGold => currentGold;

    public void UpdateCurrentGold(int newGold = -1)
    {
        if (newGold >= 0)
        {
            currentGold = newGold;
        }
        else
        {
            currentGold += 1;
        }

        if (goldText == null)
        {
            goldText = GameObject.Find(cantidadCoin).GetComponent<TMP_Text>();
        }

        goldText.text = currentGold.ToString("D3");
    }
}

