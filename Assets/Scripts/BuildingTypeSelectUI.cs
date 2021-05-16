using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingTypeSelectUI : MonoBehaviour
{
    private Dictionary<BuildingTypeSO, Transform> buildingTypeTransformDict;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
    private void Awake()
    {
        buildingTypeTransformDict = new Dictionary<BuildingTypeSO, Transform>();
        Transform buildingTemplate = transform.Find("buildingTemplate");
        buildingTemplate.gameObject.SetActive(false);
        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        Vector2 pos = buildingTemplate.GetComponent<RectTransform>().anchoredPosition;
        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            if (ignoreBuildingTypeList.Contains(buildingType)) continue;
            Transform buildingTransform = Instantiate(buildingTemplate, transform);
            buildingTransform.gameObject.SetActive(true);
            buildingTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;
            buildingTransform.GetComponent<RectTransform>().anchoredPosition = pos;
            buildingTransform.GetComponent<Button>().onClick.AddListener(() =>
                BuildingManager.Instance.ActiveBuildingType = buildingType
            );
            pos.x += 150;
            buildingTypeTransformDict[buildingType] = buildingTransform;
            MouseEnterExitEvent mouseEnterExitEvent = buildingTransform.GetComponent<MouseEnterExitEvent>();
            mouseEnterExitEvent.OnMouseEnterHandler += (object sender, System.EventArgs e) => {
                TooltipUI.Instance.Show(buildingType.buildingName + "\n" + buildingType.GetEachResourceAmount());
            };
             mouseEnterExitEvent.OnMouseExitHandler += (object sender, System.EventArgs e) => {
                TooltipUI.Instance.Hide();
            };
        }
    }

    private void Start()
    {
        SelectedActiveBuildingtype();
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManagerOnActiveBuildingTypeChanged;
    }

    private void BuildingManagerOnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs eventArgs)
    {
        SelectedActiveBuildingtype();
    }

    private void SelectedActiveBuildingtype()
    {
        foreach (BuildingTypeSO buildingType in buildingTypeTransformDict.Keys)
            buildingTypeTransformDict[buildingType].Find("selected").gameObject.SetActive(false);

        if (BuildingManager.Instance.ActiveBuildingType != null)
            buildingTypeTransformDict[BuildingManager.Instance.ActiveBuildingType].Find("selected").gameObject.SetActive(true);
    }


}
