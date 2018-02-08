using UnityEngine;

public class MouseControl : MonoBehaviour
{
	private static Vector3 previousMousePos;
	//private static Vector3 deltaMouseAng;
	private static Vector3 targetPos;

	public static Vector3 Point()
	{
		//deltaMouseAng += (Input.mousePosition - previousMousePos) * 0.03f;
		//previousMousePos = Input.mousePosition;
		Vector3 screenPos = Input.mousePosition;

		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 1000.0f))
		{
			Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
			targetPos = raycastHit.point;
		}
		else
		{
			screenPos.z = 500.0f;
			targetPos = Camera.main.ScreenToWorldPoint(screenPos);
		}
		return targetPos;
	}

}
