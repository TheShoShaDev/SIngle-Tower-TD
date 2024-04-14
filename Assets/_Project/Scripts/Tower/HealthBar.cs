using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthBar : HealthBar
{
	[SerializeField] private TowerHealth health;

	private void OnEnable()
	{
		health.HealthChanged += DrawHealth;
	}

	private void OnDisable()
	{
		health.HealthChanged -= DrawHealth;
	}

	private void OnDestroy()
	{
		health.HealthChanged -= DrawHealth;
	}
}
