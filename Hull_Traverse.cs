using System;
using UnityEngine;

public class Hull_Traverse : TankComponent
{
	[SerializeField] private float driveSpeed = 5;
	[SerializeField] private float turnSpeed = 25;

	public bool Driving { get { return (verticalDrive != 0 || horizontalDrive != 0); } }

	public event Action<float,float> OnHullTraverse;

	public float verticalDrive=0;
	public float horizontalDrive =0;

	private void Update()
	{
		//if(PhotonView.isMine)
		if (IsControlling && IsAlive && KeyboardActive)
			Traverse();
		//else
		//InterpolateMovements()
	}

	public void Traverse()
	{
		if ( IsGrounded)
		{

			float inputVertical = Mathf.MoveTowards(verticalDrive, Input.GetAxis("Vertical"), 0.3f);
			float inputHorizontal = Mathf.MoveTowards(horizontalDrive, Input.GetAxis("Horizontal"), 0.5f);

			verticalDrive = Mathf.Clamp(Mathf.Lerp(verticalDrive, inputVertical, 0.1f), -1, 1);
			horizontalDrive = Mathf.Clamp(Mathf.Lerp(horizontalDrive, inputHorizontal, 0.1f), -1, 1);

			rb.MovePosition(transform.root.position + transform.forward * inputVertical * Time.deltaTime * driveSpeed);
			rb.MoveRotation(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + (inputVertical >= 0 ? 1 : -1) * inputHorizontal * Time.deltaTime * turnSpeed, transform.eulerAngles.z));

			OnHullTraverse(inputVertical, inputHorizontal);
		}
	}



}