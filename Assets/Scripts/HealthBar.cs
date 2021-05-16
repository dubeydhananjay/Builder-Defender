using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform separatorContainer;

    private Transform barTransform;
    private void Awake()
    {
        barTransform = transform.Find("bar");
        separatorContainer = transform.Find("separatorContainer");
    }

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystemOnDamaged;
        healthSystem.OnHeal += HealthSystemOnHeal;
        healthSystem.OnHealthAmountChanged += HealthSystemOnHealthAmountChanged;
        AddHealthSeparators();
        HealthBarVisibility();

    }

    private void HealthSystemOnHealthAmountChanged(object sender, EventArgs e)
    {
        AddHealthSeparators();
    }

    private void HealthSystemOnHeal(object sender, EventArgs e)
    {
        UpdateBar();
        HealthBarVisibility();
    }

    private void HealthSystemOnDamaged(object sender, EventArgs e)
    {
        UpdateBar();
        HealthBarVisibility();
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalised, 1, 1);
    }

    private void HealthBarVisibility()
    {
        gameObject.SetActive(!healthSystem.FullHealth);
       
    }

    private void AddHealthSeparators()
    {
        try
        {
            Transform separatorTemplate = separatorContainer.Find("separatorTemplate");
            separatorTemplate.gameObject.SetActive(false);
            int healthAmountPerSeparator = 10;
            int barSize = 3;
            
            int healthSeparatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax / healthAmountPerSeparator);
            float barOneHealthAmountSize = (float)barSize / healthSystem.GetHealthAmountMax;

            foreach (Transform item in separatorContainer)
            {
                if (item == separatorTemplate) continue;
                Destroy(item.gameObject);
            }

            for (int i = 1; i < healthSeparatorCount; i++)
            {
                Transform separatorTransform = Instantiate(separatorTemplate, separatorContainer);
                separatorTransform.gameObject.SetActive(true);
                separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeparator, 0, 0);
            }
        }
        catch(System.Exception)
        {
            print("Err adding separator! Health Amount not set");
        }
    }
}
