using UnityEngine;

public class Tank_Particles : TankComponent {

	[SerializeField] private ParticleSystem shoot;


	private void Start()
	{
		cannon.OnShoot += Shoot;
	}

	private void OnDisable()
	{
		cannon.OnShoot -= Shoot;
	}


	private void Shoot()
	{
		shoot.Play();
	}


	public void PlayProperty (ParticleSystem particle) {
		if (!particle.isPlaying) {
			particle.Play ();
		}
	}

}