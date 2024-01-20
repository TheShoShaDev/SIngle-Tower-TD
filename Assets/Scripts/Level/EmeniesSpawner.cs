using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EmeniesSpawner : MonoBehaviour
{
	[Header("Info")]
	[SerializeField] private Transform[] StartPoints;
	[SerializeField] public EnemyData[] EnemiesData;

	private int WaveNumber = 0;
	private float NewWaveDelay = 2f;

	private Enemy[] WaveEnemies;

	private void StartNewWave()
	{
		NewWaveDelay = 5f;
		WaveNumber++;

		if(WaveNumber % 10 == 0)
		{
			SpawnBoss();
		}
        else
        {
            
        }
    }

	private void SpawnBoss()
	{

	}

	public Enemy[] GetWaveEnemies()
	{

		return WaveEnemies;
	}


}

public class EnemyData
{
	public string Enemy { get; set; }
	public int Value1 { get; set; }
	public int Value2 { get; set; }
}

[CustomEditor(typeof(EmeniesSpawner))]
public class EmeniesSpawnerEditor : Editor
{
	SerializedProperty enemiesData;

	private void OnEnable()
	{
		enemiesData = serializedObject.FindProperty("EnemiesData");
	}

	public override void OnInspectorGUI()
	{

		serializedObject.Update();

		EditorGUILayout.PropertyField(enemiesData, true);

		serializedObject.ApplyModifiedProperties();
	}
}