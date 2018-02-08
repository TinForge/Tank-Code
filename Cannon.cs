using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : TankComponent
{

	[SerializeField] private float reload = 5;
	float shotTimeStamp = 0;

	[SerializeField] private Vector3 gunPos;
	[SerializeField] private Vector3 gunRecoilPos = new Vector3(0, 0, 0.2f);

	private void Update()
	{
		Shoot();
	}

	public void Shoot()
	{
		if (!canShoot)
			return;

		if (Input.GetAxis("Fire1") == 1)
		{
			if ((Time.timeSinceLevelLoad) > shotTimeStamp + reload)
			{
				shotTimeStamp = Time.timeSinceLevelLoad;
				StartCoroutine(GunRecoil());


				Ray ray = new Ray(muzzle.position, gun.forward * 1000);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, 1000.0f))
				{
					Debug.DrawRay(muzzle.position, gun.forward * 1000, Color.red, 5);
					GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
					sphere.transform.position = raycastHit.point;
				}
			}

			//cooldown
			//projectile
			//sound
			//particle}
		}
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
