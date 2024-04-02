using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
	private float currenTimeScale = 1f;

	public void UpdradeDamage()
	{
		Tower.Instance.UpgradeTower(TowerUpgradeHelpers.TowerUpgeradeType.Damage);
	}

	public void UpdradeArmor()
	{
		Tower.Instance.UpgradeTower(TowerUpgradeHelpers.TowerUpgeradeType.Armor);
	}

	public void UpdradeProjectileSpeed()
	{
		Tower.Instance.UpgradeTower(TowerUpgradeHelpers.TowerUpgeradeType.ProjectileSpeed);
	}

	public void UpdradeAttackSpeed()
	{
		Tower.Instance.UpgradeTower(TowerUpgradeHelpers.TowerUpgeradeType.AttackSpeed);
	}

	public void UpdradeBlockDamage()
	{
		Tower.Instance.UpgradeTower(TowerUpgradeHelpers.TowerUpgeradeType.BlockDamage);
	}

	public void UpdradeHealth()
	{
		Tower.Instance.UpgradeTower(TowerUpgradeHelpers.TowerUpgeradeType.Health);
	}

	public void UpdradeHealthRegeneration()
	{
		Tower.Instance.UpgradeTower(TowerUpgradeHelpers.TowerUpgeradeType.HealthRegeneration);
	}

	public void UpdradeEndWaveIncome()
	{
		TowerUpgradeHelpers.UpgradeNonBattleStat(TowerUpgradeHelpers.TowerUpgeradeType.EndWaveIncome);
	}

	public void UpdradeEnemyValueMulti()
	{
		TowerUpgradeHelpers.UpgradeNonBattleStat(TowerUpgradeHelpers.TowerUpgeradeType.EnemyValueMulti);
	}

	public void UpdradeAttackRange()
	{
		Tower.Instance.UpgradeTower(TowerUpgradeHelpers.TowerUpgeradeType.AttackRange);
	}

	public void OpenPauseMenu()
	{
		if (UIManager.Instance.SetPausePanelActive())
		{
			PauseGame();
		}
		else
		{
			UnPauseGame();
		}
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
	}

	public void UnPauseGame()
	{
		Time.timeScale = currenTimeScale;
	}

	public void IncreseGameSpeed()
	{
		if (Time.timeScale == 2f)
		{
			return;
		}

		Time.timeScale += 0.5f;
		currenTimeScale = Time.timeScale;
		UIManager.Instance.WriteCurrenGameSpeed();
	}

	public void DecreseGameSpeed()
	{
		if (Time.timeScale == 0.5f)
		{
			return;
		}

		Time.timeScale -= 0.5f;
		currenTimeScale = Time.timeScale;
		UIManager.Instance.WriteCurrenGameSpeed();
	}

	public void OpenGame()
	{
		SceneManager.LoadScene("SampleScene");
		Time.timeScale = 1;
	}

	public void OpenMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}


	public void MuteEmbient()
	{

	}
}
