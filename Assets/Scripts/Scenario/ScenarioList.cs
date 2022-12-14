using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenarioList
{
    public int wave;
    public int enemyHPOffset;
    public float waveStartDelay;
    public float spawnDelay;
    public int boss;
    public List<int> enemyList = new List<int>();


    public ScenarioList()
    {
        wave = 0;
        enemyHPOffset = 0;
        waveStartDelay = 0;
        spawnDelay = 0;
        boss = 0;
    }

    public ScenarioList(int w)
    {
        wave = w;
        enemyHPOffset = w;
        waveStartDelay = 3f;
        spawnDelay = 1f;
        boss = 0;
        for (int i = 0; i < w + 2; i++)
        {
            enemyList.Add(0);
        }
    }

    public ScenarioList(ScenarioList other)
    {
        wave = other.wave;
        enemyHPOffset = other.enemyHPOffset;
        waveStartDelay = other.waveStartDelay;
        spawnDelay = other.spawnDelay;
        enemyList.AddRange(other.enemyList);
        boss = other.boss;
    }

    public bool ValidateList()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] < 0)
            {
                Debug.LogError(String.Format("no enemy on wave {0}", wave));
                return false;
            }

        }
        if (wave < 0 || enemyHPOffset < 1 || spawnDelay < 0)
        {
            Debug.LogError(String.Format("value error on wave {0}", wave));
            return false;
        }

        return true;
    }
}

[System.Serializable]
public class ScenarioLists
{
    public List<ScenarioList> waveList = new List<ScenarioList>();

    // This is for making Json files
    //public ScenarioLists()
    //{
    //    waveList.Add(new ScenarioList());
    //    waveList.Add(new ScenarioList());
    //    waveList.Add(new ScenarioList());
    //}

    public bool ValidateLists()
    {
        for (int i = 0; i < waveList.Count; i++)
        {
            if (waveList[i].ValidateList() == false)
            {
                return false;
            }

            if (waveList[i].wave != i + 1)
            {
                Debug.LogError(String.Format("wave numbering error on {0}", i + 1));
                return false;
            }
        }
        return true;
    }
}
