using DG.Tweening;
using Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	[Header("Damage")]
	[SerializeField] private TextMeshProUGUI DamageUpgradePrice;
	[SerializeField] private TextMeshProUGUI DamageUpgradeStat;

	[Header("AttackSpeed")]
	[SerializeField] private TextMeshProUGUI AttackSpeedUpgradePrice;
	[SerializeField] private TextMeshProUGUI AttackSpeedUpgradeStat;

	[Header("Health")]
	[SerializeField] private TextMeshProUGUI HeathUpgradePrice;
	[SerializeField] private TextMeshProUGUI HeathUpgradeStat;

	[Header("Health Regen")]
	[SerializeField] private TextMeshProUGUI HPRegenUpgradePrice;
	[SerializeField] private TextMeshProUGUI HPRegenUpgradeStat;

	[Header("AttackRange")]
	[SerializeField] private TextMeshProUGUI AttackRangeUpgradePrice;
	[SerializeField] private TextMeshProUGUI AttackRangeUpgradeStat;

	[Header("Armor")]
	[SerializeField] private TextMeshProUGUI ArmorUpgradePrice;
	[SerializeField] private TextMeshProUGUI ArmorUpgradeStat;

	[Header("Damage Block")]
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
	[SerializeField] private TextMeshProUGUI WaveNumber;

	[SerializeField] private TextMeshProUGUI CurrenGameSpeed;

	[Header("Game over")]
	[SerializeField] private GameObject GameOverPanel;
	[SerializeField] private TextMeshProUGUI GameOverScore;

	[Header("Pause")]
	[SerializeField] private GameObject PausePanel;

	[Header("Money")]
	[SerializeField] private TextMeshProUGUI MoneyText;

	private void OnEnable()
	{
		EventBus.OnGameSpeedChanged += WriteCurrenGameSpeed;
		EventBus.OnUpgradeStatUI += WritePrice;
		EventBus.OnMoneyChanged += UpdateMoneyText;
		EventBus.OnWaveChanged += WriteNewWave;
	}

	private void OnDisable()
	{
		EventBus.OnGameSpeedChanged -= WriteCurrenGameSpeed;
		EventBus.OnUpgradeStatUI -= WritePrice;
		EventBus.OnMoneyChanged -= UpdateMoneyText;
		EventBus.OnWaveChanged -= WriteNewWave;
	}

	void Start()
	{
		ActivateAttackUpgadesPanel();
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
		//GameOver.text = LevelManager.instance.GetScore().ToString();
		GameOverPanel.SetActive(true);
		GameOverPanel.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		GameOverPanel.transform.DOScale(Vector3.one, 0.5f)
			.SetUpdate(UpdateType.Normal, true);
		//LevelManager.instance.SetPauseState(true);
	}

	public bool SetPausePanelActive()
	{
		PausePanel.SetActive(!PausePanel.activeSelf);
		if (PausePanel.activeSelf)
		{
			PausePanel.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			PausePanel.transform.DOScale(Vector3.one, 0.5f)
				.SetUpdate(UpdateType.Normal, true);
		}

		return PausePanel.activeSelf;

	}

	private void WritePrice(UpgradePrices upgradePrice)
	{
		switch (upgradePrice._upgradeType)
		{
			case TowerUpgeradeType.Damage:
				{
					DamageUpgradePrice.text = upgradePrice._price.ToString();
					break;
				}
			case TowerUpgeradeType.Health:
				{
					HeathUpgradePrice.text = upgradePrice._price.ToString();
					break;
				}
			case TowerUpgeradeType.HealthRegeneration:
				{
					HPRegenUpgradePrice.text = upgradePrice._price.ToString();
					break;
				}
			case TowerUpgeradeType.AttackSpeed:
				{
					AttackSpeedUpgradePrice.text = upgradePrice._price.ToString();
					break;
				}
			case TowerUpgeradeType.BlockDamage:
				{
					DamageBlockUpgradePrice.text = upgradePrice._price.ToString();
					break;
				}
			case TowerUpgeradeType.Armor:
				{
					ArmorUpgradePrice.text = upgradePrice._price.ToString();
					break;
				}
			case TowerUpgeradeType.EndWaveIncome:
				{
					EndWaveIncomeUpgradePrice.text = upgradePrice._price.ToString();
					break;
				}
			case TowerUpgeradeType.EnemyValueMulti:
				{
					EnemyValueMultiUpgradePrice.text = upgradePrice._price.ToString();
					break;
				}
			case TowerUpgeradeType.AttackRange:
				{
					AttackRangeUpgradePrice.text = upgradePrice._price.ToString();
					break;
				}
			default:
				{
					break;
				}
		}

		WriteStat(upgradePrice);
	}
	private void WriteStat(UpgradePrices upgradePrice)
	{
		switch (upgradePrice._upgradeType)
		{
			case TowerUpgeradeType.Damage:
				{
					DamageUpgradeStat.text = upgradePrice._value.ToString();
					break;
				}
			case TowerUpgeradeType.Health:
				{
					HeathUpgradeStat.text = upgradePrice._value.ToString();
					break;
				}
			case TowerUpgeradeType.HealthRegeneration:
				{
					HPRegenUpgradeStat.text = upgradePrice._value.ToString();
					break;
				}
			case TowerUpgeradeType.AttackSpeed:
				{
					AttackSpeedUpgradeStat.text = upgradePrice._value.ToString();
					break;
				}
			case TowerUpgeradeType.BlockDamage:
				{
					DamageBlockUpgradeStat.text = upgradePrice._value.ToString();
					break;
				}
			case TowerUpgeradeType.Armor:
				{
					ArmorUpgradeStat.text = upgradePrice._value.ToString();
					break;
				}
			case TowerUpgeradeType.EndWaveIncome:
				{
					EndWaveIncomeUpgradeStat.text = upgradePrice._value.ToString();
					break;
				}
			case TowerUpgeradeType.EnemyValueMulti:
				{
					EnemyValueMultiUpgradeStat.text = "x" + upgradePrice._value.ToString();
					break;
				}
			case TowerUpgeradeType.AttackRange:
				{
					AttackRangeUpgradeStat.text = upgradePrice._value.ToString();
					break;
				}
			default:
				{
					break;
				}
		}
	}

	private void WriteNewWave()
	{
		WaveNumber.text = "Wave:" + EnemySpawner.GetCurrentWave();
	}

	private void WriteCurrenGameSpeed()
	{
		CurrenGameSpeed.text = Time.timeScale + "x";
	}

	private void UpdateMoneyText()
	{
		MoneyText.text = Wallet._balance.ToString();
	}
}
