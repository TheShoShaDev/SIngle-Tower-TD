using UnityEngine;
using UnityEngine.UI;

abstract public class HealthBar : MonoBehaviour
{
	[SerializeField] protected Image HealthBarImage;
	protected void DrawHealth(float currentHp, float maxHp)
	{
		HealthBarImage.fillAmount = currentHp / maxHp;
	}
}
