using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "EnemyStats")]
public class EnemyStats : ScriptableObject
{
	public float BaseHp;
	public float BaseArmor;
	public float BaseDamage;
	public float BaseAttackSpeed;
	public int Value;
}
