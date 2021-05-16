using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChromaticAberrationEffect : MonoBehaviour
{
    public static ChromaticAberrationEffect Instance { get; private set; }
    private Volume volume;
    private void Awake()
    {
        Instance = this;
        volume = GetComponent<Volume>();
    }

    private void Update() {
        if(volume.weight > 0)
        {
            float decreasedSpeed = 1;
            volume.weight -= Time.deltaTime * decreasedSpeed;
        }
    }

    public void SetVolumeWeight(float weight)
    {
        volume.weight = weight;
    }
}
