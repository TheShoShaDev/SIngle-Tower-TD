using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("BaseStats")]
    [SerializeField] private float BaseHP = 100f;
    [SerializeField] private float BaseArmor = 0f;
    [SerializeField] private float BaseDamage = 1f;
    [SerializeField] private float BaseAttackSpeed = 3f;


    public static Enemy instance;

    [Header("Info")]
	[SerializeField] private float CurrentHP;
	[SerializeField] private float CurrentArmor;
	[SerializeField] private float CurrentDamage;
	[SerializeField] private float CurrentAttackSpeed;
	[SerializeField] private float LastAttackTime;
	[SerializeField] private Tower TowerPrefab;
	
	private bool CanAttack = false;

	private void Awake()
    {
		instance = this;
	}

    private void Start()
    {
		CurrentHP = BaseHP;
		CurrentArmor = BaseArmor;
		CurrentDamage = BaseDamage;
		CurrentAttackSpeed = BaseAttackSpeed;
	}

	private void Update()
	{
		if (Time.time - LastAttackTime > CurrentAttackSpeed && CanAttack)
		{
			LastAttackTime = Time.time;

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
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        TowerPrefab.GetDamage(CurrentDamage);
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
