using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {
	[Header ("Flags")]
	public bool KeyboardInput = true; //Keyboard input active?
	public bool MouseInput = true; //Mouse input active?

	[Header ("Transforms")]
	public Transform hull;
	public Transform turret;
	public Transform mantlet;
	public Transform gun;
	public Transform muzzle;
	public Transform[] leftWheels = new Transform[8];
	public Transform[] rightWheels = new Transform[8];

	[Header ("Components")]
	public Rigidbody rb;

	void Start () {
		leftWheels = FindWheels (transform.Find ("Left Wheels"));
		rightWheels = FindWheels (transform.Find ("Right Wheels"));
	}

	private Transform[] FindWheels (Transform parent) {
		Transform[] wheels = new Transform[parent.childCount];

		for (int i = 0; i < parent.childCount; i++)
			wheels[i] = parent.GetChild (i);

		return wheels;
	}

	//OnHit
	//
}