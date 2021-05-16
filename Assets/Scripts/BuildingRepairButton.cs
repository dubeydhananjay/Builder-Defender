using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;
    private void Awake()
    {
        GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            int missingHealth = healthSystem.GetHealthAmountMax - healthSystem.GetHealthAmount;
            int repairCost = missingHealth / 2;
            ResourceAmount[] resourceAmounts = new ResourceAmount[]
            {new ResourceAmount { resourceType = goldResourceType, amount = repairCost} };
           if(ResourceManager.Instance.CanAfford(resourceAmounts))
           {
               ResourceManager.Instance.SpendResourceAmount(resourceAmounts);
               healthSystem.HealFull();
           }
           else TooltipUI.Instance.Show("Cannot afford repair cost!", new TooltipUI.ToolTipTimer {timer = 2f});
        });
    }



}
