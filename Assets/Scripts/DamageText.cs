using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    void Start()
    {
        Animation damageTextAnim = gameObject.GetComponent<Animation>();
        if (damageTextAnim != null)
        {
            damageTextAnim.Play();
        }
    }

}
