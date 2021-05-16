using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    private BuildingTypeSO activeBuildingType;

    private BuildingTypeListSO buildingTypeList;
    [SerializeField] private Building buildingHQ;
    public Building GetHQBuilding { get { return buildingHQ; } }


    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    public BuildingTypeSO ActiveBuildingType
    {
        get { return activeBuildingType; }
        set
        {
            activeBuildingType = value;
            OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs
            {
                activeBuildingType = activeBuildingType
            });
        }
    }
    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingHQ.GetComponent<HealthSystem>().OnDied += HQOnDied;

    }

    private void HQOnDied(object sender, EventArgs e)
    {
        SoundManager.Instance.PlayAudio(SoundManager.Sound.GameOver);
        GameOverUI.Instance.Show();
    }


    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())

#else
        if(UtilsClass.Tap() && !UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject)
          
#endif
            SpawnBuilding();



    }

    private void SpawnBuilding()
    {
        if (activeBuildingType)
        {
            if (CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPos(), out string em))
            {
                if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionAmountArray))
                {
                    Vector3 placedPos = UtilsClass.GetMouseWorldPos();
                    Instantiate(GameAssets.Instance.pfBuildingPlacedParticles, placedPos, Quaternion.identity);
                    BuildingConstruction.CreateBuilding(placedPos, activeBuildingType);
                    ResourceManager.Instance.SpendResourceAmount(activeBuildingType.constructionAmountArray);
                    SoundManager.Instance.PlayAudio(SoundManager.Sound.BuildingPlaced);
                }
                else TooltipUI.Instance.Show("Cannot afford " + activeBuildingType.GetEachResourceAmount(),
                new TooltipUI.ToolTipTimer { timer = 2f });
            }
            else TooltipUI.Instance.Show(em, new TooltipUI.ToolTipTimer { timer = 2f });
        }
    }
    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errMsg)
    {
        BoxCollider2D boxCollider2D = buildingType.buildingPrefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        bool isAreaClear = collider2Ds.Length == 0;
        if (!isAreaClear)
        {
            errMsg = "Area is not clear";
            return false;
        }

        collider2Ds = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D col in collider2Ds)
        {
            //Colliders in Construction radius
            BuildingTypeHolder buildingTypeHolder = col.gameObject.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder)
            {
                //Has a building type holder
                if (buildingTypeHolder.buildingType == buildingType)
                { //Check whether same building type already constructed in the construction radius
                    errMsg = "Too close to same building type";
                    return false;
                }
            }
        }

        if (buildingType.canGenerateResource)
        {
            ResourceGeneratorData resourceGeneratorData = buildingType.resourceGeneratorData;
            int nearbyResourceAmount = ResourceGenerator.GetResourceNearbyAmount(resourceGeneratorData, position);
            if (nearbyResourceAmount <= 0)
            {
                errMsg = "There is no nearby resource nodes!";
                return false;
            }
        }

        float maxConstructionRadius = 25;
        collider2Ds = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D col in collider2Ds)
        {
            BuildingTypeHolder buildingTypeHolder = col.gameObject.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder)
            { // Check if there is any building in the max construction radius
                errMsg = "";
                return true;
            }
        }
        errMsg = "Too far from any other building";
        return false;
    }
}
