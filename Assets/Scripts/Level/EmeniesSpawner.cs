using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Events;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;
using static TowerUpgradeHelpers;

public class EmeniesSpawner : MonoBehaviour
{
	[Header("Info")]
	[SerializeField] private float enemiesPerSecond = 1.0f;
	[SerializeField] private Transform[] StartPoints;

	[Header("References")]
	[SerializeField] private List<EnemyData> BossPrefab = new List<EnemyData>();
	[SerializeField] public List<EnemyData> EnemiesData = new List<EnemyData>();

	[Header("Events")]
	public static UnityEvent onEnemyDestroy = new UnityEvent();
	public static UnityEvent onWaveStart = new UnityEvent();

	[Header("Some Var")]
	[SerializeField] private int WaveNumber = 0;
	[SerializeField] private float NewWaveDelay = 10f;
	[SerializeField] private float timeSinceLastSpawn;
	[SerializeField] private int enemiesAlive;
	[SerializeField] private int enemiesLeftToSpawn;
	[SerializeField] private bool IsSpawning = false;

	public static EmeniesSpawner instance;

	private List<GameObject> WaveEnemies;

	private void Awake()
	{
		instance = this;
		onEnemyDestroy.AddListener(EnemyDestroyed);
		onWaveStart.AddListener(WaveStarted);
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
		if (!IsSpawning)
		{
			return;
		}

		timeSinceLastSpawn += Time.deltaTime;

		if (timeSinceLastSpawn >= 1 / enemiesPerSecond
			&& enemiesLeftToSpawn > 0)
		{
			if (WaveNumber % 10 == 0
				&& WaveNumber >= 10)
			{
				SpawnBoss();
			}
			else
			{
				SpawnRegularEnemy();
			}

			timeSinceLastSpawn = 0;
			enemiesLeftToSpawn--;
			enemiesAlive++;

		}
	}

	private void StartNewWave()
	{
		//if(enemiesLeftToSpawn > 0
		//	&& enemiesAlive > 0) 
		if (WaveNumber % 10 == 0
			&& enemiesAlive > 0)
		{
			return;
		}
		EndWave();
		IsSpawning = true;
		WaveNumber++;
		GetWaveEnemies();
		enemiesLeftToSpawn = GetEnemiesCount();
		UIManager.Instance.WriteNewWave();
	}

	private int GetEnemiesCount()
	{
		if (WaveNumber % 10 == 0)
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
		IsSpawning = false;
		timeSinceLastSpawn = 0f;

		foreach (EnemyData enemy in EnemiesData)
		{
			enemy.EnemyPrefab.GetComponent<Enemy>().IncreseStats(enemy.waveDmgMulti * WaveNumber, enemy.waveHpMulti * WaveNumber);
		}
		foreach (EnemyData enemy in BossPrefab)
		{
			enemy.EnemyPrefab.GetComponent<Enemy>().IncreseStats(enemy.waveDmgMulti * WaveNumber, enemy.waveHpMulti * WaveNumber);
		}

		Wallet.Instance.IncreseBalance((int) GetValueOfNonBattleUpgreade(TowerUpgeradeType.EndWaveIncome));
	} 

	private void SpawnRegularEnemy()
	{
		GameObject prefabToSpawn = WaveEnemies[UnityEngine.Random.Range(0, WaveEnemies.Count - 1)];
		Instantiate(prefabToSpawn, GetStartPoint(), Quaternion.identity);
		WaveEnemies.Remove(prefabToSpawn);
	}

	private Vector3 GetStartPoint()
	{
		int index = UnityEngine.Random.Range(0, StartPoints.Length);
		return StartPoints[index].position;
	}

	private void SpawnBoss()
	{
		BossPrefab[0].EnemyPrefab.GetComponent<Enemy>().IncreseStats(BossPrefab[0].waveDmgMulti * WaveNumber, BossPrefab[0].waveHpMulti * WaveNumber);
		GameObject prefabToSpawn = BossPrefab[0].EnemyPrefab;
		Instantiate(prefabToSpawn, GetStartPoint(), Quaternion.identity);
	}

	public void GetWaveEnemies()
	{
		WaveEnemies = new List<GameObject>();

		foreach (EnemyData _enemyData in EnemiesData)
		{
			if (WaveNumber % _enemyData.WaveMod == 0
				&& WaveNumber >= _enemyData.WaveMod
			&& WaveNumber != 10)
			{
				int EnemyCount = Convert.ToInt32(_enemyData.baseCount * _enemyData.waveMulti);
				for (int i = 0; i < EnemyCount; i++)
				{
					WaveEnemies.Add(_enemyData.EnemyPrefab);
				}

			}
		}

	}

	public float GetNewWaveDelay()
	{
		return NewWaveDelay;
	}

	public int GetCurrentWave()
	{
		return WaveNumber;
	}
}

[Serializable]
public class EnemyData
{
	public GameObject EnemyPrefab;
	public int WaveMod;
	public int baseCount;
	public float waveMulti;
	public float waveDmgMulti;
	public float waveHpMulti;
}