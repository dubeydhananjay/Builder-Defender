using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ResourceNearbyOverlay resourceNearbyOverlay;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManagerOnActiveBuildingChanged;
        resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
    }

    private void BuildingManagerOnActiveBuildingChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.activeBuildingType == null)
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
            if (e.activeBuildingType.canGenerateResource)
                resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPos();
    }

    private void Show(Sprite ghostSprite)
    {
        spriteRenderer.sprite = ghostSprite;
        spriteRenderer.gameObject.SetActive(true);
    }

    private void Hide()
    {
        spriteRenderer.gameObject.SetActive(false);
    }

}
