using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool CombatActive;
    public PlayerStats playerStats;
    public List<EnemyCharInfo> enemyCombatants;
    public GameObject pfDamageText;
    public PlayerCharInfo playerCharInfo;
    public int enemyCount;
    public Vector3[] newEnemyPos;
    public GlobalManager globalManager;

    void Awake()
    {
        CombatActive = false;
        globalManager = FindObjectOfType<GlobalManager>();
        playerStats = globalManager.playerStats;
        playerCharInfo = FindObjectOfType<PlayerCharInfo>();
        enemyCount = 0;
        newEnemyPos = new Vector3[5];
        newEnemyPos[0].Set(0, 0, 0);
        newEnemyPos[1].Set(-3, 0, 0);
        newEnemyPos[2].Set(3, 0, 0);
        newEnemyPos[3].Set(-6, 0, 0);
        newEnemyPos[4].Set(6, 0, 0);
        Debug.Log("Start function ran");
    }

    public void SpawnEnemies(string[] enemyNames)
    {
        foreach (string name in enemyNames)
        {
            if (enemyCount >= 5) { return; }
            UnityEngine.Object pfEnemy = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/" + name + ".prefab", typeof(GameObject));
            Debug.Log("Spawning " + name);
            pfEnemy = UnityEngine.Object.Instantiate(pfEnemy, GameObject.Find("EnemiesCanvas").transform);
            pfEnemy.GameObject().transform.position = pfEnemy.GameObject().transform.position + newEnemyPos[enemyCount];
            pfEnemy.GetComponent<Animation>().Play("SpriteSpawn");
            enemyCount++;
        }
    }

    public void StartCombat()
    {
        CombatActive = true;
        enemyCombatants = FindObjectsOfType<EnemyCharInfo>().ToListPooled<EnemyCharInfo>();
        Debug.Log("Combat started");
    }

    //Increment the current wave, display new wave, update wave counter, spawn enemies.
    public IEnumerator AdvanceWave()
    {
        globalManager.currentWave++;
        GameObject.Find("WaveCounter").GetComponent<TMP_Text>().SetText("Wave " + globalManager.currentWave.ToString());
        yield return new WaitForSeconds(2);
        
        SpawnEnemies(gameObject.GetComponent<EnemyWarehouse>().GetWave(globalManager.currentWave));
        yield return new WaitForSeconds(1);
    }

    void Update()
    {
        if (CombatActive)
        {
            foreach (EnemyCharInfo enemy in enemyCombatants)
            {
                if (enemy != null)
                {
                    enemy.attackProgress += enemy.attackSpeed * Time.deltaTime * globalManager.gameSpeed;

                    if (enemy.attackProgress > 300)
                    {
                        enemy.attackProgress = 0;
                        EnemyAttack(enemy, playerStats);
                    }
                }
            }

            playerStats.attackProgress += playerStats.attackSpeed * Time.deltaTime * globalManager.gameSpeed;

            if (playerStats.attackProgress > 300)
            {
                playerStats.attackProgress = 0;
                PlayerAttack(playerStats, enemyCombatants[0]);
            }
        }
    }

    void PlayerAttack(PlayerStats assailant, EnemyCharInfo victim)
    {
        Debug.Log($"Attack initiated on {victim.name}");
        bool crit = UnityEngine.Random.Range(0,100) < assailant.critChance;
        int damageDealt;

        if (crit)
        {
            damageDealt = (assailant.damage - victim.defense) * 2 * UnityEngine.Random.Range(100 - assailant.attackVolatility, 100 + assailant.attackVolatility) / 100;
            victim.HP -= damageDealt;
        }
        else 
        {
            damageDealt = (assailant.damage - victim.defense) * UnityEngine.Random.Range(100 - assailant.attackVolatility, 100 + assailant.attackVolatility) / 100;
            victim.HP -= damageDealt;
        }
        
        GameObject damageText = Instantiate(pfDamageText, victim.gameObject.transform);
        damageText.GetComponentInChildren<TMP_Text>().SetText(damageDealt.ToString());
        Animation animationComponent = victim.GetComponent<Animation>();

        if (animationComponent.IsPlaying("SpriteShake"))
        {
            animationComponent.Rewind("SpriteShake");
        }
        else
        {
            animationComponent.Play("SpriteShake");
        }

        if (crit)
        {
            damageText.GetComponentInChildren<TextMeshPro>().color = Color.magenta;
        }

        Debug.Log($"{victim.HP} HP remaining");

        if (victim.HP < 0)
        {
            StartCoroutine( victim.Die() );
            enemyCombatants.Remove(victim);
            enemyCount--;

            if (enemyCombatants.Count == 0)
            {
                CombatCompleted();
            }
        }
    }

    void EnemyAttack(EnemyCharInfo assailant, PlayerStats victim)
    {
        Debug.Log($"Attack initiated on player");
        playerCharInfo.DealDamage((assailant.damage - victim.defense) * UnityEngine.Random.Range(100 - assailant.attackVolatility, 100 + assailant.attackVolatility) / 100);
        victim = globalManager.playerStats;
        Debug.Log($"{victim.HP} HP remaining");

        if (victim.HP < 0)
        {
            playerCharInfo.Die();
            CombatActive = false;
        }
    }

    //Call when combat is won, initiate shop
    void CombatCompleted()
    {
        CombatActive = false;
        StartCoroutine(globalManager.BeginShop());
    }
}



