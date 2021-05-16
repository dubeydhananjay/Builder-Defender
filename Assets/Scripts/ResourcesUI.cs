using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, Transform> resourceTransformDict;
    private ResourceTypeListSO resourceTypeList;
    void Awake()
    {
        resourceTransformDict = new Dictionary<ResourceTypeSO, Transform>();
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        Transform resourceTemplate = transform.Find("resourceTemplate");
        resourceTemplate.gameObject.SetActive(false);
        Vector2 pos = resourceTemplate.GetComponent<RectTransform>().anchoredPosition;
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = pos;
            resourceTransform.GetComponentInChildren<Image>().sprite = resourceType.sprite;
            resourceTransformDict[resourceType] = resourceTransform;

            pos.x -= 100;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged +=  ResourceManagerOnResourceAmountChanged;
        UpdateResourceAmount();
    }

    private void ResourceManagerOnResourceAmountChanged(object sender,System.EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = resourceTransformDict[resourceType];
            resourceTransform.GetComponentInChildren<TextMeshProUGUI>().text = ResourceManager.Instance.GetResourceAmount(resourceType).ToString();
        }
    }


}
