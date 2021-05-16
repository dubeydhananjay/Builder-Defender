﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour
{
    [SerializeField] private Building building;
    private void Awake()
    {
        GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
            foreach (ResourceAmount resourceAmount in buildingType.constructionAmountArray)
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * 0.6f));

            Destroy(building.gameObject);
        });
    }



}
