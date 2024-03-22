using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy Target;
    private float Damage;
    private float MoveSpeed;
	private bool DoubleDamage = false;

    public GameObject HitSpawnPrefab;
	//[SerializeField] private GameObject Explosion;

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
			if (!DoubleDamage)
			{
				Target.GetDamage(Damage);
			}
			else
			{
				Target.GetDamage(Damage * 2);
			}

			if (HitSpawnPrefab != null)
			{
				Instantiate(HitSpawnPrefab, transform.position, Quaternion.identity);
			}

			//GameObject _explosion = Instantiate(Explosion, transform.position, Quaternion.identity);

			Destroy(gameObject);
		}
	}

	public IEnumerable GetDoubleDamage()
	{
		DoubleDamage = true;
		yield return new WaitForSeconds(60);
		DoubleDamage = false;
	}

}
