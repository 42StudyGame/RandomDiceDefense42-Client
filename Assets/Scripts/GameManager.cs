using UnityEngine;

public partial class GameManager
{
	public int playerHealth { get; private set; } = 3;
	public int sp = 500;
	public int towerCost { get; private set; } = 10;

	public TowerManager towerManager;
	public EnemyManager enemyManager;
	public ScenarioManager scenarioManager;
	public UIManager uiManager;

	public void CreateTower() => _CreateTower();
	public void OnDamage(int damage) => _OnDamage(damage);
}

public partial class GameManager : MonoBehaviour
{
	private void Awake() {
		//towerManager.Init(this);
		enemyManager.Init();
		scenarioManager.Init();
	}
}

public partial class GameManager
{
	private void _CreateTower() {
		if (sp >= towerCost && towerManager.AddTower())
		{
			sp -= towerCost;
			uiManager.SetSpText(sp.ToString());
			towerCost += 10;
			uiManager.SetCostText(towerCost.ToString());
		}
	}

	private void _OnDamage(int damage)
	{
		if (playerHealth > 0)
		{
			playerHealth -= damage;
			uiManager.PlayerOnDamage(damage);
		}
	}
}
