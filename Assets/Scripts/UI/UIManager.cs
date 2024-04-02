using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TowerUpgradeHelpers;

public class UIManager : MonoBehaviour
{
	[Header ("Damage")]
	[SerializeField] private TextMeshProUGUI DamageUpgradePrice;
	[SerializeField] private TextMeshProUGUI DamageUpgradeStat;
	
	[Header ("AttackSpeed")]
	[SerializeField] private TextMeshProUGUI AttackSpeedUpgradePrice;
	[SerializeField] private TextMeshProUGUI AttackSpeedUpgradeStat;
	
	[Header ("Health")]
	[SerializeField] private TextMeshProUGUI HeathUpgradePrice;
	[SerializeField] private TextMeshProUGUI HeathUpgradeStat;
	
	[Header ("Health Regen")]
	[SerializeField] private TextMeshProUGUI HPRegenUpgradePrice;
	[SerializeField] private TextMeshProUGUI HPRegenUpgradeStat;
	
	[Header ("AttackRange")]
	[SerializeField] private TextMeshProUGUI AttackRangeUpgradePrice;
	[SerializeField] private TextMeshProUGUI AttackRangeUpgradeStat;
	

	[Header ("Armor")]
	[SerializeField] private TextMeshProUGUI ArmorUpgradePrice;
	[SerializeField] private TextMeshProUGUI ArmorUpgradeStat;

	[Header ("Damage Block")]
	[SerializeField] private TextMeshProUGUI DamageBlockUpgradePrice;
	[SerializeField] private TextMeshProUGUI DamageBlockUpgradeStat;

	[Header("End wave income")]
	[SerializeField] private TextMeshProUGUI EndWaveIncomeUpgradePrice;
	[SerializeField] private TextMeshProUGUI EndWaveIncomeUpgradeStat;

	[Header("Enemy value multi")]
	[SerializeField] private TextMeshProUGUI EnemyValueMultiUpgradePrice;
	[SerializeField] private TextMeshProUGUI EnemyValueMultiUpgradeStat;

	[Header("Nav Blocks")]
	[SerializeField] private GameObject AttackUpgadesPanel;
	[SerializeField] private GameObject DefenceUpgadesPanel;
	[SerializeField] private GameObject SpecialUpgadesPanel;

	[Header("Another")]
	[SerializeField] private Image HeathBar;

	[SerializeField] private TextMeshProUGUI WaveNumber;

	[SerializeField] private TextMeshProUGUI CurrenGameSpeed;

	[Header("Game over")]
	[SerializeField] private GameObject GameOverPanel;
	[SerializeField] private TextMeshProUGUI GameOver;

	[Header("Pause")]
	[SerializeField] private GameObject PausePanel;


	public static UIManager Instance;

	private void Awake()
	{
		Instance = this;
	}


	void Start()
	{
		ActivateAttackUpgadesPanel();
		Initprice();
	}

	public void ActivateAttackUpgadesPanel()
	{
		AttackUpgadesPanel.SetActive(true);
		DefenceUpgadesPanel.SetActive(false);
		SpecialUpgadesPanel.SetActive(false);
	}

	public void ActivateDefenceUpgadesPanel()
	{
		AttackUpgadesPanel.SetActive(false);
		DefenceUpgadesPanel.SetActive(true);
		SpecialUpgadesPanel.SetActive(false);
	}

	public void ActivateSpecialUpgadesPanel()
	{
		AttackUpgadesPanel.SetActive(false);
		DefenceUpgadesPanel.SetActive(false);
		SpecialUpgadesPanel.SetActive(true);
	}

	public void ActivateGameOverPanel()
	{
		GameOver.text = LevelManager.instance.GetScore().ToString();
		GameOverPanel.SetActive(true);
		GameOverPanel.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		GameOverPanel.transform.DOScale(Vector3.one, 0.5f)
			.SetUpdate(UpdateType.Normal, true);
		LevelManager.instance.SetPauseState(true);
	}

	public bool SetPausePanelActive()
	{
		PausePanel.SetActive(!PausePanel.activeSelf);
		if (PausePanel.activeSelf)
		{
			PausePanel.transform.localScale = new Vector3(0.3f ,0.3f, 0.3f);
			PausePanel.transform.DOScale(Vector3.one, 0.5f)
				.SetUpdate(UpdateType.Normal, true);
		}
		
		return PausePanel.activeSelf;

	}

	private void Initprice()
	{
		List<UpgradePrices> upgradePrices = GetPrices();

		foreach (UpgradePrices upgradePrice in upgradePrices)
		{
			WritePrice(upgradePrice);
		}

		List<NonBattlePrice> nonBattlePrices = GetNonBattlePrices();
		
		foreach (NonBattlePrice nonBattlePrice in nonBattlePrices)
		{
			WritePrice(nonBattlePrice.ToUpgradePrices());
			WriteStat(nonBattlePrice.ToUpgradePrices(), nonBattlePrice.GetValue().ToString());
		}
	}

	public void WritePrice(UpgradePrices upgradePrice)
	{
		switch (upgradePrice.GetUpgradeType())
		{
			case TowerUpgeradeType.Damage:
				{
					DamageUpgradePrice.text = upgradePrice.GetPrice().ToString();
					break;
				}
			case TowerUpgeradeType.Health:
				{
					HeathUpgradePrice.text = upgradePrice.GetPrice().ToString();
					break;
				}
			case TowerUpgeradeType.HealthRegeneration:
				{
					HPRegenUpgradePrice.text = upgradePrice.GetPrice().ToString();
					break;
				}
			case TowerUpgeradeType.AttackSpeed:
				{
					AttackSpeedUpgradePrice.text = upgradePrice.GetPrice().ToString();
					break;
				}
			case TowerUpgeradeType.BlockDamage:
				{
					DamageBlockUpgradePrice.text = upgradePrice.GetPrice().ToString();
					break;
				}
			case TowerUpgeradeType.Armor:
				{
					ArmorUpgradePrice.text = upgradePrice.GetPrice().ToString();
					break;
				}
			case TowerUpgeradeType.EndWaveIncome:
				{
					EndWaveIncomeUpgradePrice.text = upgradePrice.GetPrice().ToString();
					break;
				}
			case TowerUpgeradeType.EnemyValueMulti:
				{
					EnemyValueMultiUpgradePrice.text = upgradePrice.GetPrice().ToString();
					break;
				}	
			case TowerUpgeradeType.AttackRange:
				{
					AttackRangeUpgradePrice.text = upgradePrice.GetPrice().ToString();
					break;
				}
			default:
				{
					break;
				}
		}
	}
	public void WriteStat(UpgradePrices upgradePrice, string stat)
	{
		switch (upgradePrice.GetUpgradeType())
		{
			case TowerUpgeradeType.Damage:
				{
					DamageUpgradeStat.text = stat;
					break;
				}
			case TowerUpgeradeType.Health:
				{
					HeathUpgradeStat.text = stat;
					break;
				}
			case TowerUpgeradeType.HealthRegeneration:
				{
					HPRegenUpgradeStat.text = stat;
					break;
				}
			case TowerUpgeradeType.AttackSpeed:
				{
					AttackSpeedUpgradeStat.text = stat;
					break;
				}
			case TowerUpgeradeType.BlockDamage:
				{
					DamageBlockUpgradeStat.text = stat;
					break;
				}
			case TowerUpgeradeType.Armor:
				{
					ArmorUpgradeStat.text = stat;
					break;
				}
			case TowerUpgeradeType.EndWaveIncome:
				{
					EndWaveIncomeUpgradeStat.text = stat;
					break;
				}
			case TowerUpgeradeType.EnemyValueMulti:
				{
					EnemyValueMultiUpgradeStat.text = "x" + stat;
					break;
				}	
			case TowerUpgeradeType.AttackRange:
				{
					AttackRangeUpgradeStat.text = stat;
					break;
				}
			default:
				{
					break;
				}
		}
	}

	public void WriteNewWave()
	{
		WaveNumber.text = "Wave:" + EmeniesSpawner.instance.GetCurrentWave();
	}

	public void WriteCurrenGameSpeed()
	{
		CurrenGameSpeed.text = Time.timeScale + "x";
	}

	public void DrawCurHP()
	{
		float currenthp = Tower.Instance.CurrentHealth();
		float maxhp = Tower.Instance.GetMaxHP();

		HeathBar.fillAmount = currenthp / maxhp;
	}
}
