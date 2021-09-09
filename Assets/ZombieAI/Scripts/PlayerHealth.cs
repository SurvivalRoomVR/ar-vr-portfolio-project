using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float value = 100f;
    public PlayerHealth parentReference;
    public float damageMultiplier = 1.0f;

    public delegate void OnZeroHealthHandler();
    public event OnZeroHealthHandler OnZeroHealth;

    public void TakeDamage(float damage)
    {
        damage *= damageMultiplier;
        if (parentReference != null)
        {
            parentReference.TakeDamage(damage);
            return;
        }
        value -= damage;
        value = value < 0 ? 0 : value;
        if (value == 0)
        {
            OnZeroHealth?.Invoke();
        }
    }
}
