using UnityEngine;

public class Turret_Traverse : TankComponent
{

	[SerializeField] private float traverseSpeed = 15.0f;
	[SerializeField] private float bufferAngle = 5.0f;
	[SerializeField] private float acceleration_Time = 0.2f;

	Vector3 targetPos;
	Vector3 localTargetPos;
	float targetAng;
	float speedRate;
	float currentTurretAng;

	private void Update()
	{
		Traverse();
	}

	public void Traverse()
	{
		if (canTrack)
		{
			targetPos = MouseControl.Point();
			localTargetPos = turret.InverseTransformPoint(targetPos);
			targetAng = Vector2.Angle(Vector2.up, new Vector2(localTargetPos.x, localTargetPos.z)) * Mathf.Sign(localTargetPos.x);
			//targetAng += deltaMouseAng.x;
		}
		else
		{
			targetAng = Mathf.DeltaAngle(currentTurretAng, 0.0f);
		}


		if (Mathf.Abs(targetAng) > 0.01f)
		{
			// Calculate Turn Rate.
			float targetSpeedRate = Mathf.Lerp(0.0f, 1.0f, Mathf.Abs(targetAng) / (traverseSpeed * Time.fixedDeltaTime + bufferAngle)) * Mathf.Sign(targetAng);
			// Calculate Rate
			speedRate = Mathf.MoveTowardsAngle(speedRate, targetSpeedRate, Time.fixedDeltaTime / acceleration_Time);
			// Rotate
			currentTurretAng += traverseSpeed * speedRate * Time.fixedDeltaTime;
			turret.localRotation = Quaternion.Euler(new Vector3(0.0f, currentTurretAng, 0.0f));
		}

	}
}
