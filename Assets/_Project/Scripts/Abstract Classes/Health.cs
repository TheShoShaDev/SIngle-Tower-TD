using System;
using UnityEngine;
using System.Collections;

abstract public class Health : MonoBehaviour
{
	public float _currentHealth { get; protected set; }
	public float _maxHealth { get; protected set; }
	public float _currenHealthRegen { get; protected set; }

	public event Action<float, float> HealthChanged;

	protected virtual void OnDecreseHealth(float value)
	{
		if (value < 0)
		{
			throw new Exception("Taken damage cannot be less than 0");
		}

		this._currentHealth -= value;
		HealthChanged?.Invoke(_currentHealth, _maxHealth);
	}

	protected void OnIncreseHealth(float value)
	{
		if (value < 0)
		{
			throw new Exception("Heal cannot be less than 0");
		}

		this._currentHealth += value;
		HealthChanged?.Invoke(_currentHealth, _maxHealth);
	}

	protected void OnUpgradeHealth(float value)
	{
		if (value < 0)
		{
			throw new Exception("Fix upgrade value");
		}

		this._currentHealth += value;
		this._maxHealth += value;

		HealthChanged?.Invoke(_currentHealth, _maxHealth);
	}

	private void OnValidate()
	{
		if (_maxHealth < _currentHealth)
		{
			_maxHealth = _currentHealth;
		}
	}

	protected IEnumerator Regeneration()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			OnIncreseHealth(_currenHealthRegen);
		}
	}
}