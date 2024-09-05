using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedSwitch : MonoBehaviour
{
    GlobalManager globalManager;

    void Awake()
    {
        globalManager = FindObjectOfType<GlobalManager>();
        
        if (globalManager.gameSpeed == 1)
        {
            gameObject.transform.GetChild(0).transform.position += new Vector3(1.44f / 2, 0, 0);
        }
        else if (globalManager.gameSpeed == 3)
        {
            gameObject.transform.GetChild(0).transform.position += new Vector3(-1.44f / 2, 0, 0);
        }
    }

    public void MoveIndicator()
    {
        if (globalManager.gameSpeed == 3)
        {
            globalManager.gameSpeed = 1;
            gameObject.transform.GetChild(0).transform.position += new Vector3(2.88f / 2, 0, 0);
        }
        else
        {
            globalManager.gameSpeed += 1;
            gameObject.transform.GetChild(0).transform.position += new Vector3(-1.44f / 2, 0, 0);
        }
    }

    public void OnMouseDown()
    {
        MoveIndicator();
    }
}
