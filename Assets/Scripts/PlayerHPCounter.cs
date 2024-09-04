using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPCounter : MonoBehaviour
{
    public GlobalManager globalManager;

    void Awake()
    {
        globalManager = Component.FindObjectOfType<GlobalManager>().GetComponent<GlobalManager>();
        UpdateHP();
    }

    public void UpdateHP()
    {
        if (globalManager != null)
        {
            gameObject.GetComponent<TMP_Text>().SetText(globalManager.playerStats.HP.ToString());
        }
        else
        {
            gameObject.GetComponent<TMP_Text>().SetText("");
        }
    }
}
