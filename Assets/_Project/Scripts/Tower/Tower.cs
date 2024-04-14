using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tower : MonoBehaviour, IAttackable, IDamagable, IHealable
{
	public event Action<float> DamageTaken;
	public event Action<float> Healed;
	public event Action<float> UpgradeHealth;

	public TowerTargetPriority _targetPriority;
	[SerializeField] private List<Enemy> _curEnemiesInRange = new List<Enemy>();
	private Enemy _curEnemy;

	[SerializeField] private TowerStats _stats;


	[Header("Attacking")]
	[SerializeField] private GameObject _projectilePrefab;
	[SerializeField] private Transform _projectileSpawnPos;

	private float _timeSinceLastAttack;

	[Header("CurInfo")]
	[SerializeField] private float _currentDamage;
	[SerializeField] private float _currentArmor;
	[SerializeField] private float _currentAttackSpeed;
	[SerializeField] private float _currenBlockDamage;

	[SerializeField] private GameObject _attackRadiusVisual;

	private void Start()
	{
		InitCurrentStats();
		InitUpgradeUI();

		CircleCollider2D AttackRange = gameObject.GetComponent<CircleCollider2D>();
		_attackRadiusVisual.transform.localScale = new Vector3(AttackRange.radius * 2, AttackRange.radius * 2, 1);
	}

	private void Update()
	{
		_timeSinceLastAttack += Time.deltaTime;

		if (_timeSinceLastAttack >= 1 / _currentAttackSpeed)
		{
			_curEnemy = GetEnemy();
			if (_curEnemy != null)
			{
				Attack();
				_timeSinceLastAttack = 0;
			}
		}
	}

	private void OnUpgradeStat(UpgradePrices type)
	{
		switch (type._upgradeType)
		{
			case TowerUpgeradeType.Damage:
				{
					_currentDamage += type._value;
					//UpdateUpgradeStat(TowerUpgeradeType.Damage);
					break;
				}
			case TowerUpgeradeType.AttackSpeed:
				{
					_currentAttackSpeed += type._value;
					//UpdateUpgradeStat(TowerUpgeradeType.AttackSpeed);
					break;
				}
			case TowerUpgeradeType.Health:
				{
					UpgradeHealth?.Invoke(type._value);
					//UpdateUpgradeStat(TowerUpgeradeType.Health);
					break;
				}
			case TowerUpgeradeType.Armor:
				{
					_currentArmor += type._value;
					//UpdateUpgradeStat(TowerUpgeradeType.Armor);
					break;
				}
			case TowerUpgeradeType.HealthRegeneration:
				{
					//_currenHealthRegen += upgradeValue;
					break;
				}
			case TowerUpgeradeType.BlockDamage:
				{
					_currenBlockDamage += type._value;
					break;
				}
			case TowerUpgeradeType.AttackRange:
				{
					CircleCollider2D AttackRange = gameObject.GetComponent<CircleCollider2D>();
					AttackRange.radius += type._value;
					_attackRadiusVisual.transform.localScale = new Vector3(AttackRange.radius * 2, AttackRange.radius * 2, 1);
					break;
				}
		}
	}

	private void InitCurrentStats()
	{
		_currentDamage = _stats.BaseDamage + PlayerPrefs.GetFloat("Saved Damage"); ;
		_currentArmor = _stats.BaseArmor + PlayerPrefs.GetFloat("Saved Armor");
		_currentAttackSpeed = _stats.BaseAttackSpeed + PlayerPrefs.GetFloat("Saved Attack Speed");
		_currenBlockDamage = _stats.BaseBlockDamage + PlayerPrefs.GetFloat("Saved Block Damage");
	}

	private void InitUpgradeUI()
	{
		foreach(TowerUpgeradeType type in Enum.GetValues(typeof(TowerUpgeradeType)))
		{
			EventBus.InitUpgradeUI(type);
		}
	}

	public void Attack() 
    {
		GameObject _proj = Instantiate(_projectilePrefab, _projectileSpawnPos.position, Quaternion.identity);
		_proj.GetComponent<Projectile>().Initialize(_curEnemy, _currentDamage);
	}

    public void GetDamage(float damage)
    {
		float value = damage - ((_currentArmor / 100) * damage) - _currenBlockDamage;
		value = (value > 0) ? value  : 0;
		DamageTaken?.Invoke(value);
    }

    public void GetHealed(float value)
    {
        Healed?.Invoke(value);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			_curEnemiesInRange.Add(collision.GetComponent<Enemy>());
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			_curEnemiesInRange.Remove(collision.GetComponent<Enemy>());
		}
	}

	private Enemy GetEnemy()
	{
		_curEnemiesInRange.RemoveAll(x => x == null);

		if (_curEnemiesInRange.Count == 0)
		{
			return null;
		}

		if (_curEnemiesInRange.Count == 1)
		{
			return _curEnemiesInRange[0];
		}

		switch (_targetPriority)
		{
			case TowerTargetPriority.First:
				{
					return _curEnemiesInRange[0];
				}
				/*
			case TowerTargetPriority.Strong:
				{
					Enemy Strongest = null;
					float StrongestHP = 0;
					foreach (Enemy enemy in _curEnemiesInRange)
					{
						if (enemy._health._currentHealth > StrongestHP)
						{
							Strongest = enemy;
							StrongestHP = enemy._health._currentHealth;
						}
					}

					return Strongest;
				}
				*/
			case TowerTargetPriority.Close:
				{
					Enemy Closest = null;
					float dist = 99f;
					for (int x = 0; x < _curEnemiesInRange.Count; x++)
					{
						float d = (transform.position - _curEnemiesInRange[x].transform.position).sqrMagnitude;

						if (d > dist)
						{
							Closest = _curEnemiesInRange[x];
							dist = d;
						}
					}

					return Closest;
				}
		}
		return null;
	}

	private void OnEnable()
	{

		EventBus.OnUpgradeStat += OnUpgradeStat;
		EventBus.OnTowerDamageTaken += GetDamage;
	}

	private void OnDisable()
	{
		EventBus.OnTowerDamageTaken -= GetDamage;
		EventBus.OnUpgradeStat -= OnUpgradeStat;
	}
}
