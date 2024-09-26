using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    GameObject backdrop;

    private void Start()
    {
        backdrop = transform.Find("ButtonBackdrop").gameObject;        
    }

    private void OnMouseEnter()
    {
        backdrop.SetActive(true);
    }

    private void OnMouseExit() 
    {
        backdrop?.SetActive(false);
    }
}
