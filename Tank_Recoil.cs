using System.Collections;
using UnityEngine;

public class Tank_Recoil : TankComponent
{
	[SerializeField] private float lerpRate = 0.5f;

	[SerializeField] private float driveTiltAmount = 8;
	[SerializeField] private float driveTiltSpeed = 1;

	[SerializeField] private float shootTiltAmount = 8;
	[SerializeField] private float shootTiltSpeedA = 5;
	[SerializeField] private float shootTiltSpeedB = 3;


	Quaternion driveTilt = Quaternion.identity;
	Quaternion shootTilt = Quaternion.identity;



	private void Start()
	{
		cannon.OnShoot += Shoot;
		hull_traverse.OnHullTraverse += Tilt;
	}

	private void OnDisable()
	{
		cannon.OnShoot -= Shoot;
		hull_traverse.OnHullTraverse -= Tilt;
	}

	private void Update()
	{
		Debug.DrawRay(-turret.right, -turret.right * 10);
		hull.localRotation = Quaternion.Lerp(driveTilt, shootTilt, lerpRate);
	}

	public void Tilt(float inputVertical, float inputHorizontal)
	{
			driveTilt = Quaternion.Euler(inputVertical * (Mathf.PingPong(Time.time * driveTiltSpeed, driveTiltAmount ) - driveTiltAmount), 0, inputHorizontal * (Mathf.PingPong(Time.time * driveTiltSpeed, driveTiltAmount) - driveTiltAmount));
	}

	public void Shoot()
	{
		StartCoroutine(ShootThread());
	}

	public IEnumerator ShootThread()
	{
		float t;
		t = 0;
		shootTilt = Quaternion.identity;
		while (t <= 1)
		{
			shootTilt = Quaternion.AngleAxis( Mathf.Sin(t * Mathf.PI * 0.5f) * shootTiltAmount, hull.InverseTransformDirection (-turret.right));
			t += Time.deltaTime * shootTiltSpeedA;
			yield return null;
		}
		t = 0;
		while (t <= 1)
		{
			shootTilt = Quaternion.AngleAxis(shootTiltAmount - (t * t * shootTiltAmount), hull.InverseTransformDirection(-turret.right));
			t += Time.deltaTime * shootTiltSpeedB;
			yield return null;
		}
		shootTilt = Quaternion.identity;
	}

}
