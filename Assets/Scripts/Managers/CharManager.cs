using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Character
{
    public string name;
    public int HP;
    public int damage;
    public int defense;
    public int attackSpeed; 
    public int attackVolatility;
    public int critChance;
    public int rerollCount;
    public List<Artifact> artifacts;
}

public class CharManager : MonoBehaviour
{
    public List<Character> charList;
    public ArtifactManager artifactManager;

    void Awake()
    {
        charList = new List<Character>();
        artifactManager = FindObjectOfType<ArtifactManager>();

        charList.Add(new Character()
        {
            name = "Traveler",
            HP = 35,
            damage = 11,
            defense = 1,
            attackSpeed = 60,
            attackVolatility = 10,
            critChance = 5,
            rerollCount = 3,
            artifacts = new List<Artifact>() { artifactManager.GetArtifact("Fists") }
        });

        charList.Add(new Character()
        {
            name = "null",
            HP = 350,
            damage = 60,
            defense = 10,
            attackSpeed = 600,
            attackVolatility = 50,
            critChance = 14,
            rerollCount = 4,
            artifacts = new List<Artifact>() { artifactManager.GetArtifact("Fists"), artifactManager.GetArtifact("Fists") }
        });
    }

}
