using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    ShopManager shopManager;
    [SerializeField]
    public int slotNumber;

    // Start is called before the first frame update
    void Awake()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }

    private void OnMouseDown()
    {
        shopManager.ItemSelected(slotNumber - 1);
    }
}
