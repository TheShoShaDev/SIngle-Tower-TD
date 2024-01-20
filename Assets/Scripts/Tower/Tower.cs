using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("BaseStats")]
    [SerializeField] private float BaseHP;
    [SerializeField] private float BaseDamage;
    [SerializeField] private float BaseArmor;
    [SerializeField] private float BaseAttackSpeed;
    [SerializeField] private float BaseProjectileSpeed;

    [Header("Attacking")]
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private Transform ProjectileSpawnPos;
    [SerializeField] private float LastAttackTime;

    [Header("Info")]
    [SerializeField] private float AttackRange;
    private List<Enemy> CurEnemiesInRange = new List<Enemy>();
    private Enemy CurEnemy;

    public TowerTargetPriority TargetPriority;

	public static Tower Instance;

    private float CurrentHP;
    private float CurrentDamage;
    private float CurrentArmor;
    private float CurrentAttackSpeed;
    private float CurrentProjectileSpeed;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
		CurrentHP = BaseHP;
        CurrentDamage = BaseDamage;
        CurrentArmor = BaseArmor;
        CurrentAttackSpeed = BaseAttackSpeed;
        CurrentProjectileSpeed = BaseProjectileSpeed;
    }

    void Update()
    {
        if(Time.time - LastAttackTime > CurrentAttackSpeed)
        {
            LastAttackTime = Time.time;
            CurEnemy = GetEnemy();
            if(CurEnemy != null)
            {
                Attack();
            }
        }
    }

    public enum TowerTargetPriority
    {
        First,
        Strong,
        Close
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Enemy"))
        {
            CurEnemiesInRange.Add(collision.GetComponent<Enemy>());
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Enemy"))
        {
            CurEnemiesInRange.Remove(collision.GetComponent<Enemy>());
        }
	}

	public void GetDamage(float Damage)
    {
        CurrentHP -= Damage - (CurrentArmor / 100) * Damage;
    }

	private void Attack()
    {
        GameObject proj = Instantiate(ProjectilePrefab, ProjectileSpawnPos.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Initialize(CurEnemy, CurrentDamage, CurrentProjectileSpeed);

	}

    private Enemy GetEnemy()
    {
        CurEnemiesInRange.RemoveAll(x => x == null);

        if(CurEnemiesInRange.Count == 0)
        {
            return null;
        }

        if(CurEnemiesInRange.Count == 1)
        {
            return CurEnemiesInRange[0];
        }

        switch(TargetPriority)
        {
            case TowerTargetPriority.First:
                {
                    return CurEnemiesInRange[0];
                }

            case TowerTargetPriority.Strong:
                {
                    Enemy Strongest = null;
                    float StrongestHP = 0;
                    foreach(Enemy enemy in CurEnemiesInRange)
                    {
                        if(enemy.GetCurrentHP() > StrongestHP)
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
