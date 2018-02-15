
using UnityEngine;

public class TankController : MonoBehaviour {

	[Header ("Player")]
	public string player;
	public bool isControlling;

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

	public GameObject shell;
	public Texture2D aimReticle;

	[Header ("Components")]
	public Rigidbody rb;

	void Start () {
		MouseControl.SetCursor(aimReticle, new Vector2(16, 16));

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