using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform TowerPoint;
    [SerializeField] private TextMeshProUGUI MoneyText;

    private float ScorePoints;
    private bool IsPaused = false;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateMoneyText()
    {
        MoneyText.text = Wallet.Instance.GetValue().ToString();
    }

    public void IncreseScorePoints(float score)
    {
		ScorePoints += score;
    }

    public float GetScore()
    {
        return ScorePoints;
    }

    public void SetPauseState(bool IsPaused)
    {
        this.IsPaused = IsPaused;

		Time.timeScale = IsPaused ? 0 : 1;
	}

	public void IncreseTimeScale()
	{
		Time.timeScale += 0.5f;
	}

	public void DecreseTimeScale()
	{
		Time.timeScale -= 0.5f;
	}
}
