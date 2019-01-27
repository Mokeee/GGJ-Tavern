using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAnimation : MonoBehaviour
{
    public Light Light;
    public SpriteRenderer Flame;

    public float Amplitude = 1;
    public float Frequency = 1;

    public Vector3 Weights = new Vector3(1, 1, 1);

    private Vector3 Scale;
    private float Intensity;
    private float Distance;


    public void Start()
    {
        this.Scale = Flame.transform.localScale;
        this.Intensity = Light.intensity;
        this.Distance = Light.range;
    }


    // Update is called once per frame
    void Update()
    {
        float factor = (1 + Amplitude * Mathf.Sin(Time.time * Frequency));
        Vector3 weightedFactor = factor * Weights;
        weightedFactor.Scale(Scale);

        Flame.transform.localScale = weightedFactor;
        Light.intensity = factor * Intensity;
        Light.range = factor * Distance;
    }
}
