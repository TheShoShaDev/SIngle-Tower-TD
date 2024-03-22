using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TowerUpgradeHelpers;

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
		if ( LastAttackTime >=  1 / CurrentAttackSpeed 
			&& CanAttack)
		{
			LastAttackTime = 0;

			if (gameObject != null)
			{
				Attack();
			}
		}
	}

	public void GetDamage(float Damage)
    {
		CurrentHP -= Damage - (CurrentArmor / 100) * Damage;
        if(CurrentHP <= 0)
        {

				Wallet.Instance.IncreseBalance((int) (Value * GetValueOfNonBattleUpgreade(TowerUpgeradeType.EnemyValueMulti)));
				LevelManager.instance.IncreseScorePoints(Value * 1.5f);
				EmeniesSpawner.onEnemyDestroy.Invoke();
				Destroy(gameObject);
				LevelManager.instance.UpdateMoneyText();
		}
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
