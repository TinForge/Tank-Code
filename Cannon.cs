using System;
using System.Collections;
using UnityEngine;

public class Cannon : TankComponent
{
	[SerializeField] private float reloadTime = 2;

	[SerializeField] private Vector3 gunPos;
	[SerializeField] private Vector3 gunRecoilPos = new Vector3(0, 0, 0.2f);

	new public bool IsReloading { get; private set; }

	public event Action OnShoot;
	


	private void Update()
	{
		Shoot();
	}

	public void Shoot()
	{
		if (!KeyboardActive || IsReloading)
			return;

		if (Input.GetAxis("Fire1") == 1)
		{
			StartCoroutine(ReloadTimer());
			StartCoroutine(GunRecoil());
			OnShoot();

			Instantiate(shell, muzzle.position, muzzle.rotation, null);

			/*Ray ray = new Ray(muzzle.position, gun.forward * 1000);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 1000.0f))
			{
				Debug.DrawRay(muzzle.position, gun.forward * 1000, Color.red, 5);
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.position = raycastHit.point;
			}*/
		}
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

	private IEnumerator GunRecoil()
	{
		float t;
		t = 0;
		while (t <= 1)
		{
			t += Time.deltaTime * 10;
			gun.localPosition = Vector3.Lerp(gunPos, gunRecoilPos, t);
			yield return null;
		}
		t = 0;
		while (t <= 1)
		{
			t += Time.deltaTime;
			gun.localPosition = Vector3.Lerp(gunRecoilPos, gunPos, t);
			yield return null;
		}

	}
}