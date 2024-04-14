using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{
	public event Action<TowerUpgeradeType, float> UpgradeStat;

	private static readonly List<UpgradePrices> upgradePrices = new List<UpgradePrices>();

	private void Start()
	{
		InitUpgradePrices();
	}

	public void UpgradeTower(TowerUpgeradeType upgeradeType)
	{
		UpgradePrices findedTowerUpgrade = upgradePrices.Find(x => x._upgradeType == upgeradeType);

		if (!Wallet.IsEnoughtMoney(findedTowerUpgrade._price))
		{
			return;
		}

		EventBus.DecreseBalance(findedTowerUpgrade._price);

		findedTowerUpgrade.IncresePrice(findedTowerUpgrade._price);

		EventBus.UpgradeStat(findedTowerUpgrade);

	}

	private static void InitUpgradePrices()
	{
		UpgradePrices DamageUpgrade = new UpgradePrices(TowerUpgeradeType.Damage, 10, 10);
		upgradePrices.Add(DamageUpgrade);

		UpgradePrices AttackSpeedUpgrade = new UpgradePrices(TowerUpgeradeType.AttackSpeed, 10, 10);
		upgradePrices.Add(AttackSpeedUpgrade);

		UpgradePrices HealthUpgrade = new UpgradePrices(TowerUpgeradeType.Health, 10, 10);
		upgradePrices.Add(HealthUpgrade);

		UpgradePrices ArmorUpgrade = new UpgradePrices(TowerUpgeradeType.Armor, 10, 10);
		upgradePrices.Add(ArmorUpgrade);

		UpgradePrices ProjectileSpeedUpgrade = new UpgradePrices(TowerUpgeradeType.ProjectileSpeed, 10, 10);
		upgradePrices.Add(ProjectileSpeedUpgrade);

		UpgradePrices HealthRegenerationUpgrade = new UpgradePrices(TowerUpgeradeType.HealthRegeneration, 10, 10);
		upgradePrices.Add(HealthRegenerationUpgrade);

		UpgradePrices BlockDamageSpeedUpgrade = new UpgradePrices(TowerUpgeradeType.BlockDamage, 10,10);
		upgradePrices.Add(BlockDamageSpeedUpgrade);

		UpgradePrices AttackRangeSpeedUpgrade = new UpgradePrices(TowerUpgeradeType.AttackRange, 10, 10);
		upgradePrices.Add(AttackRangeSpeedUpgrade);

		UpgradePrices EndWaveincome = new UpgradePrices(TowerUpgeradeType.EndWaveIncome, 10, 26);
		upgradePrices.Add(EndWaveincome);

		UpgradePrices EnemyValueMulti = new UpgradePrices(TowerUpgeradeType.EnemyValueMulti, 25, 1);
		upgradePrices.Add(EnemyValueMulti);

	}

	public void InitUpgradeUI(TowerUpgeradeType upgeradeType)
	{
		UpgradePrices findedTowerUpgrade = upgradePrices.Find(x => x._upgradeType == upgeradeType);
		EventBus.UpgradeStatUI(findedTowerUpgrade);
	}

	private void OnEnable()
	{
		EventBus.OnTowerUpgrade += UpgradeTower;
		EventBus.OnInitUpgradeUI += InitUpgradeUI;
	}

	private void OnDisable()
	{
		EventBus.OnTowerUpgrade -= UpgradeTower;
		EventBus.OnInitUpgradeUI -= InitUpgradeUI;
	}
}
