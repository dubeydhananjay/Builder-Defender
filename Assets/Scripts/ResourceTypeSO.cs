using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptableobjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
    public string resourceName;
    public string resourceNameShort;
    public Sprite sprite;
    public string ColorHex;
}
