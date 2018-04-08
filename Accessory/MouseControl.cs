using UnityEngine;

public class MouseControl : MonoBehaviour
{
	private static Vector3 previousMousePos;
	private static Vector3 targetPos;

	public static Vector3 Point()
	{
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


	public static void SetCursor(Texture2D reticle, Vector2 center)
	{
		Cursor.SetCursor(reticle, center, CursorMode.ForceSoftware);
	}
}
