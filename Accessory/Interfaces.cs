using UnityEngine;
using TinForge.TankController;

public interface IDestroyable
{

	void Destroy();

}

public interface IDamageable
{

	void Damage(TankController tc, int d, Vector3 pos);

}
