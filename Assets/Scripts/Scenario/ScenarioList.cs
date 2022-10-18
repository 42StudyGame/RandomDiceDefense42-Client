using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenarioList
{
    public int wave;
    public int enemyHPOffset;
    public float spawnDelay;
    public List<int> enemyList = new List<int>();


    public ScenarioList()
    {
        wave = 0;
        enemyHPOffset = 0;
        spawnDelay = 0;
    }

    public ScenarioList(ScenarioList other)
    {
        wave = other.wave;
        enemyHPOffset = other.enemyHPOffset;
        spawnDelay = other.spawnDelay;
        enemyList.AddRange(other.enemyList);
    }

    public bool ValidateList()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] < 0)
                return false;
        }
        if (wave < 0 || enemyHPOffset < 1 || spawnDelay < 0)
            return false;
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
                
            if (i != waveList.Count - 1)
            {
                if (waveList[i].wave >= waveList[i + 1].wave)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
