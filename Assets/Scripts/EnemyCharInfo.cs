using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharInfo : MonoBehaviour
{
    [SerializeField]
    public int HP;
    [SerializeField]
    public int damage;
    [SerializeField]
    public int attackSpeed;
    [SerializeField]
    public int attackVolatility;
    [SerializeField]
    public int defense;
    public float attackProgress;

    void Start()
    {
    }

    public IEnumerator Die()
    {
        GetComponent<Animation>().Play("EnemyDeath");
        yield return new WaitForSeconds(0.45f);
        gameObject.transform.DetachChildren();
        gameObject.SetActive(false);
    }
}
