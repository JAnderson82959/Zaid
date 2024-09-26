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
    GlobalManager globalManager;

    void Awake()
    {
        globalManager = FindObjectOfType<GlobalManager>();
        artifactPool = new List<Artifact>();

        artifactPool.Add(new Artifact(
            "Fists",
            "Oft-tested bludgeons of meat and bone. Your dearest friends.",
            "Regular attack.",
            "artifact_w_fist",
            true, false, true
            ));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Artifact GetArtifact(string name)
    {
        foreach (Artifact artifact in artifactPool)
        {
            if (artifact._name == name)
            {
                return artifact;
            }
        }

        return null;
    }
}
