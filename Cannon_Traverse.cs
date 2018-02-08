using UnityEngine;

public class Cannon_Traverse : TankComponent
{
	[SerializeField] private float traverseSpeed = 10.0f;
	[SerializeField] private float bufferAngle = 1.0f;
	[SerializeField] private float maxElev = 15.0f;
	[SerializeField] private float maxDep = 10.0f;

	Vector3 targetPos;
	Vector3 localTargetPos;
	float targetAng;
	float speedRate;
	float currentAng;

	private void Update()
	{
		Traverse();
	}

	public void Traverse()
	{
		if (canTrack)
		{
			targetPos = MouseControl.Point();
			localTargetPos = gun.InverseTransformPoint(targetPos);
			targetAng = Vector2.Angle(Vector2.up, new Vector2(localTargetPos.x, localTargetPos.z)) * Mathf.Sign(localTargetPos.x);
			targetAng = Mathf.Rad2Deg * (Mathf.Asin((localTargetPos.y - gun.localPosition.y) / Vector3.Distance(gun.localPosition, localTargetPos)));
		}
		else
		{
			targetAng = Mathf.DeltaAngle(currentAng, 0.0f);
		}

		if (Mathf.Abs(targetAng) > 0.01f)
		{
			// Calculate Speed Rate
			float speedRate = -Mathf.Lerp(0.0f, 1.0f, Mathf.Abs(targetAng) / (traverseSpeed * Time.fixedDeltaTime + bufferAngle)) * Mathf.Sign(targetAng);
			// Rotate
			currentAng += traverseSpeed * speedRate * Time.fixedDeltaTime;
			currentAng = Mathf.Clamp(currentAng, -maxElev, maxDep);
			gun.localRotation = Quaternion.Euler(new Vector3(currentAng, 0.0f, 0.0f));
		}

	}

}
