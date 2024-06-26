using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time;
    private float timeTillSpawn;
    private bool canSpawn = false;

    void Start()
    {
        timeTillSpawn = SetTimeTillSpawn();
		Invoke("SetCanSpawn", 1.5f);
	}

    // Update is called once per frame
    void Update()
    {
        if (!canSpawn)
        {
            return;
        }

        time += Time.deltaTime;

        if (time >= timeTillSpawn)
        {
            EmeniesSpawner.onWaveStart.Invoke();
            time = 0;
        }

    }

    private void SetCanSpawn()
    {
        canSpawn = true;
    }

    private float SetTimeTillSpawn()
    {
        return EmeniesSpawner.instance.GetNewWaveDelay();
    }
}
