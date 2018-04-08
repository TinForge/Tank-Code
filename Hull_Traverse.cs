using System;
using UnityEngine;
namespace TinForge.TankController
{
	public class Hull_Traverse : TankComponent
	{
		//Values
		[SerializeField] private float driveSpeed = 5;
		[SerializeField] private float turnSpeed = 25;

		[SerializeField, Range(0.1f, 1)] private float driveResponse = 0.3f;
		[SerializeField, Range(0.1f, 1)] private float turnResponse = 0.5f;

		[SerializeField, Range(0.1f, 1)] private float driveDropoff = 0.1f;
		[SerializeField, Range(0.1f, 1)] private float turnDropoff = 0.2f;

		//Private
		public float lastVertical = 0;
		public float lastHorizontal = 0;

		//Event
		public event Action<float, float> OnHullTraverse;

		//---

		private void FixedUpdate()
		{
			if (IsControlling && IsAlive)
				Traverse();
		}

		//---

		public void Traverse()
		{
			float vertical;
			float horizontal;

			if (IsGrounded) //Apply driving controls
			{
				vertical = Mathf.Lerp(lastVertical, InputVertical, driveResponse);
				horizontal = Mathf.Lerp(lastHorizontal, InputHorizontal, turnResponse);
			}
			else //Drop off driving inputs
			{
				vertical = Mathf.MoveTowards(lastVertical, 0, driveDropoff);
				horizontal = Mathf.MoveTowards(lastHorizontal, 0, turnDropoff);
			}

			lastVertical = Mathf.Clamp(Mathf.Lerp(lastVertical, vertical, 0.1f), -1, 1);
			lastHorizontal = Mathf.Clamp(Mathf.Lerp(lastHorizontal, horizontal, 0.1f), -1, 1);
			

			//lastVertical = Mathf.Clamp(Mathf.Lerp(lastVertical, vertical, 0.1f), -1, 1);
			//lastHorizontal = Mathf.Clamp(Mathf.Lerp(lastHorizontal, horizontal, 0.1f), -1, 1);

			rb.MovePosition(transform.root.position + transform.forward * vertical * Time.deltaTime * driveSpeed);
			rb.MoveRotation(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + (vertical >= -0.25f ? 1 : -1) * horizontal * Time.deltaTime * turnSpeed, transform.eulerAngles.z));

			OnHullTraverse(vertical, horizontal);
		}
	}


}