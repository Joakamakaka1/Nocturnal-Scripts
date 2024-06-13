using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private UIShop uishop;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        IShop shop = collider.GetComponent<IShop>();
        if(shop != null)
        {
            uishop.Show(shop);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        IShop shop = collider.GetComponent<IShop>();
        if (shop != null)
        {
            uishop.Hide();
        }
    }
}
