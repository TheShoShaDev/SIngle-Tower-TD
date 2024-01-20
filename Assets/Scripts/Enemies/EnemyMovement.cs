using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool IsNeedToStop;



    private void Awake()
    {
        Movespeed = BaseMovespeed;
    }

    void Start()
    {
        target = LevelManager.instance.TowerPoint;
    }

    private void FixedUpdate()
    {
        if (!IsNeedToStop)
        {
            direction = (target.position - transform.position).normalized;

            rb.velocity = direction * Movespeed;
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("StopEnemyTrigger"))
        {
            rb.velocity = new Vector2(0, 0);
            IsNeedToStop = true;
            HostEnemy.SetCanAttack();
        }
	}
}
