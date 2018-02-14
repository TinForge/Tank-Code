using System;
using UnityEngine;

public class Tank_Health : TankComponent {

	public int HP { get; private set; }

	public bool IsAlive { get { return HP > 0; } }

	public event Action OnHit;
	public event Action OnDestroy;

	public void Hit (string player, int damage, Vector3 hitPos) {
		if (HP < 1)
			return;

		HP -= damage;
		if (HP < 1)
			OnDestroy ();
		else
			OnHit ();
	}

}