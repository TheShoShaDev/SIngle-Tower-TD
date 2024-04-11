using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Enemy HostEnemy;


    [Header("Stats")]
    [SerializeField] private float BaseMovespeed = 2f;


	private Transform target;
    private float Movespeed;
    private Vector2 direction;



    private void Awake()
    {
        Movespeed = BaseMovespeed;
    }

	private void Start()
    {
        target = LevelManager.instance.TowerPoint;
		direction = (target.position - transform.position).normalized;

		rb.velocity = direction * Movespeed;

		Vector3 lookDirection = target.position - transform.position;
		float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Корректируем угол для спрайтов в 2D
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("StopEnemyTrigger"))
        {
            rb.velocity = new Vector2(0, 0);
            HostEnemy.SetCanAttack();
        }
	}
}
