using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
	public static event Action<float> OnTowerDamageTaken;

	public static event Action<TowerUpgeradeType> OnTowerUpgrade;

	public static event Action OnGameSpeedChanged;

	//public static event Action<UpgradePrices> OnTowerUpgraded;

	public static event Action<int> OnBalanceIncresed;
	public static event Action<int> OnBalanceDecresed;

	public static event Action OnMoneyChanged;

	public static event Action OnWaveChanged;

	public static event Action<UpgradePrices> OnUpgradeStat;
	public static event Action<UpgradePrices> OnUpgradeStatUI;

	public static event Action onWaveStart;

	public static event Action<TowerUpgeradeType> OnInitUpgradeUI;

	public static void TowerDamageTaken(float damage)
	{
		OnTowerDamageTaken?.Invoke(damage);
	}
	
	public static void TowerUpgraded(TowerUpgeradeType type)
	{
		OnTowerUpgrade?.Invoke(type);
	}

	public static void GameSpeedChanged()
	{
		OnGameSpeedChanged?.Invoke();
	}

	public static void IncreseBalance(int value)
	{
		OnBalanceIncresed?.Invoke(value);
	}
	public static void DecreseBalance(int value)
	{
		OnBalanceDecresed?.Invoke(value);
	}

	public static void MoneyChanged()
	{ 
		OnMoneyChanged?.Invoke();
	}

	public static void WaveChanged()
	{
		OnWaveChanged?.Invoke();
	}

	public static void UpgradeStat(UpgradePrices type)
	{
		OnUpgradeStat?.Invoke(type); 
	}
	public static void UpgradeStatUI(UpgradePrices type)
	{
		OnUpgradeStatUI?.Invoke(type); 
	}

	public static void WaveStart()
	{
		onWaveStart?.Invoke();
	}

	public static void InitUpgradeUI(TowerUpgeradeType type)
	{
		OnInitUpgradeUI?.Invoke(type);
	}
}
