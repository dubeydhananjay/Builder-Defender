using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceTypeSO, int> resourceAmountDict;
    public event System.EventHandler OnResourceAmountChanged;
    private void Awake()
    {
        Instance = this;
        resourceAmountDict = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)        
            resourceAmountDict[resourceType] = 100;
        
    }

  

    public void AddResource(ResourceTypeSO resourceType, int amt)
    {
        resourceAmountDict[resourceType] += amt;
        OnResourceAmountChanged?.Invoke(this, System.EventArgs.Empty);

    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDict[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach (var item in resourceAmountArray)
        {
            if (GetResourceAmount(item.resourceType) < item.amount)
                return false; // cannot afford
        }

        return true; // can afford all
    }

    public void SpendResourceAmount(ResourceAmount[] resourceAmountArray)
    {
        foreach (var item in resourceAmountArray)
            resourceAmountDict[item.resourceType] -= item.amount;

    }
}
