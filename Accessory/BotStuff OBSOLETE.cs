using UnityEngine;
using Pathfinding;
namespace TinForge.TankController
{
	public class BotController : TankComponent
	{
		//Bot
		[Header("Bot")]
		public static Transform botTarget;
		public Seeker seeker;
		private Path path;

		private int nextWaypointDistance = 3;
		private int currentWaypoint;

		private int repathInterval = 3;
		private float repathTimestamp = float.NegativeInfinity;

		//Properties
		new public float InputVertical { get; private set; }
		new public float InputHorizontal { get; private set; }
		new public Vector3 MousePos { get; private set; }
		new public bool MouseClick { get; private set; }

		//---

		protected override void Awake()
		{
			base.Awake();
			if (IsPlayer)
				SetBotTarget(transform);
		}

		public static void SetBotTarget(Transform target)
		{
			botTarget = target;
		}

		private void Update()
		{
			if (IsControlling && IsAlive)
			{
				if (IsPlayer)
					PlayerInput();
				else
					BotInput();
			}
		}

		//---

		private void PlayerInput()
		{
			InputVertical = Input.GetAxis("Vertical");
			InputHorizontal = Input.GetAxis("Horizontal");
			MouseClick = Input.GetAxis("Fire1") == 1 ? true : false;
			MousePos = MouseControl.Point();
		}

		//---

		private void BotInput()
		{
			//Hull Movement
			if (repathTimestamp + repathInterval < Time.time && seeker.IsDone())
			{
				repathTimestamp = Time.time;
				seeker.StartPath(transform.position, botTarget.position, OnPathComplete);
			}

			if (path == null)
				return;

			if (currentWaypoint > path.vectorPath.Count)
				return;
			if (currentWaypoint == path.vectorPath.Count)
			{
				currentWaypoint++;
				return;
			}

			Vector3 targetPos = path.vectorPath[currentWaypoint];
			Vector3 localTargetPos = transform.InverseTransformPoint(targetPos);
			float targetAng = Vector2.Angle(Vector2.up, new Vector2(localTargetPos.x, localTargetPos.z)) * Mathf.Sign(localTargetPos.x);

			if (Mathf.Abs(targetAng) > 5f)
			{
				float targetSpeedRate = Mathf.Lerp(0.0f, 1.0f, Mathf.Abs(targetAng) / (25/*traverseSpeed*/ * Time.fixedDeltaTime + 1/*bufferAngle*/)) * Mathf.Sign(targetAng);
				//speedRate = Mathf.MoveTowardsAngle(speedRate, targetSpeedRate, Time.fixedDeltaTime / acceleration_Time);
				//currentAng += traverseSpeed * speedRate * Time.fixedDeltaTime;
				InputHorizontal = Mathf.MoveTowards(InputHorizontal, targetSpeedRate, 0.5f);
				InputVertical = Mathf.MoveTowards(InputVertical, 0, 0.1f);
			}
			else
			{
				InputHorizontal = Mathf.MoveTowards(InputHorizontal, 0, 0.5f);
				InputVertical = Mathf.MoveTowards(InputVertical, 1, 0.5f);
			}

			if ((transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance * nextWaypointDistance)
			{
				currentWaypoint++;
			}

			//Turret Tracking
			MousePos = botTarget.position;

			//Mouse Click / Fire at Player
			Ray ray = new Ray(muzzle.position, muzzle.up);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 500))
			{
				if (raycastHit.transform.root == botTarget)
					MouseClick = true;
				else
					MouseClick = false;
			}
		}
		public void OnPathComplete(Path p)
		{
			path = p;
			currentWaypoint = 0;
		}

	}
}
