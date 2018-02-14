using System.Collections;
using UnityEngine;

public class Shell : MonoBehaviour {

	public const int LifeTime = 5;
	public const int Speed = 100;

	public string player;

	private Rigidbody rb;

	[SerializeField] private AudioSource audioSource;
	[SerializeField] private ParticleSystem particleSystem;

	bool hit;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}

	void Start () {
		StartCoroutine (Live ());
	}

	private void FixedUpdate () {
		if (!hit)
			rb.velocity = transform.up * Speed;
	}

	private IEnumerator Live () {
		float t = 0;
		while (t <= LifeTime || particleSystem.isPlaying || audioSource.isPlaying) {
			t += Time.deltaTime;
			yield return null;
		}
		DestroyObject (gameObject);
	}

	private void Explode () {
		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<Collider> ().enabled = false;
		particleSystem.Play ();
		audioSource.Play ();
	}

	private void OnCollisionEnter (Collision collision) {
		transform.position = collision.contacts[0].point;
		rb.drag = 3; //drag could be 3 automatically.
		hit = true;
		Explode ();
		//if(Photon.isServer)
		//if(collision.hit == a tank)
		SendHit (collision.transform, transform.position);
	}

	public void SendHit (transform other, Vector3 pos) {

		int damage = RNG.Damage ();
		other.GetComponent<Tank_Health> ().Hit (player, damage, pos);
		//! RPC call from reference HERE?
		//! Call on Health, and have it RPC it THERE?

	}

}