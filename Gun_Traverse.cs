using System;
using UnityEngine;

namespace TinForge.TankController
{
	public class Gun_Traverse : TankComponent
	{
		//Values
		[SerializeField] private float traverseSpeed = 10.0f;
		[SerializeField] private float bufferAngle = 1.0f;
		[SerializeField] private float maxElev = 15.0f;
		[SerializeField] private float maxDep = 10.0f;

		//Private
		private Vector3 targetPos;
		private Vector3 localTargetPos;
		private float targetAng;
		private float speedRate;
		private float currentAng;

		//Events
		public event Action OnCannonTraverse;


		void Update()
		{
			if (IsControlling && IsAlive)
				Traverse();
		}

		public void Traverse()
		{
			targetPos = MousePos;
			localTargetPos = gun.InverseTransformPoint(targetPos);
			targetAng = Vector2.Angle(Vector2.up, new Vector2(localTargetPos.x, localTargetPos.z)) * Mathf.Sign(localTargetPos.x);
			targetAng = Mathf.Rad2Deg * (Mathf.Asin((localTargetPos.y - gun.localPosition.y) / Vector3.Distance(gun.localPosition, localTargetPos)));

			if (Mathf.Abs(targetAng) > 0.01f)
			{
				float speedRate = -Mathf.Lerp(0.0f, 1.0f, Mathf.Abs(targetAng) / (traverseSpeed * Time.fixedDeltaTime + bufferAngle)) * Mathf.Sign(targetAng);
				currentAng += traverseSpeed * speedRate * Time.fixedDeltaTime;
				currentAng = Mathf.Clamp(currentAng, -maxElev, maxDep);
				gun.localRotation = Quaternion.Euler(new Vector3(currentAng, 0.0f, 0.0f));
			}
		}
	}
}
