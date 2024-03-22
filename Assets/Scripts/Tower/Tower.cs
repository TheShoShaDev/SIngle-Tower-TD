using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static TowerUpgradeHelpers;

public class Tower : MonoBehaviour
{
	[Header("BaseStats")]
	[SerializeField] private float BaseHP;
	[SerializeField] private float BaseDamage;
	[SerializeField] private float BaseArmor;
	[SerializeField] private float BaseAttackSpeed;
	[SerializeField] private float BaseProjectileSpeed;
	[SerializeField] private float BaseBlockDamage;
	[SerializeField] private float BaseHealthRegen;

	[Header("Attacking")]
	[SerializeField] private GameObject ProjectilePrefab;
	[SerializeField] private Transform ProjectileSpawnPos;
	[SerializeField] private float timeSinceLastAttack;

	[Header("Info")]
	[SerializeField] private float AttackRange;
	[SerializeField] private List<Enemy> CurEnemiesInRange = new List<Enemy>();
	private Enemy CurEnemy;

	public TowerTargetPriority TargetPriority;

	public static Tower Instance;

	[Header("CurInfo")]
	[SerializeField] private float CurrentHP;
	[SerializeField] private float CurrentDamage;
	[SerializeField] private float CurrentArmor;
	[SerializeField] private float CurrentAttackSpeed;
	[SerializeField] private float CurrentProjectileSpeed;
	[SerializeField] private float CurrenBlockDamage;
	[SerializeField] private float CurrenHealthRegen;
	[SerializeField] private float MaxHP;

	[Header("Some Trash")]
	[SerializeField] private CircleCollider2D circleCollider;

	private float SavedHP = 0;
	private float SavedDamage = 0;
	private float SavedArmor = 0;
	private float SavedAttackSpeed = 0;
	private float SavedProjectileSpeed = 0;
	private float SavedBlockDamage = 0;
	private float SavedHealthRegen = 0;

	private bool IsDied = false;

	private List<UpgradePrices> upgradePrices = new List<UpgradePrices>();

	private void Awake()
	{
		Instance = this;
		InitUpgradePrices();
		UpdateAttackRadius();
	}

	private void Start()
	{
		LoadUpgrades();
		InitCurrentStats();
		StartCoroutine(HPRegen());

		foreach (UpgradePrices upgrade in upgradePrices)
		{
			UpdateUpgradeStat(upgrade.GetUpgradeType());
		}
	}

	private Tower()
	{
		//InitUpgradePrices();
	}

	private void Update()
	{
		timeSinceLastAttack += Time.deltaTime;

		if (timeSinceLastAttack >= 1 / CurrentAttackSpeed)
		{
			CurEnemy = GetEnemy();
			if (CurEnemy != null)
			{
				Attack();
				timeSinceLastAttack = 0;
			}
		}
	}

	private void InitCurrentStats()
	{
		CurrentHP = BaseHP + SavedHP;
		CurrentDamage = BaseDamage + SavedDamage;
		CurrentArmor = BaseArmor + SavedArmor;
		CurrentAttackSpeed = BaseAttackSpeed + SavedAttackSpeed;
		CurrentProjectileSpeed = BaseProjectileSpeed + SavedProjectileSpeed;
		CurrenBlockDamage = BaseBlockDamage + SavedBlockDamage;
		CurrenHealthRegen = BaseHealthRegen + SavedHealthRegen;
		MaxHP = CurrentHP;
	}

	#region Upgrades
	private void UpdateAttackRadius()
	{
		Vector3 center = transform.position;
		float radius = circleCollider.radius;
		Debug.DrawLine(center + Vector3.left * radius, center + Vector3.right * radius, Color.red, 0f, false);
	}

	private IEnumerator HPRegen()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			IncreseHP(CurrenHealthRegen);
		}
	}

	private void InitUpgradePrices()
	{
		upgradePrices = TowerUpgradeHelpers.GetPrices();
	}

	public List<UpgradePrices> GetPrices()
	{
		return upgradePrices;
	}

	public void SaveUpgrades()
	{
		PlayerPrefs.SetFloat("Saved Damage", SavedDamage);
		PlayerPrefs.SetFloat("Saved Attack Speed", SavedAttackSpeed);
		PlayerPrefs.SetFloat("Saved Health", SavedHP);
		PlayerPrefs.SetFloat("Saved Armor", SavedArmor);
		PlayerPrefs.SetFloat("Saved Projectile Speed", SavedProjectileSpeed);
		PlayerPrefs.SetFloat("Saved Block Damage", SavedBlockDamage);
		PlayerPrefs.SetFloat("Saved Health Regen", SavedHealthRegen);
	}

	private void LoadUpgrades()
	{
		SavedDamage = PlayerPrefs.GetFloat("Saved Damage");
		SavedAttackSpeed = PlayerPrefs.GetFloat("Saved Attack Speed");
		SavedHP = PlayerPrefs.GetFloat("Saved Health");
		SavedArmor = PlayerPrefs.GetFloat("Saved Armor");
		SavedProjectileSpeed = PlayerPrefs.GetFloat("Saved Projectile Speed");
		SavedBlockDamage = PlayerPrefs.GetFloat("Saved Block Damage");
		SavedHealthRegen = PlayerPrefs.GetFloat("Saved Health Regen");
	}

	public void UpgradeTower(TowerUpgeradeType Type)
	{
		UpgradePrices FindedTowerUpgrade = upgradePrices.Find(x => x.GetUpgradeType() == Type);
		int index = upgradePrices.IndexOf(FindedTowerUpgrade);

		if (!Wallet.Instance.IsEnoughtMoney(FindedTowerUpgrade.GetPrice()))
		{
			return;
		}
		Wallet.Instance.DecreseBalance(FindedTowerUpgrade.GetPrice());


		switch (Type)
		{
			case TowerUpgeradeType.Damage:
				{
					FindedTowerUpgrade.IncresePrice(3);
					CurrentDamage += 7;
					UpdateUpgradeStat(TowerUpgeradeType.Damage);
					break;
				}
			case TowerUpgeradeType.AttackSpeed:
				{
					FindedTowerUpgrade.IncresePrice(4);
					CurrentAttackSpeed += 0.11f;
					UpdateUpgradeStat(TowerUpgeradeType.AttackSpeed);
					break;
				}
			case TowerUpgeradeType.Health:
				{
					FindedTowerUpgrade.IncresePrice(6);
					CurrentHP += 15;
					MaxHP += 15;
					UpdateUpgradeStat(TowerUpgeradeType.Health);
					break;
				}
			case TowerUpgeradeType.Armor:
				{
					FindedTowerUpgrade.IncresePrice(3);
					CurrentArmor += 3;
					UpdateUpgradeStat(TowerUpgeradeType.Armor);
					break;
				}
			case TowerUpgeradeType.ProjectileSpeed:
				{
					FindedTowerUpgrade.IncresePrice(5);
					CurrentProjectileSpeed += 1.25f;
					UpdateUpgradeStat(TowerUpgeradeType.ProjectileSpeed);
					break;
				}
			case TowerUpgeradeType.HealthRegeneration:
				{
					FindedTowerUpgrade.IncresePrice(12);
					CurrenHealthRegen += 3;
					UpdateUpgradeStat(TowerUpgeradeType.HealthRegeneration);
					break;
				}
			case TowerUpgeradeType.BlockDamage:
				{
					FindedTowerUpgrade.IncresePrice(7);
					CurrenBlockDamage += 4;
					UpdateUpgradeStat(TowerUpgeradeType.BlockDamage);
					break;
				}
			case TowerUpgeradeType.AttackRange:
				{
					FindedTowerUpgrade.IncresePrice(50);
					CircleCollider2D AttackRange = gameObject.GetComponent<CircleCollider2D>();
					AttackRange.radius += 0.1f;
					UIManager.Instance.WriteStat(FindedTowerUpgrade, AttackRange.radius.ToString());
					break;
				}
		}

		upgradePrices[index] = FindedTowerUpgrade;
		UIManager.Instance.WritePrice(FindedTowerUpgrade);
		LevelManager.instance.UpdateMoneyText();
	}
	
	private void UpdateUpgradeStat(TowerUpgeradeType Type)
	{
		UpgradePrices FindedTowerUpgrade = upgradePrices.Find(x => x.GetUpgradeType() == Type);

		switch (Type)
		{
			case TowerUpgeradeType.Damage:
				{
					UIManager.Instance.WriteStat(FindedTowerUpgrade, CurrentDamage.ToString());
					break;
				}
			case TowerUpgeradeType.AttackSpeed:
				{
					UIManager.Instance.WriteStat(FindedTowerUpgrade, CurrentAttackSpeed.ToString());
					break;
				}
			case TowerUpgeradeType.Health:
				{
					UIManager.Instance.WriteStat(FindedTowerUpgrade, CurrentHP.ToString());
					break;
				}
			case TowerUpgeradeType.Armor:
				{
					UIManager.Instance.WriteStat(FindedTowerUpgrade, CurrentArmor.ToString());
					break;
				}
			case TowerUpgeradeType.ProjectileSpeed:
				{
					UIManager.Instance.WriteStat(FindedTowerUpgrade, CurrentProjectileSpeed.ToString());
					break;
				}
			case TowerUpgeradeType.HealthRegeneration:
				{
					UIManager.Instance.WriteStat(FindedTowerUpgrade, CurrenHealthRegen.ToString());
					break;
				}
			case TowerUpgeradeType.BlockDamage:
				{
					UIManager.Instance.WriteStat(FindedTowerUpgrade, CurrenBlockDamage.ToString());
					break;
				}
			case TowerUpgeradeType.AttackRange:
				{
					CircleCollider2D AttackRange = gameObject.GetComponent<CircleCollider2D>();
					UIManager.Instance.WriteStat(FindedTowerUpgrade, AttackRange.radius.ToString());
					break;
				}
		}
	}
	#endregion

	private void IncreseHP(float hp)
	{
		CurrentHP += hp;
		if (CurrentHP > MaxHP)
		{
			CurrentHP = MaxHP;
		}
	}

	public void GetDamage(float Damage)
	{
		if (IsDied)
		{
			return;
		}

		CurrentHP -= Damage - ((CurrentArmor / 100) * Damage) - CurrenBlockDamage;

		UIManager.Instance.DrawCurHP();


		if (CurrentHP <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		IsDied = true;
		UIManager.Instance.ActivateGameOverPanel();
		//LevelManager.instance.SetPauseState(true);
	}

	public float CurrentHealth()
	{
		return CurrentHP;
	}

	public float GetMaxHP()
	{
		return MaxHP;
	}

	public enum TowerTargetPriority
	{
		First,
		Strong,
		Close
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			CurEnemiesInRange.Add(collision.GetComponent<Enemy>());
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			CurEnemiesInRange.Remove(collision.GetComponent<Enemy>());
		}
	}

	private void Attack()
	{
		GameObject proj = Instantiate(ProjectilePrefab, ProjectileSpawnPos.position, Quaternion.identity);
		proj.GetComponent<Projectile>().Initialize(CurEnemy, CurrentDamage, CurrentProjectileSpeed);

	}

	private Enemy GetEnemy()
	{
		CurEnemiesInRange.RemoveAll(x => x == null);

		if (CurEnemiesInRange.Count == 0)
		{
			return null;
		}

		if (CurEnemiesInRange.Count == 1)
		{
			return CurEnemiesInRange[0];
		}

		switch (TargetPriority)
		{
			case TowerTargetPriority.First:
				{
					return CurEnemiesInRange[0];
				}

			case TowerTargetPriority.Strong:
				{
					Enemy Strongest = null;
					float StrongestHP = 0;
					foreach (Enemy enemy in CurEnemiesInRange)
					{
						if (enemy.GetCurrentHP() > StrongestHP)
						{
							Strongest = enemy;
							StrongestHP = enemy.GetCurrentHP();
						}
					}

					return Strongest;
				}

			case TowerTargetPriority.Close:
				{
					Enemy Closest = null;
					float dist = 99f;
					for (int x = 0; x < CurEnemiesInRange.Count; x++)
					{
						float d = (transform.position - CurEnemiesInRange[x].transform.position).sqrMagnitude;

						if (d > dist)
						{
							Closest = CurEnemiesInRange[x];
							dist = d;
						}
					}

					return Closest;
				}
		}
		return null;
	}
}
