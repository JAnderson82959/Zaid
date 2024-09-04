using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButton : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Fight button clicked");
        GameObject.Find("CombatManager").GetComponent<CombatManager>().StartCombat();
    }
}
