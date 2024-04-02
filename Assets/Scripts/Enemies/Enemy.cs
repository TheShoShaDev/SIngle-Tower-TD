using DG.Tweening;
using UnityEngine;
using static TowerUpgradeHelpers;
using Enums;

public class Enemy : MonoBehaviour
{
	[Header("BaseStats")]
	[SerializeField] private float BaseHP = 100f;
	[SerializeField] private float BaseArmor = 0f;
	[SerializeField] private float BaseDamage = 1f;
	[SerializeField] private float BaseAttackSpeed = 3f;
	[SerializeField] private int Value;


	public static Enemy instance;

	[Header("Info")]
	[SerializeField] private float CurrentHP = 100f;
	[SerializeField] private float ExtraHP = 0;
	[SerializeField] private float CurrentArmor = 0f;
	[SerializeField] private float CurrentDamage = 1f;
	[SerializeField] private float ExtraDamage = 0f;
	[SerializeField] private float CurrentAttackSpeed = 3f;
	[SerializeField] private float LastAttackTime;

	private bool CanAttack = false;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		CurrentHP = BaseHP + ExtraHP;
		CurrentArmor = BaseArmor;
		CurrentDamage = BaseDamage + ExtraDamage;
		CurrentAttackSpeed = BaseAttackSpeed;
	}

	private void Update()
	{
		LastAttackTime += Time.deltaTime;
		if (LastAttackTime >= 1 / CurrentAttackSpeed
			&& CanAttack)
		{
			LastAttackTime = 0;

			if (gameObject != null)
			{
				Attack();
			}
		}
	}

	public void GetDamage(float damage)
	{
		CurrentHP -= damage - (CurrentArmor / 100) * damage;
		if (CurrentHP <= 0)
		{

			Wallet.Instance.IncreseBalance((int)(Value * GetValueOfNonBattleUpgreade(TowerUpgeradeType.EnemyValueMulti)));
			LevelManager.instance.IncreseScorePoints(Value * 1.5f);
			EmeniesSpawner.onEnemyDestroy.Invoke();
			DOTween.KillAll();
			Destroy(gameObject);
			LevelManager.instance.UpdateMoneyText();
			return;
		}

		Vector3 _localScale = transform.localScale;
		Vector3 _newScale = transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);

		DOTween.Sequence()
			.Append(transform.DOScale(_newScale, 0.05f).SetUpdate(UpdateType.Normal, true))
			.Append(transform.DOScale(_localScale, 0.05f).SetUpdate(UpdateType.Normal, true));
	}

	public void IncreseStats(float extDamage, float extHP)
	{
		ExtraHP = extHP;
		ExtraDamage = extDamage;
	}

	public void Attack()
	{
		Tower.Instance.GetDamage(CurrentDamage);
	}

	public float GetCurrentHP()
	{
		return CurrentHP;
	}

	public void SetCanAttack()
	{
		CanAttack = true;
	}
}
