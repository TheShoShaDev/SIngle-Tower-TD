using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePrices : MonoBehaviour
{
	public TowerUpgeradeType _upgradeType {  get; private set; }
	public int _price { get; private set; }
	public float _value { get; private set; }

	public void IncresePrice(int Value)
	{
		_price += Value;
	}

	public void IncreseValue(float Value)
	{
		_value += Value;
	}

	public UpgradePrices(TowerUpgeradeType UpgeradeType, int Price, float Value)
	{
		_upgradeType = UpgeradeType;
		_price = Price;
		_value = Value;
	}

}