using System;
using UnityEngine;
namespace TinForge.TankController
{
	//Goes on root transform of tank
	public class TankController : MonoBehaviour
	{
		[Header("Player")]
		new public string name;
		public bool isPlayer;
		public bool isControlling;

		[Header("Transforms")]
		public Transform hull;
		[Tooltip("Y Axis movement")] public Transform turret;
		[Tooltip("X Axis movement")] public Transform gun;
		[Tooltip("Gun recoil")] public Transform barrel;
		[Tooltip("End of Barrel")] public Transform muzzle;
		public Transform[] leftTrackWheels = new Transform[2];
		public Transform[] rightTrackWheels = new Transform[2];
		public Transform[] leftWheels = new Transform[6];
		public Transform[] rightWheels = new Transform[6];

		[Header("Input")]
		[HideInInspector] public float inputVertical;
		[HideInInspector] public float inputHorizontal;
		[HideInInspector] public Vector3 mousePos;
		[HideInInspector] public bool mouseClick;

		[Header("Assets")]
		public GameObject shell;
		public Texture2D aimReticle;

		[Header("Components")]
		[HideInInspector] public Rigidbody rb;


		//---

		void Start()
		{
			MouseControl.SetCursor(aimReticle, new Vector2(16, 16));
			rb = gameObject.transform.root.GetComponentInChildren<Rigidbody>();
		}

		private void Update()
		{
			if (isPlayer && isControlling)
				GetInput();
		}


		//---

		private void GetInput()
		{
			inputVertical = Input.GetAxis("Vertical");
			inputHorizontal = Input.GetAxis("Horizontal");
			mouseClick = Input.GetAxis("Fire1") == 1 ? true : false;
			mousePos = MouseControl.Point();
		}

		#region Collision

		//Events *Since collisions get called to root only
		public event Action<Collision> CollisionStay;
		public event Action CollisionExit;

		private void OnCollisionStay(Collision collision)
		{
			if (CollisionStay != null)
				CollisionStay(collision);
		}

		private void OnCollisionExit()
		{
			if (CollisionExit != null)
				CollisionExit();
		}
		#endregion
	}
}