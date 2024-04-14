using UnityEngine;

public class TowerHealth : Health
{
	[SerializeField] private Tower _tower;

	private void Start()
	{
		_maxHealth = _currentHealth + PlayerPrefs.GetFloat("Saved Health");
		_currentHealth = _maxHealth;

		StartCoroutine(Regeneration());
		_currenHealthRegen = PlayerPrefs.GetFloat("Saved Health Regen");
	}

	private void OnEnable()
	{
		_tower.DamageTaken += OnDecreseHealth;
		_tower.Healed += OnIncreseHealth;
		_tower.UpgradeHealth += OnUpgradeHealth;
	}

	private void OnDestroy()
	{
		if (_tower != null)
		{
			_tower.DamageTaken -= OnDecreseHealth;
			_tower.Healed -= OnIncreseHealth;
			_tower.UpgradeHealth -= OnUpgradeHealth;
		}
	}

	private void OnDisable()
	{
		if (_tower != null)
		{
			_tower.DamageTaken -= OnDecreseHealth;
			_tower.Healed -= OnIncreseHealth;
			_tower.UpgradeHealth -= OnUpgradeHealth;
		}
	}

}
