using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static void SetPauseState(bool IsPaused)
	{
		Time.timeScale = IsPaused ? 0 : 1;
	}
}
