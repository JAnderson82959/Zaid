using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool combatActive;
    public PlayerStats playerStats;
    public List<EnemyCharInfo> enemyCombatants;
    public GameObject pfDamageText;
    public PlayerCharInfo playerCharInfo;
    public int enemyCount;
    public Vector3[] newEnemyPos;
    public GlobalManager globalManager;
    public bool combatPaused;
    public List<GameObject> artifactObjects;

    void Awake()
    {
        combatActive = false;
        globalManager = FindObjectOfType<GlobalManager>();
        playerCharInfo = FindObjectOfType<PlayerCharInfo>();
        enemyCount = 0;

        newEnemyPos = new Vector3[5];
        newEnemyPos[0].Set(0, 0, 0);
        newEnemyPos[1].Set(-3, 0, 0);
        newEnemyPos[2].Set(3, 0, 0);
        newEnemyPos[3].Set(-6, 0, 0);
        newEnemyPos[4].Set(6, 0, 0);

        artifactObjects = new List<GameObject>();

        Debug.Log("Start function ran");
    }

    public void SpawnEnemies(string[] enemyNames)
    {
        foreach (string name in enemyNames)
        {
            if (enemyCount >= 5) { return; }
            UnityEngine.Object pfEnemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemies/" + name + ".prefab");
            Debug.Log("Spawning " + name);
            pfEnemy = UnityEngine.Object.Instantiate(pfEnemy, GameObject.Find("EnemiesCanvas").transform);
            pfEnemy.GameObject().GetComponent<Card>().storedPos = pfEnemy.GameObject().transform.position + newEnemyPos[enemyCount];
            pfEnemy.GetComponent<Animation>().Play("SpriteSpawn");
            enemyCount++;
        }
    }

    public void SpawnArtifacts(List<Artifact> artifacts)
    {
        GameObject canvas = GameObject.Find("MagicalItemsCanvas");

        foreach (Artifact artifact in artifacts)
        {
            GameObject artifactObject = UnityEngine.Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Artifact.prefab"), GameObject.Find("MagicalObjectsCanvas").transform);
            artifactObject.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/" + artifact._sprite + ".png") as Sprite;
            artifactObject.name = artifact._name;

            artifactObjects.Add(artifactObject);
        }

        UpdateArtifactPositions();
    }

    public void UpdateArtifactPositions()
    {
        int counter = 0;
        int scale = 18 / artifactObjects.Count;

        foreach (GameObject artifact in  artifactObjects)
        {
            artifact.transform.position = artifact.transform.parent.position + new Vector3 (0, scale * (counter - (artifactObjects.Count / 2)), 0);
        }
    }

    public void StartCombat()
    {
        combatActive = true;
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
        if (combatActive)
        {
            foreach (EnemyCharInfo enemy in enemyCombatants)
            {
                if (enemy != null)
                {
                    enemy.attackProgress += enemy.attackSpeed * Time.deltaTime * globalManager.gameSpeed;

                    if (enemy.attackProgress > 300)
                    {
                        enemy.attackProgress = 0;
                        EnemyAttack(enemy, globalManager.playerStats);
                    }
                }
            }

            globalManager.playerStats.attackProgress += globalManager.playerStats.attackSpeed * Time.deltaTime * globalManager.gameSpeed;

            if (globalManager.playerStats.attackProgress > 300)
            {
                globalManager.playerStats.attackProgress = 0;
                PlayerAttack(globalManager.playerStats, enemyCombatants[0]);
            }
        }
    }

    void PlayerAttack(PlayerStats assailant, EnemyCharInfo victim)
    {
        Debug.Log($"Attack initiated on {victim.name}");

        //check if artifacts will be triggered


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
        
        GameObject damageText = Instantiate(pfDamageText, GameObject.Find("HitNumbers").transform);
        damageText.GetComponentInChildren<TMP_Text>().SetText(damageDealt.ToString());
        damageText.transform.position = victim.transform.position;
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
        Debug.Log($"{victim.HP} HP remaining");

        if (victim.HP < 0)
        {
            playerCharInfo.Die();
            combatActive = false;
        }
    }

    //Call when combat is won, initiate shop
    void CombatCompleted()
    {
        combatActive = false;
        StartCoroutine( globalManager.BeginShop() );
    }
}



