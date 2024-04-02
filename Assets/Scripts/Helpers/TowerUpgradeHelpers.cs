using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public static class TowerUpgradeHelpers
{
	static float EndWaveIncome;

	static TowerUpgradeHelpers()
	{
		InitUpgradePrices();
		EndWaveIncome = PlayerPrefs.GetFloat("End wave income");
	}

	private static List<UpgradePrices> upgradePrices = new List<UpgradePrices>();
	private static List<NonBattlePrice> nonBattleUpgradePrices = new List<NonBattlePrice>();


	public struct UpgradePrices
	{
		private TowerUpgeradeType UpgradeType;
		private int Price;

		public void IncresePrice(int Value)
		{
			Price += Value;
		}

		public UpgradePrices(TowerUpgeradeType UpgeradeType, int Price)
		{
			this.UpgradeType = UpgeradeType;
			this.Price = Price;
		}

		public TowerUpgeradeType GetUpgradeType()
		{
			return UpgradeType;
		}

		public int GetPrice()
		{
			return Price;
		}

	}

	public struct NonBattlePrice
	{
		private TowerUpgeradeType UpgradeType;
		private int Price;
		private float Value;

		public void IncresePrice(int Value)
		{
			this.Price += Value;
		}

		public void IncreseValue(float Value)
		{
			this.Value += Value;
		}

		public NonBattlePrice(TowerUpgeradeType UpgeradeType, int Price, float Value)
		{
			this.UpgradeType = UpgeradeType;
			this.Price = Price;
			this.Value = Value;
		}

		public TowerUpgeradeType GetUpgradeType()
		{
			return UpgradeType;
		}

		public int GetPrice()
		{
			return Price;
		}

		public float GetValue()
		{ 
			return Value; 
		}

		public UpgradePrices ToUpgradePrices()
		{
			return new UpgradePrices(UpgradeType, GetPrice());
		}
	}

	private static void InitUpgradePrices()
	{
		UpgradePrices DamageUpgrade = new UpgradePrices(TowerUpgeradeType.Damage, 10);
		upgradePrices.Add(DamageUpgrade);

		UpgradePrices AttackSpeedUpgrade = new UpgradePrices(TowerUpgeradeType.AttackSpeed, 10);
		upgradePrices.Add(AttackSpeedUpgrade);

		UpgradePrices HealthUpgrade = new UpgradePrices(TowerUpgeradeType.Health, 10);
		upgradePrices.Add(HealthUpgrade);

		UpgradePrices ArmorUpgrade = new UpgradePrices(TowerUpgeradeType.Armor, 10);
		upgradePrices.Add(ArmorUpgrade);

		UpgradePrices ProjectileSpeedUpgrade = new UpgradePrices(TowerUpgeradeType.ProjectileSpeed, 10);
		upgradePrices.Add(ProjectileSpeedUpgrade);

		UpgradePrices HealthRegenerationUpgrade = new UpgradePrices(TowerUpgeradeType.HealthRegeneration, 10);
		upgradePrices.Add(HealthRegenerationUpgrade);

		UpgradePrices BlockDamageSpeedUpgrade = new UpgradePrices(TowerUpgeradeType.BlockDamage, 10);
		upgradePrices.Add(BlockDamageSpeedUpgrade);	
		
		UpgradePrices AttackRangeSpeedUpgrade = new UpgradePrices(TowerUpgeradeType.AttackRange, 10);
		upgradePrices.Add(AttackRangeSpeedUpgrade);

		NonBattlePrice EndWaveincome = new NonBattlePrice(TowerUpgeradeType.EndWaveIncome, 10, EndWaveIncome);
		nonBattleUpgradePrices.Add(EndWaveincome);

		NonBattlePrice EnemyValueMulti = new NonBattlePrice(TowerUpgeradeType.EnemyValueMulti, 25, 1);
		nonBattleUpgradePrices.Add(EnemyValueMulti);

	}

	public static List<UpgradePrices> GetPrices()
	{
		return upgradePrices;
	}

	public static List<NonBattlePrice> GetNonBattlePrices()
	{
		return nonBattleUpgradePrices;
	}

	public static float GetValueOfNonBattleUpgreade(TowerUpgeradeType type)
	{
		return nonBattleUpgradePrices.Find(x => x.GetUpgradeType() == type).GetValue();
	}

	public static void UpgradeNonBattleStat(TowerUpgeradeType type)
	{
		NonBattlePrice FindedNonBattleUpgrade = nonBattleUpgradePrices.Find(x => x.GetUpgradeType() == type);

		int index = nonBattleUpgradePrices.IndexOf(FindedNonBattleUpgrade);

		if (!Wallet.Instance.IsEnoughtMoney(FindedNonBattleUpgrade.GetPrice()))
		{
			return;
		}
		Wallet.Instance.DecreseBalance(FindedNonBattleUpgrade.GetPrice());

		switch (type)
		{
			case TowerUpgeradeType.EndWaveIncome:
				{
					FindedNonBattleUpgrade.IncresePrice(26);
					FindedNonBattleUpgrade.IncreseValue(26);
					break;
				}
			case TowerUpgeradeType.EnemyValueMulti:
				{
					FindedNonBattleUpgrade.IncresePrice(45);
					FindedNonBattleUpgrade.IncreseValue(0.1f);
					break;
				}
		}

		nonBattleUpgradePrices[index] = FindedNonBattleUpgrade;
		UIManager.Instance.WritePrice(FindedNonBattleUpgrade.ToUpgradePrices());
		UIManager.Instance.WriteStat(FindedNonBattleUpgrade.ToUpgradePrices(), FindedNonBattleUpgrade.GetValue().ToString());
		LevelManager.instance.UpdateMoneyText();
	}

}
