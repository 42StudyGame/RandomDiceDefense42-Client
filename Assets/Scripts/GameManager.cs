using UnityEngine;

public partial class GameManager : MonoBehaviour {
	public TowerManager towerManager;
	public EnemyManager enemyManager;
	public int playerHealth { get; private set; } = 3;
	public int sp = 500;
	public int towerCost { get; private set; } = 10;
    
    
	private void Awake() {
		//towerManager.Init(this);
		enemyManager.Init();
	}

	public void CreateTower() {
		if (sp >= towerCost)
		{
			towerManager.AddTower();
			sp -= towerCost;
			towerCost += 10;
		}
	}

	public void OnDamage(int damage)
	{
		if (playerHealth > 0)
		{
			playerHealth -= damage;
			
		}
	}
    // private void Update()
    // {
    //     
    // }
}
