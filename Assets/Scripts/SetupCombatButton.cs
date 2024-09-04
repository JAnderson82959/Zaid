using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetupCombatButton : MonoBehaviour
{
    // Start is called before the first frame update
    GlobalManager globalManager;
    GameObject combatMap;

    void Start()
    {
        globalManager = GameObject.Find("GlobalManager").GetComponent<GlobalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        combatMap = globalManager.CreateCombatMap();
        StartCoroutine(combatMap.GetComponentInChildren<CombatManager>().AdvanceWave());
    }
}
