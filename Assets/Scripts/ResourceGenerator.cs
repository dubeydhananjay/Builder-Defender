using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    private ResourceGeneratorData resourceGeneratorData;
    private float timer, timerMax;
    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        int nearbyResourceAmount = GetResourceNearbyAmount(resourceGeneratorData,transform.position);
        if (nearbyResourceAmount <= 0)
        {
            //no nearby resource so disable script
            enabled = false;
        }
        else
        {
            timerMax = (timerMax / 2f) +
            timerMax * (1 - (float)(nearbyResourceAmount / resourceGeneratorData.maxResourceAmount));
        }

    }

    public static int GetResourceNearbyAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, resourceGeneratorData.detectionRadius);
        int nearbyResourceAmount = 0;
        foreach (var collider in colliders)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if (resourceNode && resourceNode.resourceType == resourceGeneratorData.resourceType)
                nearbyResourceAmount++;
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }


    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer += timerMax;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        return timer / timerMax;
    }

    public float GetGeneratedAmountPerSecond()
    {
        return (float)1 / timerMax; // Amount of resource generated per second
    }
}
