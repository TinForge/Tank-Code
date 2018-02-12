using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Health : TankComponent
{

	private int hp;

	public int HP { get { return hp; } }

	public event Action OnHit;
	public event Action OnDestroy;

	public void Hit(string player, int damage)
	{
		//if ()

	}

}