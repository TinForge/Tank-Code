using UnityEngine;

public class TankComponent : MonoBehaviour {

	protected TankController controller;
	protected Tank_Health health;
	protected Tank_Recoil recoil;
	new protected Tank_Audio audio;
	protected Tank_Particles particle;

	protected Hull_Traverse hull_traverse;
	protected Turret_Traverse turret_traverse;
	protected Cannon_Traverse cannon_traverse;
	protected Cannon cannon; //name is not the greatest
	protected Tracks tracks;

	protected bool IsControlling { get { return controller.IsControlling; } }
	protected bool KeyboardActive { get { return controller.KeyboardInput; } }
	protected bool MouseActive { get { return controller.MouseInput; } }
	protected bool IsAlive { get { return health.IsAlive; } }
	protected bool IsReloading { get { return cannon.IsReloading; } }
	protected bool IsGrounded { get { return tracks.IsGrounded; } }

	new protected Transform transform { get { return controller.transform; } }
	protected Transform hull { get { return controller.hull; } }
	protected Transform turret { get { return controller.turret; } }
	protected Transform mantlet { get { return controller.mantlet; } }
	protected Transform gun { get { return controller.gun; } }
	protected Transform muzzle { get { return controller.muzzle; } }
	protected Transform[] leftWheels { get { return controller.leftWheels; } }
	protected Transform[] rightWheels { get { return controller.rightWheels; } }

	public GameObject shell { get { return controller.shell; } }

	protected Rigidbody rb { get { return controller.rb; } }

	protected virtual void Awake () {
		controller = gameObject.transform.root.GetComponentInChildren<TankController> ();
		health = gameObject.transform.root.GetComponentInChildren<Tank_Health> ();
		recoil = gameObject.transform.root.GetComponentInChildren<Tank_Recoil> ();
		audio = gameObject.transform.root.GetComponentInChildren<Tank_Audio> ();
		particle = gameObject.transform.root.GetComponentInChildren<Tank_Particles> ();
		hull_traverse = gameObject.transform.root.GetComponentInChildren<Hull_Traverse> ();
		turret_traverse = gameObject.transform.root.GetComponentInChildren<Turret_Traverse> ();
		cannon_traverse = gameObject.transform.root.GetComponentInChildren<Cannon_Traverse> ();
		cannon = gameObject.transform.root.GetComponentInChildren<Cannon> ();
		tracks = gameObject.transform.root.GetComponentInChildren<Tracks> ();

		health.OnHit += Hit;
		health.OnHit += Hit;
	}

	protected virtual void Hit () {

	}

	protected virtual void Destroy () {

	}

}