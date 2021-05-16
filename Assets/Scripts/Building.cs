using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingType;
    private BuildingDemolishButton buildingDemolishButton;
    private BuildingRepairButton buildingRepairButton;
    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        buildingDemolishButton = GetComponentInChildren<BuildingDemolishButton>();
        buildingRepairButton = GetComponentInChildren<BuildingRepairButton>();

        healthSystem.SetHealthAmountMax(buildingType.healtAmountMax, true);
        healthSystem.OnDamaged += HealthSystemOnDamaged;
        healthSystem.OnDied += HealthSystemOnDied;
        healthSystem.OnHeal += HealthSystemOnHeal;
        RepairButtonActivation(false);

    }

    private void HealthSystemOnHeal(object sender, EventArgs e)
    {
        if (healthSystem.FullHealth)
            RepairButtonActivation(false);
    }

    private void HealthSystemOnDamaged(object sender, EventArgs e)
    {
        ChromaticAberrationEffect.Instance.SetVolumeWeight(1f);
        CinemachineShake.Instance.ShakeCamera(7f, 0.15f);
        RepairButtonActivation(true);
        SoundManager.Instance.PlayAudio(SoundManager.Sound.BuildingDamaged);
    }

    private void HealthSystemOnDied(object sender, EventArgs e)
    {
        ChromaticAberrationEffect.Instance.SetVolumeWeight(1f);
        CinemachineShake.Instance.ShakeCamera(10f, 0.2f);
        SoundManager.Instance.PlayAudio(SoundManager.Sound.BuildingDestroyed);
        Instantiate(GameAssets.Instance.pfBuildingDestroyedParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        DemolishButtonActivation(true);
    }

    private void OnMouseExit()
    {
        DemolishButtonActivation(false);
    }

    private void DemolishButtonActivation(bool flag)
    {
        if (buildingDemolishButton)
            buildingDemolishButton.gameObject.SetActive(flag);
    }

    private void RepairButtonActivation(bool flag)
    {
        if (buildingRepairButton)
            buildingRepairButton.gameObject.SetActive(flag);
    }
}
