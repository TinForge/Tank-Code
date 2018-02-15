// Smooth Follow from Standard Assets
// Converted to C# because I fucking hate UnityScript and it's inexistant C# interoperability
// If you have C# code and you want to edit SmoothFollow's vars ingame, use this instead.
using UnityEngine;
using System.Collections;

///Moves and rotates with player with zoom out
/// Mouse doesnt affect rotation
/// Mounts on Camera object, needs target gameObject
public class CameraFollow : MonoBehaviour
{
	#region Spectator

	[SerializeField]private float speed;
	[SerializeField]private float xSensitivity;
	[SerializeField]private float ySensitivity;

	public bool isSpectating{ get { return (false); } }

	#endregion

	#region Player

	// The target we are following
	[SerializeField]private Transform target;
	new private Camera camera;

	[SerializeField] private float lerp = 0f;
	//offset where the camera should be centered on
	[SerializeField] private float offset = 1;
	[SerializeField] private float minOffset = 3f;
	[SerializeField] private float maxOffset = 4.5f;
	// The distance in the x-z plane to the target
	[SerializeField]private float distance = 5;
	[SerializeField]private float minDistance = 5f;
	[SerializeField]private float maxDistance = 15f;
	// the height we want the camera to be above the target
	[SerializeField]private float height = 3f;
	[SerializeField] private float minHeight = 3f;
	[SerializeField] private float maxHeight = 5f;


	// How much we dampen
	[SerializeField]private float heightDamping = 2.0f;
	[SerializeField]private float rotationDamping = 3.0f;

	#endregion

	private void Awake()
	{
		camera = GetComponent<Camera>();
	}

	public Transform Target {
		set {
			target = value;
		}
	}

	void LateUpdate ()
	{
		if (!target)
			Spectator ();
		else
			FollowPlayer ();

	}

	private void Spectator ()
	{
		if (isSpectating) {
			float x = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
			float z = Input.GetAxis ("Vertical") * speed * Time.deltaTime;
			float y = 0;
			if (Input.GetKey (KeyCode.Space))
				y += 1 * speed * Time.deltaTime;
			if (Input.GetKey (KeyCode.LeftShift))
				y -= 1 * speed * Time.deltaTime;
			
			transform.Translate (new Vector3 (x, y, z));

			float X = Input.GetAxis ("Mouse X") * xSensitivity;
			float Y = -Input.GetAxis ("Mouse Y") * ySensitivity;

			transform.Rotate (new Vector3 (Y, X, 0));
			Quaternion r = transform.rotation;
			transform.rotation = Quaternion.Euler (new Vector3 (r.eulerAngles.x, r.eulerAngles.y, 0));
		}
	}

	private void FollowPlayer ()
	{
		lerp = Mathf.Clamp(lerp - Input.GetAxis("Mouse ScrollWheel"), 0, 1);
		distance = Mathf.Lerp (minDistance, maxDistance, lerp);
		height = Mathf.Lerp(minHeight, maxHeight, lerp);
		offset = Mathf.Lerp(minOffset, maxOffset, lerp);
		camera.fieldOfView = Mathf.Lerp(30, 60, lerp);


		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		// Set the height of the camera
		transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);
		// Always look at the target
		transform.LookAt (target.position + Vector3.up*offset);
	}
}