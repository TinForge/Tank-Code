using System;
using UnityEngine;

public class Hull_Traverse : TankComponent
{
	[SerializeField] private float driveSpeed = 5;
	[SerializeField] private float turnSpeed = 25;

	public bool Driving { get { return (verticalDrive != 0 || horizontalDrive != 0); } }

	public event Action<float,float> OnHullTraverse;

	float verticalDrive;
	float horizontalDrive;

	private void Update()
	{
		//if(PhotonView.isMine)
		Traverse();
		//else
		//InterpolateMovements()
	}

	public void Traverse()
	{
		if (!KeyboardActive || !IsGrounded)
			return;

		float inputVertical = Input.GetAxis("Vertical");
		float inputHorizontal = Input.GetAxis("Horizontal");

		verticalDrive = Mathf.Clamp(Mathf.Lerp(verticalDrive, inputVertical, 0.1f), -1, 1);
		horizontalDrive = Mathf.Clamp(Mathf.Lerp(horizontalDrive, inputHorizontal, 0.1f), -1, 1);

		rb.MovePosition(transform.root.position + transform.forward * inputVertical * Time.deltaTime * driveSpeed);
		rb.MoveRotation(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + (inputVertical >= 0 ? 1 : -1) * inputHorizontal * Time.deltaTime * turnSpeed, transform.eulerAngles.z));

		OnHullTraverse(inputVertical, inputHorizontal);
	}



}