using UnityEngine;
namespace TinForge.TankController
{
	public class TankComponent : MonoBehaviour
	{
		//Scripts
		protected TankController controller;
		protected Health health;
		protected Hull_Traverse hull_traverse;
		protected Turret_Traverse turret_traverse;
		protected Gun_Traverse gun_traverse;
		protected Gun_Control gun_control;
		protected Tracks_Grounded tracks_grounded;
		//Flag Properties
		protected bool IsPlayer { get { return controller.isPlayer; } }
		protected bool IsControlling { get { return controller.isControlling; } }
		protected bool IsAlive { get { return health.IsAlive; } }
		protected bool IsReloading { get { return gun_control.IsReloading; } }
		protected bool IsGrounded { get { return tracks_grounded.IsGrounded; } }
		//Input Properties
		protected float InputVertical { get { return controller.inputVertical; } }
		protected float InputHorizontal { get { return controller.inputHorizontal; } }
		protected bool MouseClick { get { return controller.mouseClick; } }
		protected Vector3 MousePos { get { return controller.mousePos; } }
		//Transforms
		new protected Transform transform { get { return controller.transform; } }
		protected Transform hull { get { return controller.hull; } }
		protected Transform turret { get { return controller.turret; } }
		protected Transform gun { get { return controller.gun; } }
		protected Transform barrel { get { return controller.barrel; } }
		protected Transform muzzle { get { return controller.muzzle; } }
		protected Transform[] leftTrackWheels { get { return controller.leftTrackWheels; } }
		protected Transform[] rightTrackWheels { get { return controller.rightTrackWheels; } }
		protected Transform[] leftWheels { get { return controller.leftWheels; } }
		protected Transform[] rightWheels { get { return controller.rightWheels; } }
		//Assets
		public GameObject shell { get { return controller.shell; } }
		//Components
		protected Rigidbody rb { get { return controller.rb; } }

		protected virtual void Awake()
		{
			controller = gameObject.transform.root.GetComponentInChildren<TankController>();
			health = gameObject.transform.root.GetComponentInChildren<Health>();
			hull_traverse = gameObject.transform.root.GetComponentInChildren<Hull_Traverse>();
			turret_traverse = gameObject.transform.root.GetComponentInChildren<Turret_Traverse>();
			gun_traverse = gameObject.transform.root.GetComponentInChildren<Gun_Traverse>();
			gun_control = gameObject.transform.root.GetComponentInChildren<Gun_Control>();
			tracks_grounded = gameObject.transform.root.GetComponentInChildren<Tracks_Grounded>();
		}

	}
}