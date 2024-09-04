using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    List<Item> slots;
    GlobalManager globalManager;

    void Awake()
    {
        slots = new List<Item>();
        globalManager = FindObjectOfType<GlobalManager>();
    }

    public void LoadShop(int wave)
    {
        for (int index = 0; index < 3; index++)
        {
            slots.Add(FindObjectOfType<ItemWarehouse>().GetItem(wave));
            GameObject.Find("ItemTitle" + (index + 1).ToString()).GetComponent<TMP_Text>().SetText(slots[index]._name);
            GameObject.Find("ItemDesc" + (index + 1).ToString()).GetComponent<TMP_Text>().SetText(slots[index]._description);
        }
    }

    public void ItemSelected(int slot)
    {
        globalManager.ObtainItem(slots[slot]);
        globalManager.EndShop();
    }
}
