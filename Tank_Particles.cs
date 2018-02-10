using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Particles : TankComponent {
	void Start () {

	}

	void Update () {

	}

	public void PlayProperty (ParticleSystem particle) {
		if (!particle.isPlaying) {
			particle.Play ();
		}
	}

}