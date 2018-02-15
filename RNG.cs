using UnityEngine;

public class RNG : MonoBehaviour {

	private const int damageBase = 25;
	private const float damageRange = 0.25f;

	public static int Damage () {
		float d = damageBase;
		d *= Random.Range(1 - damageRange, 1 + damageRange);
		//return 1;
		return (int) d;
	}

}