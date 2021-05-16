using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
   private Image constructionTimerImage;
   [SerializeField] private BuildingConstruction buildingConstruction;

   private void Awake() {
       constructionTimerImage = transform.Find("mask").Find("image").GetComponent<Image>();
   }

   private void Update() {
       SetConstructionProgress();
   }

   private void SetConstructionProgress()
   {
       constructionTimerImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized;
   }


}
