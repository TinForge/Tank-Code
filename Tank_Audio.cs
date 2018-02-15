using UnityEngine;

public class Tank_Audio : TankComponent
{
	AudioSource source;

	[SerializeField] private AudioSource hullTraverse;
	[SerializeField] private AudioClip turretTraverse;
	[SerializeField] private AudioClip cannonTraverse;
	[SerializeField] private AudioClip shoot;
	[SerializeField] private AudioClip hit;
	[SerializeField] private AudioClip dead;

	float engineRate;

	protected override void Awake()
	{
		base.Awake();
		source = GetComponent<AudioSource>();
	}

	private void Start()
	{
		hull_traverse.OnHullTraverse += HullTraverse;
		//turret_traverse.OnTurretTraverse += TurretTraverse;
		//cannon_traverse.OnCannonTraverse += CannonTraverse;
		cannon.OnShoot += Shoot;
		health.OnHit += Hit;
		health.OnDestroy+= Destroy;
	}

	private void OnDisable()
	{
		hull_traverse.OnHullTraverse -= HullTraverse;
		turret_traverse.OnTurretTraverse -= TurretTraverse;
		cannon_traverse.OnCannonTraverse -= CannonTraverse;
		cannon.OnShoot -= Shoot;
		health.OnDestroy -= Destroy;
	}

	private void HullTraverse(float inputVertical,float inputHorizontal)
	{
		float mag = Mathf.Abs(inputVertical*0.75f) + Mathf.Abs(inputHorizontal * 0.75f);
		engineRate = Mathf.MoveTowards(engineRate, mag, 0.03f);
		hullTraverse.pitch = Mathf.Lerp(.9f, 1.25f, engineRate);
		hullTraverse.volume = Mathf.Lerp(.15f, .4f, engineRate);
	}
	private void TurretTraverse()
	{

	}
	private void CannonTraverse()
	{

	}

	private void Shoot()
	{
		source.PlayOneShot(shoot);
	}

	private void Hit(Vector3 redundant)
	{
		source.PlayOneShot(hit);
	}

	private void Destroy()
	{
		source.PlayOneShot(dead);
	}

	public void Play(AudioClip clip)
	{
		
		source.PlayOneShot(clip);
	}

	public void Mute(bool mute)
	{
		source.mute = mute;
	}
}