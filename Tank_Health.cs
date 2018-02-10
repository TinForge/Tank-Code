using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Health : TankComponent {

	private int health;

	public int Health { get { return health; } }

	public event Action OnHit;
	public event Action OnHit;
	public event Action OnDestroy;

	public void Hit (string player, int damage) {
		if ()

	}

}