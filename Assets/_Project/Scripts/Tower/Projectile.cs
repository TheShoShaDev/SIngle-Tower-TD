using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
	private Enemy _target;
	private Vector3 _enemyPosition;
	private float _damage;
	private float _moveSpeed = 2;
	private bool _IsDoubleDamage = false;
	private bool _IsGottaBeDestroyed;

	public GameObject HitSpawnPrefab;

	public void Initialize(Enemy target, float damage)
	{
		this._target = target;
		this._damage = damage;
		_enemyPosition = target.transform.position;
	}

	private void Update()
	{
		if (_target != null)
		{
			transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
			_enemyPosition = _target.transform.position;
		}
		else
		{
			transform.position = Vector2.MoveTowards(transform.position, _enemyPosition + transform.position, _moveSpeed * Time.deltaTime);
			if (!_IsGottaBeDestroyed)
			{
				StartCoroutine(DestroyInTime());
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			if (!_IsDoubleDamage)
			{
				_target.GetDamage(_damage);
			}
			else
			{
				_target.GetDamage(_damage * 2);
			}

			if (HitSpawnPrefab != null)
			{
				Instantiate(HitSpawnPrefab, transform.position, Quaternion.identity);
			}

			Destroy(gameObject);
		}
	}

	public IEnumerator GetDoubleDamage()
	{
		_IsDoubleDamage = true;
		yield return new WaitForSeconds(60);
		_IsDoubleDamage = false;
	}

	public IEnumerator DestroyInTime()
	{
		_IsGottaBeDestroyed = true;
		yield return new WaitForSeconds(5);
		if (gameObject != null)
		{
			Destroy(gameObject);
		}
	}


}
