using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float Start_Hp = 100;
	private float health;
	private TextMesh text;

	private void Start() {
		health = Start_Hp;
		text = GetComponentInChildren<TextMesh>();
		text.text = health.ToString();
	}

	public void OnDamage(float Damage) {
		health -= Damage;
		if (health <= 0)
			Die();
		text.text = health.ToString();
	}

	public void Die() {
		//effect
		Destroy(gameObject);
	}
}
