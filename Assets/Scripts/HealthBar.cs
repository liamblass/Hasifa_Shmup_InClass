using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFront;
    [SerializeField] private float changeDuration = 0.1f;

    private void Awake()
    {
        Health.OnPlayerLoseHealth += UpdateHealth;
    }

    private void OnDestroy()
    {
        Health.OnPlayerLoseHealth -= UpdateHealth;
    }

    private void UpdateHealth(int maxValue, int newValue)
    {
        StartCoroutine(UpdateHealthCoroutine(maxValue, newValue));

    }

    private IEnumerator UpdateHealthCoroutine(int maxValue, int newValue)
    {
        float initialFill = healthBarFront.fillAmount;
        float targetFill = (float)newValue / (float)maxValue;

        float timeElapsed = 0;

        while (timeElapsed < changeDuration)
        {
            timeElapsed += Time.deltaTime;

            healthBarFront.fillAmount = Mathf.Lerp(initialFill, targetFill, timeElapsed / changeDuration);

            yield return null;
        }
    }
}
