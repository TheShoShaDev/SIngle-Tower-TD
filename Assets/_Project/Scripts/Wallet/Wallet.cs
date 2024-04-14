using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
	[SerializeField] private WalletConfig _config;

	public static int _balance { get; private set; }
	public static int _diamondBalance { get; private set; }

	private void Start()
	{
		_balance = _config._balance;
		_diamondBalance = PlayerPrefs.GetInt("Diamond Balance");
		EventBus.MoneyChanged();
	}

	public void IncreseBalance(int value)
	{
		if (value < 0)
		{
			throw new Exception("Extra money cannot be less than 0");
		}

		_balance += value;
		IncreseDiamondBalance(value);
		EventBus.MoneyChanged();
	}
	public void IncreseDiamondBalance(int value)
	{
		if (value < 0)
		{
			throw new Exception("Extra diamond cannot be less than 0");
		}

		_diamondBalance += value / 2;
	}

	public void DecreseBalance(int value)
	{
		if (value < 0)
		{
			throw new Exception("Taken amount of money cannot be less than 0");
		}

		_balance -= value;

		EventBus.MoneyChanged();
	}
	public static bool IsEnoughtMoney(int price)
	{
		return _balance >= price;
	}

	public void SaveDiamondBalance()
	{
		PlayerPrefs.SetInt("Diamond Balance", _diamondBalance);
	}

	private void OnEnable()
	{
		EventBus.OnBalanceIncresed += IncreseBalance;
		EventBus.OnBalanceDecresed += DecreseBalance;
	}

	private void OnDisable()
	{
		EventBus.OnBalanceIncresed -= IncreseBalance;
		EventBus.OnBalanceDecresed -= DecreseBalance;
	}
}

