using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats
{
    public int HP {  get; set; }
    public int damage { get; set; }
    public int defense { get; set; }
    public int attackSpeed { get; set; }
    public int attackVolatility { get; set; }
    public int critChance { get; set; }
    public float attackProgress { get; set; }
}

public class Artifact
{
    public string _name;
    public string _description;
    public string _ability;
    public string _sprite;
    public bool _isPlayerAttackTriggered;
    public bool _isPlayerDamageTriggered;

    public Artifact(string name, string description, string ability, string sprite, bool isPlayerAttackTriggered, bool isPlayerDamageTriggered)
    {
        _name = name;
        _description = description;
        _ability = ability;
        _sprite = sprite;
        _isPlayerAttackTriggered = isPlayerAttackTriggered;
        _isPlayerDamageTriggered = isPlayerDamageTriggered;
    }
}

public class GlobalManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject pfCombatMap;
    public GameObject pfShopCanvas;
    public int currentWave;
    public GameObject dimOverlay;
    GameObject shopCanvas;
    public List<Artifact> playerArtifacts;

    public int gameSpeed;

    void Start()
    {
        playerStats = new PlayerStats();
        playerStats.HP = 30;
        playerStats.damage = 8;
        playerStats.defense = 1;
        playerStats.critChance = 5;
        playerStats.attackSpeed = 60;
        playerStats.attackVolatility = 10;
        playerStats.attackProgress = 0;
        currentWave = 0;
        gameSpeed = 3;

        dimOverlay = GameObject.Find("DimOverlay");
    }

    //Initialize metastructures for population
    public GameObject CreateCombatMap()
    {
        if (pfCombatMap != null)
        {
            GameObject combatMap = Instantiate(pfCombatMap);
            return combatMap;
        }
        return null;
    }

    //Fill combat map with player, artifacts and specified enemies
    public void LoadCombat()
    {

    }

    //Start shop sequence
    public IEnumerator BeginShop()
    {
        yield return new WaitForSeconds(0.45f);
        Animation animation = dimOverlay.GetComponent<Animation>();
        animation.Play("DimOverlay");
        yield return new WaitForSeconds(0.5f);

        if (pfShopCanvas != null)
        {
            shopCanvas = Instantiate(pfShopCanvas);
            FindObjectOfType<ShopManager>().LoadShop(currentWave);
        }
    }

    public void EndShop()
    {
        StartCoroutine( EndShopHelper() );
    }

    public IEnumerator EndShopHelper()
    {
        Animation animation = dimOverlay.GetComponent<Animation>();
        shopCanvas.SetActive(false);
        animation.Play("LightenOverlay");
        yield return new WaitForSeconds(0.5f);

        StartCoroutine( FindObjectOfType<CombatManager>().AdvanceWave() );
    }

    public void ObtainItem( Item item )
    {
        foreach ( string stat in item._statsChanged )
        {
            typeof(PlayerStats).GetProperty( stat ).SetValue( playerStats, (int)typeof(PlayerStats).GetProperty( stat ).GetValue( playerStats ) + item._amountChanged[item._statsChanged.IndexOf(stat)]);
            
            if ( stat == "HP" )
            {
                FindObjectOfType<HPCounter>().UpdateHP();
            }
        }

        return;
    }
}
