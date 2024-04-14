using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttackable, IDamagable
{
	[SerializeField] private EnemyStats _stats;

	public event Action<float> DamageTaken;

	public event Action<float> HealthIncreesed;

	public bool _canAttack = false;

	[Header("Info")]
	[SerializeField] private float _currentArmor = 0f;
	[SerializeField] private float _currentDamage = 1f;
	[SerializeField] private float _extraDamage = 0f;
	[SerializeField] private float _currentAttackSpeed = 3f;
	[SerializeField] private float _lastAttackTime;
	[SerializeField] public int _value { get; private set; }

	void Start()
    {
		_currentArmor = _stats.BaseArmor;
		_currentDamage = _stats.BaseDamage + _extraDamage;
		_currentAttackSpeed = _stats.BaseAttackSpeed;
		_value = _stats.Value;
	}

    void Update()
    {
		_lastAttackTime += Time.deltaTime;
		if (_lastAttackTime >= 1 / _currentAttackSpeed
			&& _canAttack)
		{
			_lastAttackTime = 0;

			if (gameObject != null)
			{
				Attack();
			}
		}
    }

    public void Attack()
    {
		EventBus.TowerDamageTaken(_currentDamage);
	}

    public void GetDamage(float value)
    {
		float takenDamage = value - ((_currentArmor / 100) * value);
		DamageTaken?.Invoke(takenDamage);
    }

	public void IncreseStats(float extDamage, float extHP)
	{
		HealthIncreesed?.Invoke(extHP);
		_extraDamage = extDamage;
	}

}
