using UnityEngine;

public class CoreUI : MonoBehaviour
{
    [SerializeField] private Core targetCore;

    private void OnEnable()
    {
        if (targetCore != null)
        {
            targetCore.OnHealthChanged += UpdateHealthUI;
            targetCore.OnCoreDestroyed += HandleCoreDestroyed;
        }
    }

    private void OnDisable()
    {
        if (targetCore != null)
        {
            targetCore.OnHealthChanged -= UpdateHealthUI;
            targetCore.OnCoreDestroyed -= HandleCoreDestroyed;
        }
    }

    private void UpdateHealthUI(float healthPercentage)
    {
        Debug.Log($"[UI] CORE HP: %{Mathf.RoundToInt(healthPercentage * 100)}");
    }

    private void HandleCoreDestroyed()
    {
        Debug.Log("[UI] GAME OVER");
    }
}