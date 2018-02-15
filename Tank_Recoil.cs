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

	[SerializeField] private float hitTiltAmount = 10;
	[SerializeField] private float hitTiltSpeedA = 5;
	[SerializeField] private float hitTiltSpeedB = 5;

	Quaternion driveTilt = Quaternion.identity;
	Quaternion shootTilt = Quaternion.identity;
	Quaternion hitTilt = Quaternion.identity;
	

	private void Start()
	{
		cannon.OnShoot += Shoot;
		hull_traverse.OnHullTraverse += Tilt;
		health.OnHit += Hit;
	}

	private void OnDisable()
	{
		cannon.OnShoot -= Shoot;
		hull_traverse.OnHullTraverse -= Tilt;
		health.OnHit -= Hit;
	}

	private void Update()
	{
		Debug.DrawRay(-turret.right, -turret.right * 10);
		hull.localRotation = Quaternion.Lerp(driveTilt, shootTilt, lerpRate);
		hull.localRotation = Quaternion.Lerp(hull.localRotation, hitTilt, lerpRate);
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

	public void Hit(Vector3 direction)
	{
		StartCoroutine(HitThread(direction));
	}

	public IEnumerator HitThread(Vector3 direction)
	{
		float t;
		t = 0;
		hitTilt = Quaternion.identity;
		while (t <= 1)
		{
			hitTilt = Quaternion.AngleAxis(Mathf.Sin(t * Mathf.PI * 0.5f) * hitTiltAmount, hull.InverseTransformDirection(direction));
			t += Time.deltaTime * hitTiltSpeedA;
			yield return null;
		}
		t = 0;
		while (t <= 1)
		{
			hitTilt = Quaternion.AngleAxis(hitTiltAmount - (Mathf.Sin(t * Mathf.PI * 0.5f) * hitTiltAmount), hull.InverseTransformDirection(direction));
			t += Time.deltaTime * hitTiltSpeedB;
			yield return null;
		}
		hitTilt = Quaternion.identity;
	}

}
