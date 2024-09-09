using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact
{
    public string _name;
    public string _description;
    public string _abilityDesc;
    public string _sprite;
    public bool _isPlayerAttackTriggered;
    public bool _isPlayerDamageTriggered;
    public bool _isWeapon;

    public Artifact(string name, string description, string abilityDesc, string sprite, bool isPlayerAttackTriggered, bool isPlayerDamageTriggered, bool isWeapon)
    {
        _name = name;
        _description = description;
        _abilityDesc = abilityDesc;
        _sprite = sprite;
        _isWeapon = isWeapon;
        _isPlayerAttackTriggered = isPlayerAttackTriggered;
        _isPlayerDamageTriggered = isPlayerDamageTriggered;
    }
}

public class ArtifactManager : MonoBehaviour
{
    List<Artifact> artifactPool;
    List<Artifact> playerArtifacts;

    void Start()
    {
        artifactPool = new List<Artifact>();
        playerArtifacts = new List<Artifact>();

        artifactPool.Add(new Artifact(
            "Fists",
            "Long-honed bludgeons of meat and bone. Your dearest friends.",
            "Regular attack.",
            "artifact_w_fist",
            true, false, true
            ));

        playerArtifacts.Add(artifactPool[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
