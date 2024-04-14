using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour 
{
	private float currenTimeScale = 1f;

	public void UpdradeDamage()
	{
		EventBus.TowerUpgraded(TowerUpgeradeType.Damage);
	}

	public void UpdradeArmor()
	{
		EventBus.TowerUpgraded(TowerUpgeradeType.Armor);
	}

	public void UpdradeProjectileSpeed()
	{
		EventBus.TowerUpgraded(TowerUpgeradeType.ProjectileSpeed);
	}

	public void UpdradeAttackSpeed()
	{
		EventBus.TowerUpgraded(TowerUpgeradeType.AttackSpeed);
	}

	public void UpdradeBlockDamage()
	{
		EventBus.TowerUpgraded(TowerUpgeradeType.BlockDamage);
	}

	public void UpdradeHealth()
	{
		EventBus.TowerUpgraded(TowerUpgeradeType.Health);
	}

	public void UpdradeHealthRegeneration()
	{
		EventBus.TowerUpgraded(TowerUpgeradeType.HealthRegeneration);
	}

	public void UpdradeEndWaveIncome()
	{
		EventBus.TowerUpgraded(TowerUpgeradeType.EndWaveIncome);
	}

	public void UpdradeEnemyValueMulti()
	{
		EventBus.TowerUpgraded(TowerUpgeradeType.EnemyValueMulti);
	}

	public void UpdradeAttackRange()
	{
		EventBus.TowerUpgraded(TowerUpgeradeType.AttackRange);
	}

	public void OpenPauseMenu()
	{
		/*if (UIManager.Instance.SetPausePanelActive())
		{
			PauseGame();
		}
		else
		{
			UnPauseGame();
		}*/
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
		EventBus.GameSpeedChanged();
	}

	public void DecreseGameSpeed()
	{
		if (Time.timeScale == 0.5f)
		{
			return;
		}

		Time.timeScale -= 0.5f;
		currenTimeScale = Time.timeScale;
		EventBus.GameSpeedChanged();
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
