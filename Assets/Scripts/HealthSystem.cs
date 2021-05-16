using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public event EventHandler OnDamaged;
    public event EventHandler OnHeal;
    public event EventHandler OnDied;
    public event EventHandler OnHealthAmountChanged;
    private int healthAmount;
    [SerializeField] private int healthAmountMax;

    public int GetHealthAmount { get { return healthAmount; } }
    public int GetHealthAmountMax { get { return healthAmountMax; } }

    public float GetHealthAmountNormalised { get { return (float)healthAmount / healthAmountMax; } }

    public bool FullHealth { get { return healthAmount == healthAmountMax; } }

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }


    public void Damage(int damageAmount)
    {
        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead()) OnDied?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnHeal?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        healthAmount = healthAmountMax;
        OnHeal?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return healthAmount <= 0;
    }

    public void SetHealthAmountMax(int healthAmountMax, bool setHealthAmount = false)
    {
        this.healthAmountMax = healthAmountMax;
        if (setHealthAmount) healthAmount = healthAmountMax;
        OnHealthAmountChanged?.Invoke(this, EventArgs.Empty);
    }
}
