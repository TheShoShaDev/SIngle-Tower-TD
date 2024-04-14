using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyDataSet : ScriptableObject
{
	public GameObject _enemyPrefab;
	public int _waveMod;
	public int _baseCount;
	public float _waveMulti;
	public float _waveDmgMulti;
	public float _waveHpMulti;
}

