using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
	[SerializeField] private Enemy enemy;
	[SerializeField] private EnemyStats _stats;

	public float _extraHp;

	private void Start()
	{
		_maxHealth = _stats.BaseHp + _extraHp;
		_currentHealth = _maxHealth;
	}

	private void OnEnable()
	{
		enemy.DamageTaken += OnDecreseHealth;
		enemy.HealthIncreesed += OnHealthIncreesed;
	}

	private void OnDestroy()
	{
		enemy.DamageTaken -= OnDecreseHealth;
		enemy.HealthIncreesed -= OnHealthIncreesed;
	}

	private void OnDisable()
	{
		enemy.DamageTaken -= OnDecreseHealth;
		enemy.HealthIncreesed -= OnHealthIncreesed;
	}

	private void OnHealthIncreesed(float extraHP)
	{
		_extraHp = extraHP;
	}

	protected override void OnDecreseHealth(float value)
	{
		Vector3 _localScale = transform.localScale;
		Vector3 _newScale = transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);

		DOTween.Sequence()
			.Append(transform.DOScale(_newScale, 0.05f).SetUpdate(UpdateType.Normal, true))
			.Append(transform.DOScale(_localScale, 0.05f).SetUpdate(UpdateType.Normal, true));

		base.OnDecreseHealth(value);

		if(_currentHealth <= 0)
		{
			DOTween.KillAll();
			EventBus.IncreseBalance(enemy._value);
			Destroy(gameObject);
			Destroy(enemy);
			return;
		}
	}
}
