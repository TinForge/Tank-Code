using UnityEngine;

public class Wheels : TankComponent {

	[SerializeField] private float wheelRate = 250;

	private void Start()
	{
		hull_traverse.OnHullTraverse += RotateWheels;
	}

	private void OnDisable()
	{
		hull_traverse.OnHullTraverse -= RotateWheels;
	}

	public void RotateWheels(float inputVertical, float inputHorizontal)
	{
		float leftStick = Mathf.Clamp((inputVertical * -1) + (inputHorizontal * -1), -1, 1);

		float rightStick = Mathf.Clamp(inputVertical + (inputHorizontal * -1), -1, 1);

		foreach (Transform wheel in leftWheels)
			wheel.Rotate(Vector3.left * Time.deltaTime * wheelRate * leftStick);
		foreach (Transform wheel in rightWheels)
			wheel.Rotate(Vector3.right * Time.deltaTime * wheelRate * rightStick);
	}
}
