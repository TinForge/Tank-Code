using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : TankComponent {
	//Start position and spawn time Photon
	//Move forward
	//On hit - Destroy & special effects
	//if server client, send hit RPC on 

	void Start () {

	}

	void Update () {

	}

	public void private void OnCollisionEnter (Collision other) {
		//if(Photon.isServer)
		//if(collision.hit == a tank)
		//SendHit (collision);
	}

	public void SendHit () {

		//int damage = RNG();
		//! RPC call from reference HERE?
		//! Call on Health, and have it RPC it  THERE?

	}

}