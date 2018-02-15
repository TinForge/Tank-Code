using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tank_UI : TankComponent {

	[SerializeField] private Text tankName;
	
	[SerializeField] private Image reloadFill;

	[SerializeField] private Image healthFill;

	[SerializeField] private Image engineFill;

	[SerializeField] private Image hitImage;
	[SerializeField] private Image deadImage;

	private void Start()
	{
		health.OnHit += Hit;
		health.OnDestroy += Destroy;
	}

	private void OnDisable()
	{
		health.OnHit -= Hit;
		health.OnDestroy -= Destroy;
	}

	void Update () {

		reloadFill.fillAmount = cannon.CoolDown;
		healthFill.fillAmount = (float) health.HP / Tank_Health.MaxHealth;
		engineFill.fillAmount = Mathf.Clamp(Mathf.Abs(hull_traverse.verticalDrive * hull_traverse.verticalDrive) + Mathf.Abs(hull_traverse.horizontalDrive * hull_traverse.horizontalDrive), 0, 1);
	}

	void Hit(Vector3 redundant)
	{
		StartCoroutine(HitThread());
	}

	private IEnumerator HitThread()
	{
		float t;
		t = 0;
		Color color = hitImage.color;
		while (t <= 1)
		{
			color.a =t*t;
			hitImage.color = color;
			t += Time.deltaTime*10;
			yield return null;
		}
		t = 0;
		while (t <= 1)
		{
			color.a = 1-t;
			hitImage.color = color;
			t += Time.deltaTime * 2;
			yield return null;
		}
	}

	private void Destroy()
	{
		deadImage.enabled = true;
	}


}