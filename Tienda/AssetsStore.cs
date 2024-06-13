using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsStore : MonoBehaviour
{
    private static AssetsStore _i;

    public Sprite ArmorNone;
    public Sprite Armor_1;
    public Sprite Armor_2;

    public Sprite LifePotionNone;
    public Sprite Potion_1;
    public Sprite Potion_2;

    public Sprite DamageBuffNone;
    public Sprite Buff_1;
    public Sprite Buff_2;

    public static AssetsStore i
    {
        get
        {
            if (_i == null) _i = Resources.Load<AssetsStore>("GameAssetsStore");
            return _i;
        }
    }
}
