using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShop
{
    void BoughtItem(Item.ItemType type);
    bool TrySpendGold(int goldamount);
}
