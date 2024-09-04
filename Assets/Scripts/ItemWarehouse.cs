using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public bool _isArtifact;
    public string _name;
    public string _description;
    public List<string> _statsChanged;
    public List<int> _amountChanged;
    
    public Item(string name, bool isArtifact, string description, List<string> statsChanged, List<int> amountChanged)
    {
        _isArtifact = isArtifact;
        _name = name;
        _description = description;
        _statsChanged = statsChanged;
        _amountChanged = amountChanged;
    }
}
public class ItemWarehouse : MonoBehaviour
{
    public List<Item> itemPool;

    // Start is called before the first frame update
    void Awake()
    {
        itemPool = new List<Item>();

        itemPool.Add(new Item(
            "Survival",
            false,
            "Increases HP by 150",
            new List<string> { "HP" },
            new List<int> { 150 }
            ));

        itemPool.Add(new Item(
            "Resolve",
            false,
            "Increases damage by 10",
            new List<string> { "damage" },
            new List<int> { 10 }
            ));

        itemPool.Add(new Item(
            "Planning",
            false,
            "Increases attack speed by 1",
            new List<string> { "attackSpeed" },
            new List<int> { 1 }
            ));

        itemPool.Add(new Item(
            "Fear",
            false,
            "Increases defense by 3",
            new List<string> { "defense" },
            new List<int> { 3 }
            ));

        itemPool.Add(new Item(
            "Intuition",
            false,
            "Increases crit chance by 5%",
            new List<string> { "critChance" },
            new List<int> { 5 }
            ));
    }


    public Item GetItem(int wave)
    {
        return itemPool[UnityEngine.Random.Range(0, itemPool.Count)];
    }
}
