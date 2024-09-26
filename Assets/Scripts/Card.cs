using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Vector3 storedPos;
    public bool grabbed;
    private Vector3 screenPoint;
    private Vector3 offset;
    public float timer;

    void Awake()
    {
        grabbed = false;
        storedPos = transform.position;
    }

    void Update()
    {
        if (!grabbed && (storedPos != transform.position))
        {
            transform.position = storedPos;
            Debug.Log("position reset");
        }
    }

    private void OnMouseDown()
    {
        grabbed = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z + 0.1f));
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        grabbed = false;
    }

    private void OnMouseOver()
    {


        if (!grabbed)
        {
            timer += Time.deltaTime;

            if (timer > 1)
            {
                StartHoverDisplay();
            }
        }
        else
        {
            timer = 0.0f;
            EndHoverDisplay();
        }
    }

    private void StartHoverDisplay()
    {

    }

    private void EndHoverDisplay()
    {

    }
}
