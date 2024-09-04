using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class EnemyWarehouse : MonoBehaviour
{
    public string[][] wavePools;
    public string wavePools_txt;

    // Start is called before the first frame update
    void Awake()
    {
        string[] temp;
        int counter = 0;
        
        wavePools_txt = System.IO.File.ReadAllText(@"Assets/Data/WavePools.txt");
        temp = wavePools_txt.Split(':');
        wavePools = new string[temp.Length][];
        
        foreach (string difficulty in temp)
        {
            wavePools[counter] = difficulty.Split(Environment.NewLine);
            counter++;
        }
    }

    public string[] GetWave(int wave)
    {
        int difficulty = wave / 5;
        string[] waveReturned = new string[] { "" };

        while (waveReturned[0] == "")
        {
            waveReturned = wavePools[difficulty][UnityEngine.Random.Range(1, wavePools[difficulty].Length)].Split(",");
        }

        return waveReturned;
    }
}
