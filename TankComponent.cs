using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankComponent : MonoBehaviour {

	protected TankController controller;
	protected Tank_Health health;
	new protected Tank_Audio audio;
	protected Tank_Particles particle;

	protected Hull_Traverse hull_traverse;
	protected Turret_Traverse turret_traverse;
	protected Cannon_Traverse cannon_traverse;
	protected Tracks tracks;
	protected Cannon cannon; //name is not the greatest

	protected bool KeyboardActive { get { return controller.KeyboardActive; } }
	protected bool MouseActive { get { return controller.MouseActive; } }
	protected bool IsGrounded { get { return tracks.IsGrounded; } }
	protected bool IsReloading { get { return cannon.IsReloading; } }

	new protected Transform transform { get { return controller.transform; } }
	protected Transform hull { get { return controller.hull; } }
	protected Transform turret { get { return controller.turret; } }
	protected Transform mantlet { get { return controller.mantlet; } }
	protected Transform gun { get { return controller.gun; } }
	protected Transform muzzle { get { return controller.muzzle; } }
	protected Transform[] leftWheels { get { return controller.leftWheels; } }
	protected Transform[] rightWheels { get { return controller.rightWheels; } }

	protected Rigidbody rb { get { return controller.rb; } }

	void Awake () {
		cannon = GetComponent<Cannon> ();
		hull = GetComponent<Hull_Traverse> ();
		turret = GetComponent<Turret_Traverse> ();
		tracks = GetComponent<Tracks> ();

		controller = gameObject.transform.root.GetComponent<TankController> ();
		health = gameObject.transform.root.GetComponent<Tank_Health> ();
		audio = gameObject.transform.root.GetComponent<Tank_Audio> ();
		particle = gameObject.transform.root.GetComponent<Tank_Particles> ();

		health.OnHit += Hit;
		health.OnHit += Hit;
	}

	protected virtual void Hit () {

	}

	protected virtual void Destroy () {

	}

}