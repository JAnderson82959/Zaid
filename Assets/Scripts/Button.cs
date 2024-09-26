using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public enum ButtonType
{
    NewRun,
    Options,
    CharSelect
}

public class Button : MonoBehaviour
{
    GameObject backdrop;
    [SerializeField]
    public ButtonType buttonType;
    [SerializeField]
    public int slotNumber;
    public CharManager charManager;
    public Character character;
    GlobalManager globalManager;


    private void Start()
    {
        backdrop = transform.Find("ButtonBackdrop").gameObject;     
        charManager = FindObjectOfType<CharManager>();
        globalManager = FindObjectOfType<GlobalManager>();

        if (buttonType == ButtonType.CharSelect)
        {
            character = charManager.charList[slotNumber];
            GetComponentInChildren<TMP_Text>().SetText(character.name);
        }
    }

    private void OnMouseEnter()
    {
        backdrop.SetActive(true);
    }

    private void OnMouseExit() 
    {
        backdrop.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (buttonType == ButtonType.NewRun)
        {
            Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/pfCharacterSelect.prefab"));
            Destroy(GameObject.Find("pfMainMenu"));
        }

        else if (buttonType == ButtonType.Options)
        {

        }

        else if (buttonType == ButtonType.CharSelect)
        {
            GameObject combatMap = globalManager.CreateCombatMap();

            globalManager.playerStats.HP = character.HP;
            globalManager.playerStats.damage = character.damage;
            globalManager.playerStats.defense = character.defense;
            globalManager.playerStats.critChance = character.critChance;
            globalManager.playerStats.attackSpeed = character.attackSpeed;
            globalManager.playerStats.attackVolatility = character.attackVolatility;
            globalManager.playerArtifacts = character.artifacts;

            globalManager.StartRun();
            transform.root.gameObject.SetActive(false);
        }
    }
}
