using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private int balance = 0;
    [SerializeField] private int diamondBalance = 0;

    public static Wallet Instance;

	private void Awake()
	{
		Instance = this;
        DontDestroyOnLoad(gameObject);
		LoadDiamondBalance();
        LevelManager.instance.UpdateMoneyText();
	}

	public void IncreseBalance(int value)
    {
        balance += value;
    }	
    public void IncreseDiamondBalance(int value)
    {
        diamondBalance += value / 2;
    }

    public void DecreseBalance(int value)
    {
        balance -= value;
    }

    public int GetValue()
    { 
        return balance;
    }

    public bool IsEnoughtMoney(int Price)
    {
        return balance >= Price;
    }

    public void SaveDiamondBalance()
    {
		PlayerPrefs.SetInt("Diamond Balance", diamondBalance);
	}

    private void LoadDiamondBalance()
    {
		diamondBalance = PlayerPrefs.GetInt("Diamond Balance");
	}
}
