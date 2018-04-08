using UnityEngine;
namespace TinForge.TankController
{
	//Simulate grounded via collision checks
	//Collision messages only get sent to root gameobject, so we subscribe to our controller for collision calls.
	//*Only needs one script for both tracks, doesn't need to be placed on Track object
	public class Tracks_Grounded : TankComponent
	{
		//Properties
		public bool leftGrounded { get; private set; }
		public bool rightGrounded { get; private set; }
		new public bool IsGrounded { get { return (leftGrounded || rightGrounded); } }

		//---

		private void Start()
		{
			controller.CollisionStay += CollisionStay;
			controller.CollisionExit += CollisionExit;
		}
		private void OnDisable()
		{
			controller.CollisionStay -= CollisionStay;
			controller.CollisionExit -= CollisionExit;
		}

		//---

		private void CollisionStay(Collision collision)
		{
			if (collision.contacts[0].thisCollider.name == "Left Wheels")
				leftGrounded = true;
			if (collision.contacts[0].thisCollider.name == "Right Wheels")
				rightGrounded = true;
		}

		private void CollisionExit()
		{
			leftGrounded = rightGrounded = false;
		}
	}
}