using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAI : TankComponent {

	[SerializeField] private float reloadTime = 4;

	public bool IsReloading = false;

	[SerializeField] private Transform target;
	[SerializeField] private Transform turret;
	[SerializeField] private Transform muzzle;
	[SerializeField] private	GameObject shell;


	void Update()
	{
		if (IsAlive)
		{
			turret.LookAt(target);

			Ray ray = new Ray(muzzle.position, muzzle.up);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 500))
			{
				if (raycastHit.transform.root == target)
					Fire();
			}
		}
	}

	public void Fire()
	{
		if (IsReloading)
			return;

			StartCoroutine(ReloadTimer());
			Instantiate(shell, muzzle.position, muzzle.rotation, null);
	}

	private IEnumerator ReloadTimer()
	{
		IsReloading = true;
		float t = 0;
		while (t <= reloadTime)
		{
			t += Time.deltaTime;
			yield return null;
		}
		IsReloading = false;
	}

}
