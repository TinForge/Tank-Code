using UnityEngine;
namespace TinForge.TankController
{
	//Raycast hit distance to ground and simulate suspension
	//Wheels don't use rigidbodies
	public class Wheels_Suspension : TankComponent
	{
		//Values
		[SerializeField] private float extendLength = .7f;
		[SerializeField] private float wheelRadius = .3f;

		//Private
		private Vector3[] leftOrigins;
		private Vector3[] rightOrigins;

		//---

		void Start()
		{
			//populate original positions

			//left side
			leftOrigins = new Vector3[leftWheels.Length];
			for (int i = 0; i < leftWheels.Length; i++)
				leftOrigins[i] = leftWheels[i].localPosition;
			//right side
			rightOrigins = new Vector3[rightWheels.Length];
			for (int i = 0; i < rightWheels.Length; i++)
				rightOrigins[i] = rightWheels[i].localPosition;
		}

		//---

		void Update()
		{
			//Raycast and position wheels

			//left side
			for (int i = 0; i < leftWheels.Length; i++)
			{
				leftWheels[i].localPosition = leftOrigins[i];
				Ray ray = new Ray(leftWheels[i].position, transform.TransformDirection(Vector3.down));
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, extendLength))
					leftWheels[i].localPosition = leftOrigins[i] - Vector3.up * (hit.distance - wheelRadius);
				else
					leftWheels[i].localPosition = Vector3.Lerp(leftWheels[i].localPosition, leftOrigins[i] - Vector3.up * (extendLength - wheelRadius), 0.5f);
			}
			//right side
			for (int i = 0; i < rightWheels.Length; i++)
			{
				rightWheels[i].localPosition = rightOrigins[i];
				Ray ray = new Ray(rightWheels[i].position, transform.TransformDirection(Vector3.down));
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, extendLength))
					rightWheels[i].localPosition = rightOrigins[i] - Vector3.up * (hit.distance - wheelRadius);
				else
					rightWheels[i].localPosition = Vector3.Lerp(rightWheels[i].localPosition, rightOrigins[i] - Vector3.up * (extendLength - wheelRadius), 0.5f);
			}
		}
	}
}
