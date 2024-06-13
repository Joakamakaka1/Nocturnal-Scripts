using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;
    private IShop shop;

    public AudioClip attackSound;

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(Item.ItemType.Armor_1, Item.GetSprite(Item.ItemType.Armor_1), "Armor 1", Item.GetCost(Item.ItemType.Armor_1), 0);
        CreateItemButton(Item.ItemType.Potion_1, Item.GetSprite(Item.ItemType.Potion_1), "Pocion 1", Item.GetCost(Item.ItemType.Potion_1), 1);
        CreateItemButton(Item.ItemType.Buff_1, Item.GetSprite(Item.ItemType.Buff_1), "Buff daño x1.5", Item.GetCost(Item.ItemType.Buff_1), 3);
        CreateItemButton(Item.ItemType.Buff_2, Item.GetSprite(Item.ItemType.Buff_2), "Buff daño x2.5", Item.GetCost(Item.ItemType.Buff_2), 4);

        Hide();
    }
    private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 20f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, positionIndex * -shopItemHeight - positionIndex * 110f);

        shopItemTransform.Find("Nombre").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("Precio").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("ItemImagen").GetComponent<Image>().sprite = itemSprite;

        
        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {       
            TryBuyItem(itemType);
        };
        
    }

    public void TryBuyItem(Item.ItemType type)
    {
        if (shop.TrySpendGold(Item.GetCost(type)))
        {
            PlayAttackSound();
            shop.BoughtItem(type);
        }
        
    }

    public void Show(IShop shop)
    {
        this.shop = shop;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
    }
}
