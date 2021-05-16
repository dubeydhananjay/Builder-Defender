using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction CreateBuilding(Vector3 position, BuildingTypeSO buildingType)
    {
        BuildingConstruction pfBuildingConstruction = GameAssets.Instance.pfBuildingConstruction;
        BuildingConstruction buildingConstruction = Instantiate(pfBuildingConstruction, position, Quaternion.identity);
        buildingConstruction.SetBuildingType(buildingType);
        return buildingConstruction;
    }

    private float constructionTimerMax;
    private float constructionTimer;
    private BuildingTypeSO buildingType;
    private BoxCollider2D boxCollider2D;
    public float GetConstructionTimerNormalized { get { return 1 - constructionTimer / constructionTimerMax; } }
    private SpriteRenderer spriteRenderer;
    private Material materialBuildingConstruction;
    private BuildingTypeHolder buildingTypeHolder;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        materialBuildingConstruction = spriteRenderer.material;
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
    }
    private void Update()
    {
        constructionTimer -= Time.deltaTime;
        materialBuildingConstruction.SetFloat("_Progress", GetConstructionTimerNormalized);
        if (constructionTimer <= 0)
        {
            Instantiate(GameAssets.Instance.pfBuildingPlacedParticles, transform.position, Quaternion.identity);
            Instantiate(buildingType.buildingPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        this.buildingType = buildingType;
        constructionTimer = constructionTimerMax = buildingType.constructionTimerMax;
        boxCollider2D.offset = buildingType.buildingPrefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.buildingPrefab.GetComponent<BoxCollider2D>().size;
        spriteRenderer.sprite = buildingType.sprite;
        buildingTypeHolder.buildingType = buildingType;
    }
}
