using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class GameManager : MonoBehaviour {
	public TowerManager towerManager;
	public EnemyManager enemyManager;
    
	private void Awake() {
		//towerManager.Init(this);
		enemyManager.Init();
	}

	public void CreateTower() {
		towerManager.AddTower();
	}

    // private void Update()
    // {
    //     
    // }
}
