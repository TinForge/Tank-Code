using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull_Traverse : TankComponent
{
	[SerializeField] private float driveSpeed = 5;
	[SerializeField] private float turnSpeed = 25;
	[SerializeField] private float wheelRate = 50;
	[SerializeField] private float tiltAmount = 5;

	float verticalDrive;
	float horizontalDrive;


	private void Update()
	{
		Traverse();
	}

	public void SmoothInputs()
	{
		float inputVertical = Input.GetAxis("Vertical");
		float inputHorizontal = Input.GetAxis("Horizontal");

		verticalDrive = Mathf.Clamp(Mathf.Lerp(verticalDrive, inputVertical, 0.1f), -1, 1);
		horizontalDrive = Mathf.Clamp(Mathf.Lerp(horizontalDrive, inputHorizontal, 0.1f), -1, 1);
	}

	public void Traverse()
	{
		if (!canMove || !grounded)
			return;

		SmoothInputs();

		float inputVertical = verticalDrive;
		float inputHorizontal = horizontalDrive;

		//transform.Translate(Vector3.forward * inputVertical * Time.deltaTime * driveSpeed);
		//transform.Rotate(Vector3.up * (inputVertical >= 0 ? 1 : -1) * inputHorizontal * Time.deltaTime * turnSpeed);

		//Vector3 targetPos = transform.root.TransformPoint(transform.root.forward);

		rb.MovePosition(transform.root.position + transform.forward * inputVertical * Time.deltaTime * driveSpeed);
		rb.MoveRotation(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + (inputVertical >= 0 ? 1 : -1) * inputHorizontal * Time.deltaTime * turnSpeed, transform.eulerAngles.z));


		RotateWheels(inputVertical, inputHorizontal);
		Tilt(inputVertical, inputHorizontal);
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

	public void Tilt(float inputVertical, float inputHorizontal)
	{
		hull.localRotation = Quaternion.Euler(inputVertical * (Mathf.PingPong(hull.localRotation.eulerAngles.x, tiltAmount * 2) - tiltAmount), 0, inputHorizontal * (Mathf.PingPong(hull.localRotation.eulerAngles.z, tiltAmount * 2) - tiltAmount));
	}
}
