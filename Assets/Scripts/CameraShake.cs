using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1;
    [SerializeField] private float shakeMagnitude = 1;

    private Vector3 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float timeElapsed = 0;

        while (timeElapsed < shakeMagnitude)
        {
            timeElapsed += Time.deltaTime;

            transform.position = initialPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;

            yield return new WaitForEndOfFrame();
        }

        transform.position = initialPos;
    }
}
