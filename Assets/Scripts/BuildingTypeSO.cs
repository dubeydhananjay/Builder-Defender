using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptableobjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string buildingName;
    public Transform buildingPrefab;

    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionAmountArray;
    public int healtAmountMax;
    public bool canGenerateResource;
    public float constructionTimerMax;

    public string GetEachResourceAmount()
    {
        string str = "";
        foreach (var item in constructionAmountArray)
            str += "<color=#" + item.resourceType.ColorHex + ">" + item.resourceType.resourceNameShort + item.amount + "</color> ";
        return str;

    }
}
