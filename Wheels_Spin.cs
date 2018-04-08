using UnityEngine;
namespace TinForge.TankController
{
	//Visualize wheel spin
	//Correct spin directions based on traverse controls
	public class Wheels_Spin: TankComponent
	{

		//Values
		[SerializeField] private float spinRate = 250;

		//---

		private void Start()
		{
			hull_traverse.OnHullTraverse += RotateWheels;
		}

		private void OnDisable()
		{
			hull_traverse.OnHullTraverse -= RotateWheels;
		}

		//---

		public void RotateWheels(float inputVertical, float inputHorizontal)
		{
			//Simulate stick controls
			float leftStick = Mathf.Clamp((inputVertical * -1) + (inputHorizontal * -1), -1, 1);
			float rightStick = Mathf.Clamp(inputVertical + (inputHorizontal * -1), -1, 1);

			//left side
			foreach (Transform wheel in leftTrackWheels)
				wheel.Rotate(Vector3.left * Time.deltaTime * spinRate * leftStick);
			foreach (Transform wheel in leftWheels)
				wheel.Rotate(Vector3.left * Time.deltaTime * spinRate * leftStick);
			//right side
			foreach (Transform wheel in rightTrackWheels)
				wheel.Rotate(Vector3.right * Time.deltaTime * spinRate * rightStick);
			foreach (Transform wheel in rightWheels)
				wheel.Rotate(Vector3.right * Time.deltaTime * spinRate * rightStick);
		}
	}
}
