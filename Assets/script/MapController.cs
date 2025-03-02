using System.Collections;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    public SurfaceEffector2D surfaceEffector; // Assign in Inspector
    public float multiplier = 1.005f;
    public float interval = 5f; 

    private void Start()
    {
        if (surfaceEffector == null)
        {
            surfaceEffector = GetComponent<SurfaceEffector2D>();
        }

        StartCoroutine(IncreaseSpeedOverTime());
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            if (surfaceEffector != null)
            {
                surfaceEffector.speed *= multiplier;
                Debug.Log($"New Speed: {surfaceEffector.speed}");
            }
        }
    }
}