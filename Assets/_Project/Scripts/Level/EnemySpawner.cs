using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
	[Header("Info")]
	[SerializeField] private float enemiesPerSecond = 1.0f;
	[SerializeField] private Transform[] StartPoints;

	[Header("References")]
	[SerializeField] private List<EnemyDataSet> BossPrefab = new List<EnemyDataSet>();
	[SerializeField] public List<EnemyDataSet> EnemiesData = new List<EnemyDataSet>();

	[Header("Events")]
	public static UnityEvent onEnemyDestroy = new UnityEvent();
	public static UnityEvent onWaveStart = new UnityEvent();

	[Header("Some Var")]
	[SerializeField] private static int _waveNumber = 0;
	[SerializeField] private static float _newWaveDelay = 10f;
	[SerializeField] private float _timeSinceLastSpawn;
	[SerializeField] private int enemiesAlive;
	[SerializeField] private int enemiesLeftToSpawn;
	[SerializeField] private bool _IsSpawning = false;

	private List<GameObject> WaveEnemies;

	private void Awake()
	{
		onEnemyDestroy.AddListener(EnemyDestroyed);
		//onWaveStart.AddListener(WaveStarted);
	}

	private void EnemyDestroyed()
	{
		enemiesAlive--;
		if (enemiesAlive == 0
			&& enemiesLeftToSpawn == 0)
		{
			//EndWave();
		}
	}

	private void WaveStarted()
	{
		StartNewWave();
	}

	private void Start()
	{
		enemiesLeftToSpawn = GetEnemiesCount();
		Invoke("StartNewWave", 1.5f);
	}

	private void Update()
	{
		if (!_IsSpawning)
		{
			return;
		}

		_timeSinceLastSpawn += Time.deltaTime;

		if (_timeSinceLastSpawn >= 1 / enemiesPerSecond
			&& enemiesLeftToSpawn > 0)
		{
			if (_waveNumber % 10 == 0
				&& _waveNumber >= 10)
			{
				SpawnBoss();
			}
			else
			{
				SpawnRegularEnemy();
			}

			_timeSinceLastSpawn = 0;
			enemiesLeftToSpawn--;
			enemiesAlive++;

		}
	}

	private void StartNewWave()
	{
		if (_waveNumber % 10 == 0
			&& enemiesAlive > 0)
		{
			return;
		}
		EndWave();
		_IsSpawning = true;
		_waveNumber++;
		GetWaveEnemies();
		enemiesLeftToSpawn = GetEnemiesCount();
		EventBus.WaveChanged();
	}

	private int GetEnemiesCount()
	{
		if (_waveNumber % 10 == 0)
		{
			return 1;
		}
		else
		{
			return WaveEnemies.Count;
		}
	}

	private void EndWave()
	{
		_IsSpawning = false;
		_timeSinceLastSpawn = 0f;

		foreach (EnemyDataSet enemy in EnemiesData)
		{
			enemy._enemyPrefab.GetComponent<Enemy>().IncreseStats(enemy._waveDmgMulti * _waveNumber, enemy._waveHpMulti * _waveNumber);
		}
		foreach (EnemyDataSet enemy in BossPrefab)
		{
			enemy._enemyPrefab.GetComponent<Enemy>().IncreseStats(enemy._waveDmgMulti * _waveNumber, enemy._waveHpMulti * _waveNumber);
		}

		//EventBus.IncreseBalance():

		//Wallet.Instance.IncreseBalance((int)GetValueOfNonBattleUpgreade(TowerUpgeradeType.EndWaveIncome));
	}

	private void SpawnRegularEnemy()
	{
		GameObject _prefabToSpawn = WaveEnemies[UnityEngine.Random.Range(0, WaveEnemies.Count - 1)];
		Instantiate(_prefabToSpawn, GetStartPoint(), Quaternion.identity);
		WaveEnemies.Remove(_prefabToSpawn);
	}

	private Vector3 GetStartPoint()
	{
		int _index = UnityEngine.Random.Range(0, StartPoints.Length);
		return StartPoints[_index].position;
	}

	private void SpawnBoss()
	{
		BossPrefab[0]._enemyPrefab.GetComponent<Enemy>().IncreseStats(BossPrefab[0]._waveDmgMulti * _waveNumber, BossPrefab[0]._waveHpMulti * _waveNumber);
		GameObject _prefabToSpawn = BossPrefab[0]._enemyPrefab;
		Instantiate(_prefabToSpawn, GetStartPoint(), Quaternion.identity);
	}

	public void GetWaveEnemies()
	{
		WaveEnemies = new List<GameObject>();

		foreach (EnemyDataSet _enemyData in EnemiesData)
		{
			if (_waveNumber % _enemyData._waveMod == 0
				&& _waveNumber >= _enemyData._waveMod
			&& _waveNumber != 10)
			{
				for (int i = 0; i < _enemyData._baseCount; i++)
				{
					WaveEnemies.Add(_enemyData._enemyPrefab);
				}

			}
		}

	}

	public static float GetNewWaveDelay()
	{
		return _newWaveDelay;
	}

	public static int GetCurrentWave()
	{
		return _waveNumber;
	}

	private void OnEnable()
	{
		EventBus.onWaveStart += WaveStarted;
	}

	private void OnDisable()
	{
		EventBus.onWaveStart -= WaveStarted;
	}
}
