using System;
using System.Collections;
using UnityEngine;
namespace TinForge.TankController
{
	public class Gun_Control : TankComponent
	{
		//Consts
		private int maxRayDist = 1000;

		//Values
		[SerializeField] private float reload = 2;

		//Vectors
		[SerializeField, Space] private Vector3 originPos;
		[SerializeField] private Vector3 recoilPos = new Vector3(0, 0, 0.2f);

		//Properties
		new public bool IsReloading { get; private set; }
		public float CoolDown { get; private set; }
		public Vector3 AimPoint { get; private set; }

		//Events
		public event Action OnShoot;

		private void Update()
		{
			if (IsControlling && IsAlive)
			{
				SetAimPoint();

				if (MouseClick)
					Shoot();
			}
		}

		public void SetAimPoint()
		{
			Ray ray = new Ray(muzzle.position, muzzle.up);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, maxRayDist))
				AimPoint = Camera.main.WorldToScreenPoint(hit.point);
		}

		public void Shoot()
		{
			if (IsReloading)
				return;

			OnShoot();
			GameObject instanceShell = Instantiate(shell, muzzle.position, muzzle.rotation, null);
			instanceShell.GetComponent<Shell>().Controller = controller;

			StartCoroutine(Reload());
			if (barrel != null)
				StartCoroutine(BarrelRecoil());
		}

		private IEnumerator Reload()
		{
			IsReloading = true;
			float t = 0;
			while (t <= reload)
			{
				t += Time.deltaTime;
				CoolDown = Mathf.Clamp(1 - (t / reload), 0, 1);
				yield return null;
			}
			IsReloading = false;
		}

		private IEnumerator BarrelRecoil()
		{
			float t;
			t = 0;
			while (t <= 1)
			{
				t += Time.deltaTime * 10;
				barrel.localPosition = Vector3.Lerp(originPos, recoilPos, t);
				yield return null;
			}
			t = 0;
			while (t <= 1)
			{
				t += Time.deltaTime;
				barrel.localPosition = Vector3.Lerp(recoilPos, originPos, t);
				yield return null;
			}
		}
	}
}