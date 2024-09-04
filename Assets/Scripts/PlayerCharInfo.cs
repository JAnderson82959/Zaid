using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharInfo : MonoBehaviour
{
    public GlobalManager globalManager;
    Animation animationComponent;
    void Awake()
    {
        globalManager = Component.FindObjectOfType<GlobalManager>().GetComponent<GlobalManager>();
        animationComponent = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Die()
    {
        Debug.Log("Player died");
    }

    public void DealDamage(int amount)
    {
        globalManager.playerStats.HP -= amount;
        GetComponentInChildren<HPCounter>().UpdateHP();

        if (animationComponent.IsPlaying("SpriteShake"))
        {
            animationComponent.Rewind();
        }
        else
        {
            animationComponent.Play();
        }
    }
}
