using System;
using UnityEngine;

public class Core : MonoBehaviour, IDamageable
{
    public float MaxHealth { get; private set; } = 100f;
    public float CurrentHealth { get; private set; }

    public event Action<float> OnHealthChanged;
    public event Action OnCoreDestroyed;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= amount;
        Debug.Log($"Core Damaged! Remaining HP: {CurrentHealth}");
        
        OnHealthChanged?.Invoke(CurrentHealth / MaxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnCoreDestroyed?.Invoke();
        Debug.Log("Core breached! Game Over.");
    }
}