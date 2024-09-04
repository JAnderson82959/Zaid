using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAnimation : MonoBehaviour
{
    public void DestroyParent()
    {
        if (gameObject.transform.parent != null)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
