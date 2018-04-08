using System.Collections;
using UnityEngine;
using TinForge.TankController;

public class Shell : MonoBehaviour
{
	//Statics
	private static int LifeTime = 5;

	//Values
	[SerializeField, Tooltip("Can shell ricochet?")] private bool ricochet;
	[SerializeField, Range(10, 100)] private int damage = 10;
	[SerializeField, Range(100, 1000)] private int velocity = 250;
	[SerializeField, Range(1, 25)] private int weight = 10;
	[SerializeField, Range(0, 1)] private float accuracyDeviation = 0.2f;
	[SerializeField, Range(0, 1)] private float damageDeviation = 0.5f;

	//Components
	private MeshRenderer mr;
	private Rigidbody rb;
	private Collider c;
	private AudioSource a;
	private ParticleSystem p;
	private Light l;

	//Properties
	public TankController Controller { get; set; } //Set by Tank_Cannon on instantiation
	public string Player { get { return Controller.name; } }

	//Private
	bool hit = false;

	//---

	private void Awake()
	{
		mr = GetComponent<MeshRenderer>();
		rb = GetComponent<Rigidbody>();
		c = GetComponent<Collider>();
		a = GetComponentInChildren<AudioSource>();
		p = GetComponentInChildren<ParticleSystem>();
		l = GetComponentInChildren<Light>();
	}

	void Start()
	{
		rb.mass = weight;
		rb.drag = velocity / 20; //affects particles after shell hits
		l.intensity = velocity / 250;
		l.range = velocity / 50;

		StartCoroutine(Cleanup());
	}

	//---

	private void FixedUpdate()
	{
		if (!hit)
			rb.velocity = transform.up * velocity;
	}

	//---

	private IEnumerator Cleanup()
	{
		float t = 0;
		while (t <= LifeTime || a.isPlaying || p.isPlaying)
		{
			t += Time.deltaTime;
			yield return null;
		}
		DestroyObject(gameObject);
	}

	//---

	private void OnCollisionEnter(Collision collision)
	{
		transform.position = collision.contacts[0].point; //Move to collision point
		rb.isKinematic = !ricochet;
		hit = true;
		Explode();

		SendHit(collision);
	}

	private void Explode()
	{
		mr.enabled = false;
		c.enabled = false;
		l.enabled = false;
		a.Play();
		p.Play();
	}

	public void SendHit(Collision collision)
	{
		IDestroyable d1 = collision.transform.root.GetComponentInChildren<IDestroyable>();
		if (d1 != null)
			d1.Destroy();

		IDamageable d2 = collision.transform.root.GetComponentInChildren<IDamageable>();
		if (d2 != null)
		{
			int damage = RNG.Damage(this.damage, damageDeviation);
			d2.Damage(Controller, damage, transform.position);
		}
		//! RPC call from reference HERE?
		//! Call on Health, and have it RPC it THERE?

	}

}