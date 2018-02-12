using UnityEngine;

public class Tracks : TankComponent
{

	public bool leftGrounded;
	public bool rightGrounded;

	public bool IsGrounded { get { return (leftGrounded || rightGrounded); } }

	private void OnCollisionStay(Collision collision)
	{
		leftGrounded = rightGrounded = false;

		if (collision.contacts[0].thisCollider.name == "Left Wheels")
			leftGrounded = true;
		if (collision.contacts[0].thisCollider.name == "Right Wheels")
			rightGrounded = true;
	}

	private void OnCollisionExit()
	{
		leftGrounded = rightGrounded = false;
	}

}