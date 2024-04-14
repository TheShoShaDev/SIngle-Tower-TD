using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
	
	[Header("References")]
	[SerializeField] private Rigidbody2D _rigidBody;
	[SerializeField] private Enemy _hostEnemy;
	//[SerializeField] private Transform _towerPoint;

	[Header("Stats")]
	[SerializeField] private float _baseMovespeed = 2f;

	[SerializeField] private Transform _target;
	private float _movespeed;
	private Vector2 _direction;

	private void Awake()
	{
		_movespeed = _baseMovespeed;
	}

	private void Start()
	{
		_direction = (_target.position - transform.position).normalized;

		_rigidBody.velocity = _direction * _movespeed;

		Vector3 lookDirection = _target.position - transform.position;
		float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle - 90); 
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("StopEnemyTrigger"))
		{
			_rigidBody.velocity = new Vector2(0, 0);
			_hostEnemy._canAttack = true;
		}
	}

	private void OnValidate()
	{
		_rigidBody ??= GetComponent<Rigidbody2D>();
		_hostEnemy ??= GetComponent<Enemy>();
	}
}
