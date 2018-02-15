using UnityEngine;

public class Tank_Particles : TankComponent {

	[SerializeField] private ParticleSystem shoot;
	[SerializeField] private ParticleSystem deathExplosion;
	[SerializeField] private ParticleSystem deathSmoke;
	[SerializeField] private ParticleSystem deathFire;


	private void Start()
	{
		cannon.OnShoot += Shoot;
		health.OnDestroy += Destroy;
	}

	private void OnDisable()
	{
		cannon.OnShoot -= Shoot;
		health.OnDestroy -= Destroy;
	}


	private void Shoot()
	{
		shoot.Play();
	}
	private void Destroy()
	{
		deathExplosion.Play();
		deathSmoke.Play();
		deathFire.Play();
	}

	public void PlayProperty (ParticleSystem particle) {
		if (!particle.isPlaying) {
			particle.Play ();
		}
	}

}