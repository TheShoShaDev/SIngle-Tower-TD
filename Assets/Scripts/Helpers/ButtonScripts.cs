using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;

public class ButtonScripts : MonoBehaviour
{
	private float currenTimeScale = 1f;

	public void UpdradeDamage()
	{
		Tower.Instance.UpgradeTower(TowerUpgeradeType.Damage);
	}

	public void UpdradeArmor()
	{
		Tower.Instance.UpgradeTower(TowerUpgeradeType.Armor);
	}

	public void UpdradeProjectileSpeed()
	{
		Tower.Instance.UpgradeTower(TowerUpgeradeType.ProjectileSpeed);
	}

	public void UpdradeAttackSpeed()
	{
		Tower.Instance.UpgradeTower(TowerUpgeradeType.AttackSpeed);
	}

	public void UpdradeBlockDamage()
	{
		Tower.Instance.UpgradeTower(TowerUpgeradeType.BlockDamage);
	}

	public void UpdradeHealth()
	{
		Tower.Instance.UpgradeTower(TowerUpgeradeType.Health);
	}

	public void UpdradeHealthRegeneration()
	{
		Tower.Instance.UpgradeTower(TowerUpgeradeType.HealthRegeneration);
	}

	public void UpdradeEndWaveIncome()
	{
		TowerUpgradeHelpers.UpgradeNonBattleStat(TowerUpgeradeType.EndWaveIncome);
	}

	public void UpdradeEnemyValueMulti()
	{
		TowerUpgradeHelpers.UpgradeNonBattleStat(TowerUpgeradeType.EnemyValueMulti);
	}

	public void UpdradeAttackRange()
	{
		Tower.Instance.UpgradeTower(TowerUpgeradeType.AttackRange);
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
