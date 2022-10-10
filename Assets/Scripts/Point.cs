using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
	public bool isTower { get; set; }

	private void Start() {
		isTower = false;
	}
}
