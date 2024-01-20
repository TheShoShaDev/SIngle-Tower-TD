using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy Target;
    private float Damage;
    private float MoveSpeed;

    public GameObject HitSpawnPrefab;

	public void Initialize(Enemy target, float damage, float moveSpeed)
	{
		this.Target = target;
		this.Damage = damage;
		this.MoveSpeed = moveSpeed;
	}

	private void Update()
	{
		if(Target != null)
		{
			transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);
			//transform.LookAt(Target.transform);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			Target.GetDamage(Damage);

			if (HitSpawnPrefab != null)
			{
				Instantiate(HitSpawnPrefab, transform.position, Quaternion.identity);
			}

			Destroy(gameObject);
		}
	}

}
