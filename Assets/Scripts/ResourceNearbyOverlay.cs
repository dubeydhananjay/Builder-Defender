using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    [SerializeField] private TMPro.TextMeshPro text;

    private void Awake() {
        Hide();
    }
    private void Update()
    {
        int nearbyResourceAmount = ResourceGenerator.GetResourceNearbyAmount(resourceGeneratorData,transform.position - transform.localPosition);
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount/resourceGeneratorData.maxResourceAmount * 100f);
        text.SetText(percent + "%");
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        gameObject.SetActive(true);
        this.resourceGeneratorData = resourceGeneratorData;
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    
   
}
