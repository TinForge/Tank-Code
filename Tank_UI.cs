using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank_UI : TankComponent {

	[SerializeField] private Text tankName;
	[SerializeField] private Text reloadText;
	[SerializeField] private Image reloadImage;

	[SerializeField] private Text healthText;
	[SerializeField] private Text healthImage;

	[SerializeField] private Text speedText;
	[SerializeField] private Image speedImage;

	void Awake () { }

	void Update () {

	}
}