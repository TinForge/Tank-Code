using System;
using UnityEngine;
namespace TinForge.TankController
{
	public class Turret_Traverse : TankComponent
	{
		//Values
		[SerializeField] private float traverseSpeed = 15.0f;
		[SerializeField] private float bufferAngle = 5.0f;
		[SerializeField] private float acceleration_Time = 0.2f;

		//Private
		private Vector3 targetPos;
		private Vector3 localTargetPos;
		private float targetAng;
		private float speedRate;
		private float currentAng;

		//Events
		public event Action OnTurretTraverse;

		void Update()
		{
			if (IsControlling && IsAlive)
				Traverse();
		}

		public void Traverse()
		{
			targetPos = MousePos;
			localTargetPos = turret.InverseTransformPoint(targetPos);
			targetAng = Vector2.Angle(Vector2.up, new Vector2(localTargetPos.x, localTargetPos.z)) * Mathf.Sign(localTargetPos.x);

			if (Mathf.Abs(targetAng) > 0.01f)
			{
				float targetSpeedRate = Mathf.Lerp(0.0f, 1.0f, Mathf.Abs(targetAng) / (traverseSpeed * Time.fixedDeltaTime + bufferAngle)) * Mathf.Sign(targetAng);
				speedRate = Mathf.MoveTowardsAngle(speedRate, targetSpeedRate, Time.fixedDeltaTime / acceleration_Time);
				currentAng += traverseSpeed * speedRate * Time.fixedDeltaTime;
				turret.localRotation = Quaternion.Euler(new Vector3(0.0f, currentAng, 0.0f));
			}
		}
	}
}
