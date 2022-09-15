using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class TorchLight : MonoBehaviour
{
    public Light2D lightThis;
    private float time;
    private float timeDuration = 0.1f;
    public float MinIntensity = 1f;
    public float MaxIntensity = 2f;



    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            lightThis.intensity = Random.Range(MinIntensity, MaxIntensity);
            time = timeDuration;
        }
        
    }
}
