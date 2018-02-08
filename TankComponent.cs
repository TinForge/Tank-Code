using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankComponent : MonoBehaviour
{

	protected TankController controller;
	new protected Tank_Audio audio;
	protected Tank_Particles particle;

	protected bool grounded { get { return controller.grounded; } }
	protected bool canMove { get { return controller.canMove; } }
	protected bool canTrack { get { return controller.canTrack; } }
	protected bool canShoot { get { return controller.canShoot; } }

	new protected Transform transform { get { return controller.transform; } }
	protected Transform hull { get { return controller.hull; } }
	protected Transform turret { get { return controller.turret; } }
	protected Transform mantlet { get { return controller.mantlet; } }
	protected Transform gun { get { return controller.gun; } }
	protected Transform muzzle { get { return controller.muzzle; } }
	protected Transform[] leftWheels { get { return controller.leftWheels; } }
	protected Transform[] rightWheels { get { return controller.rightWheels; } }

	protected Rigidbody rb { get { return controller.rb; } }

	void Awake()
	{
		controller = gameObject.transform.root.GetComponent<TankController>();
		audio = gameObject.transform.root.GetComponent<Tank_Audio>();
		particle = gameObject.transform.root.GetComponent<Tank_Particles>();
	}


	protected virtual void Damage()
	{

	}

	protected virtual void Destroy()
	{

	}

}
