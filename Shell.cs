using System.Collections;
using UnityEngine;

public class Shell : MonoBehaviour
{
	public const int LifeTime = 5;
	public const int Speed = 100;

	bool collided;

	[SerializeField] private Rigidbody rb;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private ParticleSystem particleSystem;

	void Start()
	{
		StartCoroutine(Stuff());
	}

	private void FixedUpdate()
	{
		if (!collided)
			rb.velocity = transform.up * Speed;//, ForceMode.VelocityChange);
	}

	private IEnumerator Stuff()
	{
		float t = 0;
		while (t <= LifeTime || particleSystem.isPlaying ||audioSource.isPlaying)
		{
			t += Time.deltaTime;
			yield return null;
		}
		DestroyObject(gameObject);
	}

	private void Explode()
	{
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		particleSystem.Play();
		audioSource.Play();
	}


	private void OnCollisionEnter(Collision collision)
	{
		transform.position = collision.contacts[0].point;
		rb.drag = 3;
		collided = true;
		Explode();
		//if(Photon.isServer)
		//if(collision.hit == a tank)
		//SendHit (collision);
	}

	//Start position and spawn time Photon
	//Move forward
	//On hit - Destroy & special effects
	//if server client, send hit RPC on 

	public void SendHit()
	{

		//int damage = RNG();
		//! RPC call from reference HERE?
		//! Call on Health, and have it RPC it  THERE?

	}

}