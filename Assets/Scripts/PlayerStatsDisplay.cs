using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
    public GlobalManager globalManager;
    private TMP_Text damageDisplay;
    private TMP_Text attackSpeedDisplay;
    private TMP_Text defenseDisplay;
    private TMP_Text maxHPDisplay;
    private TMP_Text critChanceDisplay;
    private TMP_Text attackProgressDisplay;

    void Awake()
    {
        globalManager = FindObjectOfType<GlobalManager>();
        damageDisplay = GameObject.Find("DamageStatText").GetComponent<TMP_Text>();
        attackSpeedDisplay = GameObject.Find("AttackSpeedStatText").GetComponent<TMP_Text>();
        defenseDisplay = GameObject.Find("DefenseStatText").GetComponent<TMP_Text>();
        maxHPDisplay = GameObject.Find("MaxHPStatText").GetComponent<TMP_Text>();
        critChanceDisplay = GameObject.Find("CritChanceStatText").GetComponent<TMP_Text>();
        attackProgressDisplay = GameObject.Find("AttackProgressStatText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        damageDisplay.SetText("Damage: " + globalManager.playerStats.damage.ToString());
        attackSpeedDisplay.SetText("Attack Speed: " + globalManager.playerStats.attackSpeed.ToString());
        defenseDisplay.SetText("Defense: " + globalManager.playerStats.defense.ToString());
        maxHPDisplay.SetText("HP: " + globalManager.playerStats.HP.ToString());
        critChanceDisplay.SetText("Crit Chance: " + globalManager.playerStats.critChance.ToString() + "%");
        attackProgressDisplay.SetText("Attack progress: " + globalManager.playerStats.attackSpeed.ToString() + " / 100");
    }
}
