using System;
using UnityEngine;
namespace TinForge.TankController
{
	public class Health : MonoBehaviour, IDamageable
	{

		public const int MaxHealth = 100;
		public int HP;

		new public bool IsAlive { get { return HP > 0; } }

		//Events
		public event Action OnHit;
		public event Action<int> OnHitDamage;
		public event Action<Vector3> OnHitDirection;
		public event Action OnDead;


		public void Damage(TankController player, int damage, Vector3 hitPos)
		{
			if (HP < 1)
				return;

			HP -= damage;
			Debug.Log("We've been hit by " + player.name);


			if (HP < 1)
				OnDead();
			else
			{
				if (OnHit != null)
					OnHit();
				if (OnHitDamage != null)
					OnHitDamage(damage);
				if (OnHitDirection != null)
					OnHitDirection(hitPos);
			}
		}
	}

}