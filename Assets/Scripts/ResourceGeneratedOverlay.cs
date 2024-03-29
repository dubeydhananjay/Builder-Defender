﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceGeneratedOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform barTransform;
    void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetGeneratedAmountPerSecond().ToString("F1"));
        barTransform = transform.Find("bar");
    }

    // Update is called once per frame
    void Update()
    {
        barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
