using System;
using UnityEngine;

public class Tank_Health : TankComponent {

	public const int MaxHealth=100;

	public int HP { get; private set; }

	new public bool IsAlive { get { return HP > 0; } }

	public event Action<Vector3> OnHit;
	public event Action OnDestroy;

	private void Start()
	{
		HP = 100;
	}


	public void Hit (string player, int damage, Vector3 hitDirection) {
		if (HP < 1)
			return;

		HP -= damage;
		if (HP < 1)
			OnDestroy ();
		else
			OnHit (hitDirection);
	}

}