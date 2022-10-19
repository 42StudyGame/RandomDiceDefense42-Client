using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject {
	public string type = "Normal";
	public float health = 100;
	public int sp = 10;
	public float speed = 1f;
	public Sprite sprite;
}
