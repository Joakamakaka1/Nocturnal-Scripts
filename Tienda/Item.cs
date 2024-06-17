using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        ArmorNone,
        Armor_1,
        Armor_2,
        LifePotionNone,
        Potion_1,
        Potion_2,
        DamageBuffNone,
        Buff_1,
        Buff_2
    }

    public static int GetCost(ItemType type)
    {
        switch (type)
        {
            default:
            case ItemType.ArmorNone: return 0;
            case ItemType.Armor_1: return 25;
            case ItemType.Armor_2: return 100;
            case ItemType.LifePotionNone: return 0;
            case ItemType.Potion_1: return 15;
            case ItemType.Potion_2: return 100;
            case ItemType.DamageBuffNone: return 0;
            case ItemType.Buff_1: return 10;
            case ItemType.Buff_2: return 15;

        }
    }

    
    public static Sprite GetSprite(ItemType type)
    {
        switch(type)
        {
            default:
            case ItemType.ArmorNone: return AssetsStore.i.ArmorNone;
            case ItemType.Armor_1: return AssetsStore.i.Armor_1;
            case ItemType.Armor_2: return AssetsStore.i.Armor_2;
            case ItemType.LifePotionNone: return AssetsStore.i.LifePotionNone;
            case ItemType.Potion_1: return AssetsStore.i.Potion_1;
            case ItemType.Potion_2: return AssetsStore.i.Potion_2;
            case ItemType.DamageBuffNone: return AssetsStore.i.DamageBuffNone;
            case ItemType.Buff_1: return AssetsStore.i.Buff_1;
            case ItemType.Buff_2: return AssetsStore.i.Buff_2;
        }
    }
    
}
