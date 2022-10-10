using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public TowerManager towerManager;
	public EnemyManager enemyManager;
    
	private void Awake() {
		towerManager = FindObjectOfType<TowerManager>();
		enemyManager = FindObjectOfType<EnemyManager>();
		towerManager.Init(this);
		enemyManager.Init();
	}

    // private void Update()
    // {
    //     
    // }
}
